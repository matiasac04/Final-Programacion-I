-- ============================================================================
--  sys_comercio - script de creacion de la base de datos
--  SQL Server. Uso: abrir en SQL Server Management Studio y ejecutar.
--  (Si la base ya existe, el script no la vuelve a crear.)
-- ============================================================================

USE master

GO
IF DB_ID('comercio') IS NULL
    EXEC('CREATE DATABASE [comercio] COLLATE Modern_Spanish_CI_AS')

GO
USE [comercio]

GO
-- ============================ TABLAS ============================
IF OBJECT_ID('dbo.usuarios', 'U') IS NULL
CREATE TABLE [dbo].[usuarios](
	[id_usuario] [int] NOT NULL,
	[usuario] [nvarchar](15) NULL,
	[psw] [int] NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED ([id_usuario] ASC)
)
GO
IF OBJECT_ID('dbo.clientes', 'U') IS NULL
CREATE TABLE [dbo].[clientes](
	[id_cliente] [int] NOT NULL,
	[razon_social] [nvarchar](60) NULL,
 CONSTRAINT [PK_clientes] PRIMARY KEY CLUSTERED ([id_cliente] ASC)
)
GO
IF OBJECT_ID('dbo.productos', 'U') IS NULL
CREATE TABLE [dbo].[productos](
	[codigo] [int] NOT NULL,
	[descripcion] [nvarchar](60) NULL,
	[stock] [int] NULL,
	[precio] [decimal](18, 2) NULL,
 CONSTRAINT [PK_productos] PRIMARY KEY CLUSTERED ([codigo] ASC)
)
GO
IF OBJECT_ID('dbo.ventas', 'U') IS NULL
CREATE TABLE [dbo].[ventas](
	[id_venta] [int] NOT NULL,
	[fecha] [datetime] NULL,
	[id_cliente] [int] NULL,
	[importe] [decimal](18, 2) NULL,
	[id_usuario] [int] NULL,
 CONSTRAINT [PK_ventas] PRIMARY KEY CLUSTERED ([id_venta] ASC)
)
GO
IF OBJECT_ID('dbo.detalle_venta', 'U') IS NULL
CREATE TABLE [dbo].[detalle_venta](
	[id_venta] [int] NOT NULL,
	[codigo] [int] NOT NULL,
	[precio] [decimal](18, 2) NULL,
	[cant] [int] NULL,
	[precio_r] [decimal](18, 2) NULL,
 CONSTRAINT [PK_detalle_venta] PRIMARY KEY CLUSTERED ([id_venta] ASC, [codigo] ASC)
)
GO
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_ventas_clientes')
    ALTER TABLE [dbo].[ventas] ADD CONSTRAINT [FK_ventas_clientes] FOREIGN KEY ([id_cliente]) REFERENCES [dbo].[clientes] ([id_cliente])
GO
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_ventas_usuarios')
    ALTER TABLE [dbo].[ventas] ADD CONSTRAINT [FK_ventas_usuarios] FOREIGN KEY ([id_usuario]) REFERENCES [dbo].[usuarios] ([id_usuario])
GO
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_detalle_venta_ventas')
    ALTER TABLE [dbo].[detalle_venta] ADD CONSTRAINT [FK_detalle_venta_ventas] FOREIGN KEY ([id_venta]) REFERENCES [dbo].[ventas] ([id_venta])
GO
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_detalle_venta_productos')
    ALTER TABLE [dbo].[detalle_venta] ADD CONSTRAINT [FK_detalle_venta_productos] FOREIGN KEY ([codigo]) REFERENCES [dbo].[productos] ([codigo])
GO

-- ==================== USUARIOS DE DEMOSTRACION ====================
-- (psw en texto plano: es un TP academico, no usar asi en produccion)
IF NOT EXISTS (SELECT 1 FROM usuarios WHERE id_usuario = 1)
    INSERT INTO usuarios (id_usuario, usuario, psw) VALUES (1, 'admin', 123)
