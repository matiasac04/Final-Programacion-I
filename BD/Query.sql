create database comercio
go
use comercio
go

--TABLAS
create table usuarios(
id_usuario int primary key,
usuario nvarchar(15),
psw int
)

create table clientes(
id_cliente int primary key,
razon_social nvarchar(60)
)

create table productos(
codigo int primary key,
descripcion nvarchar(60),
stock int,
precio decimal(18,2)
)

create table ventas(
id_venta int primary key,
fecha datetime,
id_cliente int foreign key references clientes(id_cliente),
importe decimal(18,2),
id_usuario int foreign key references usuarios(id_usuario)
)

create table detalle_venta(
id_venta int foreign key references ventas(id_venta),
codigo int foreign key references productos(codigo),
precio decimal(18,2),
cant int,
precio_r decimal(18,2),
primary key(id_venta,codigo)
)

--SPU
--CLIENTES

create procedure spu_alta_cliente
@id_cliente int,
@razon_social nvarchar(60)
as
insert into clientes
values(@id_cliente, @razon_social)

create procedure spu_baja_cliente
@id_cliente int
as
delete from clientes
where id_cliente = @id_cliente

create procedure spu_modificar_cliente
@id_cliente int,
@razon_social nvarchar(60)
as
update clientes set razon_social = @razon_social where id_cliente = @id_cliente

drop procedure spu_mostrar_cliente
create procedure spu_mostrar_cliente
as
select*from clientes

--PROCUCTOS
create procedure spu_alta_productos
@codigo int,
@descripcion nvarchar(60),
@stock int,
@precio decimal(18,2)
as
insert into productos
values(@codigo, @descripcion, @stock, @precio)

create procedure spu_baja_productos
@codigo int
as
delete from productos where codigo = @codigo

create procedure spu_modificar_productos
@codigo int,
@descripcion nvarchar(60),
@stock int,
@precio decimal(18,2)
as
update productos set
descripcion = @descripcion,
stock = @stock,
precio = @precio
where codigo = @codigo;

create procedure spu_mostrar_productos
as
SELECT * FROM productos


--VENTAS

create procedure spu_alta_venta
@id_venta int,
@fecha datetime,
@id_cliente int,
@importe decimal,
@id_usuario int
as
insert into ventas
values(@id_venta, @fecha, @id_cliente, @importe, @id_usuario)

create procedure spu_modificar_venta
@id_venta int,
@fecha datetime,
@id_cliente int,
@importe decimal,
@id_usuario int
as
update ventas set
fecha = @fecha,
id_cliente = @id_cliente,
importe = @importe,
id_usuario = @id_usuario
where id_venta= @id_venta

create procedure spu_eliminar_venta
@id_venta int
as
delete from ventas where id_venta = @id_venta

create procedure spu_mostrar_venta
as
select*from ventas 

drop procedure 
create procedure spu_id_nueva_venta
as
declare @id_venta int;
SELECT @id_venta = ISNULL(MAX(id_venta), 0) + 1 FROM ventas;
SELECT @id_venta AS nuevo_id_venta;


exec spu_id_nueva_venta 1

--DETALLE VENTA

create procedure spu_mostrar_detalle_venta
@id_venta int
as
select descripcion, detalle_venta.precio, cant, precio_r from detalle_venta
inner join productos on detalle_venta.codigo = productos.codigo
where id_venta = @id_venta

create procedure spu_actualizar_venta
@id_venta int,
@id_cliente int,
@importe decimal(18,2)
as
update ventas
set id_cliente = @id_cliente,
importe = @importe
where id_venta = @id_venta


create procedure spu_cargar_detalleventa
@id_venta int,
@codigo int,
@precio decimal,
@cant int,
@precio_r decimal
as
insert into Detalle_Venta
values(@id_venta, @codigo, @precio, @cant, @precio_r);

create procedure spu_eliminar_detalleventa
@id_venta int
as
delete from detalle_venta where id_venta = @id_venta

--BACKUP

CREATE PROCEDURE spu_backup
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

--LOGIN
drop procedure sp_login
CREATE PROCEDURE sp_login
    @usuario NVARCHAR(50),
    @psw NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT id_usuario, usuario
    FROM usuarios
    WHERE usuario = @usuario AND psw = @psw;
END



exec sp_login @usuario = 'Matias', @psw = 456;

--TRIGGERS
create trigger baja_stock on detalle_venta
for insert
as
update productos set stock = stock - (select cant from inserted)
where codigo = (select codigo from inserted)


create trigger sube_stock on detalle_venta
for delete
as
update productos set stock = stock + (select cant from deleted)
where codigo = (select codigo from deleted)