GO
IF NOT EXISTS (SELECT 1 FROM usuarios WHERE id_usuario = 2)
    INSERT INTO usuarios (id_usuario, usuario, psw) VALUES (2, 'Matias', 456)

GO
-- ================ PROCEDIMIENTOS ALMACENADOS ==================
CREATE OR ALTER PROCEDURE sp_login
    @usuario NVARCHAR(50),
    @psw NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT id_usuario, usuario
    FROM usuarios
    WHERE usuario = @usuario AND psw = @psw;
END
GO

GO
create or alter procedure spu_actualizar_venta
@id_venta int,
@id_cliente int,
@importe decimal(18,2)
as
update ventas
set id_cliente = @id_cliente,
importe = @importe
where id_venta = @id_venta
GO

GO
CREATE OR ALTER PROCEDURE spu_alta_cliente
@id_cliente INT,
@razon_social NVARCHAR(60)
AS
INSERT INTO clientes
VALUES(@id_cliente, @razon_social)
GO

GO
CREATE OR ALTER PROCEDURE spu_alta_productos
@codigo INT,
@descripcion NVARCHAR(60),
@stock INT,
@precio DECIMAL(18,2)
AS
INSERT INTO productos
VALUES(@codigo, @descripcion, @stock, @precio)
GO

GO
create or alter procedure spu_alta_venta
@id_venta int,
@fecha datetime,
@id_cliente int,
@importe decimal,
@id_usuario int
as
insert into ventas
values(@id_venta, @fecha, @id_cliente, @importe, @id_usuario)
GO

GO
CREATE OR ALTER PROCEDURE [dbo].[spu_backup]
    @ruta_destino NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Carpeta destino: la que se pasa por parametro o, si no se pasa,
    -- la carpeta de backup por defecto de la instancia.
    DECLARE @ruta NVARCHAR(500) =
        ISNULL(NULLIF(LTRIM(RTRIM(@ruta_destino)), ''),
               CONVERT(NVARCHAR(500), SERVERPROPERTY('InstanceDefaultBackupPath')));

    IF @ruta IS NULL
    BEGIN
        RAISERROR('No se pudo determinar la carpeta de destino: pasá @ruta_destino o configurá InstanceDefaultBackupPath en la instancia.', 16, 1);
        RETURN;
    END

    IF RIGHT(@ruta, 1) <> '\'
        SET @ruta = @ruta + '\';

    -- yyyyMMdd.bak, sin pisar un respaldo anterior del mismo dia.
    DECLARE @archivo NVARCHAR(500) = @ruta + CONVERT(NVARCHAR(8), GETDATE(), 112) + '.bak';
    DECLARE @existe INT;
    EXEC master.dbo.xp_fileexist @archivo, @existe OUTPUT;

    DECLARE @correlativo INT = 1;
    WHILE @existe = 1
    BEGIN
        SET @archivo = @ruta + CONVERT(NVARCHAR(8), GETDATE(), 112) + '_' + CAST(@correlativo AS NVARCHAR(3)) + '.bak';
        EXEC master.dbo.xp_fileexist @archivo, @existe OUTPUT;
        SET @correlativo = @correlativo + 1;
    END

    -- BACKUP DATABASE necesita ir en su propio lote: por eso se ejecuta con SQL dinamico.
    -- Se respaldo DB_NAME() (la base desde la que se llama), no un nombre fijo.
    DECLARE @sql NVARCHAR(MAX) =
        N'BACKUP DATABASE ' + QUOTENAME(DB_NAME()) + N' TO DISK = @archivo WITH INIT;';

    EXEC sys.sp_executesql @sql, N'@archivo NVARCHAR(500)', @archivo = @archivo;

    SELECT @archivo AS archivo_backup;
END
GO

GO
CREATE OR ALTER PROCEDURE spu_baja_cliente
@id_cliente INT
as
DELETE FROM clientes
WHERE id_cliente = @id_cliente
GO

GO
CREATE OR ALTER PROCEDURE spu_baja_productos
@codigo INT
AS
DELETE FROM productos WHERE codigo = @codigo
GO

GO
CREATE OR ALTER PROCEDURE spu_cargar_detalleventa
@id_venta INT,
@codigo INT,
@precio DECIMAL,
@cant INT,
@precio_r DECIMAL
AS
INSERT INTO Detalle_Venta
VALUES(@id_venta, @codigo, @precio, @cant, @precio_r);
GO

GO
CREATE OR ALTER PROCEDURE spu_eliminar_detalles_por_venta
    @id_venta INT
AS
BEGIN
    DELETE FROM detalle_venta
    WHERE id_venta = @id_venta
END
GO

GO
CREATE OR ALTER PROCEDURE spu_eliminar_detalleventa
  @id_venta INT,
    @codigo INT
AS
    DELETE FROM detalle_venta
    WHERE id_venta = @id_venta AND codigo = @codigo
GO

GO
CREATE OR ALTER PROCEDURE spu_eliminar_venta
@id_venta INT
as
DELETE FROM ventas where id_venta = @id_venta
GO

GO
create or alter procedure spu_id_nueva_cliente
as
declare @id_cliente int;
SELECT @id_cliente = ISNULL(MAX(id_cliente), 0) + 1 FROM clientes;
SELECT @id_cliente AS nuevo_id_cliente;
GO

GO
create or alter procedure spu_id_nueva_producto
as
declare @codigo int;
SELECT @codigo = ISNULL(MAX(codigo), 0) + 1 FROM productos;
SELECT @codigo AS nuevo_id_producto;
GO

GO
create or alter procedure spu_id_nueva_venta
as
declare @id_venta int;
SELECT @id_venta = ISNULL(MAX(id_venta), 0) + 1 FROM ventas;
SELECT @id_venta AS nuevo_id_venta;
GO

GO
CREATE OR ALTER PROCEDURE spu_modificar_cliente
@id_cliente INT,
@razon_social NVARCHAR(60)
AS
UPDATE clientes SET razon_social = @razon_social WHERE id_cliente = @id_cliente
GO

GO
CREATE OR ALTER PROCEDURE spu_modificar_productos
@codigo INT,
@descripcion NVARCHAR(60),
@stock INT,
@precio DECIMAL(18,2)
as
UPDATE productos set
descripcion = @descripcion,
stock = @stock,
precio = @precio
WHERE codigo = @codigo;
GO

GO
CREATE OR ALTER PROCEDURE spu_modificar_venta
@id_venta INT,
@fecha DATETIME,
@id_cliente INT,
@importe DECIMAL,
@id_usuario INT
as
UPDATE ventas set
fecha = @fecha,
id_cliente = @id_cliente,
importe = @importe,
id_usuario = @id_usuario
where id_venta= @id_venta
GO

GO
create or alter procedure spu_mostrar_cliente
as
select*from clientes
GO

GO
CREATE OR ALTER PROCEDURE [dbo].[spu_mostrar_detalle_venta]
@id_venta INT
AS
BEGIN
    SELECT detalle_venta.codigo, productos.descripcion, detalle_venta.precio, cant, precio_r
    FROM detalle_venta
    INNER JOIN productos ON detalle_venta.codigo = productos.codigo WHERE id_venta = @id_venta
END
GO

GO
CREATE OR ALTER PROCEDURE spu_mostrar_productos
AS
BEGIN
    SELECT * FROM productos
END
GO

GO
create or alter procedure spu_mostrar_venta
as
select*from ventas
GO

GO
-- =========================== TRIGGERS ===========================
create or alter trigger baja_stock on detalle_venta
for insert
as
 UPDATE p
    SET p.stock = p.stock - i.cant
    FROM productos p
    INNER JOIN inserted i ON p.codigo = i.codigo;
GO

GO
create or alter trigger sube_stock on detalle_venta
for delete
as
 UPDATE p
    SET p.stock = p.stock + d.cant
    FROM productos p
    INNER JOIN deleted d ON p.codigo = d.codigo;
GO

GO
-- ==================== FIN DEL SCRIPT ====================
