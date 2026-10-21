# sys_comercio

Sistema de comercio en **C# WinForms (.NET Framework 4.8)** con **SQL Server** y la librería
de interfaz **Guna.UI2**.

Permite administrar clientes y productos, registrar ventas con su detalle y bajar/subir el
stock automáticamente según se cargan o eliminan líneas de venta.

---

## Requisitos

- Windows 10/11
- Visual Studio 2022 (o Build Tools) con el *developer pack* de **.NET Framework 4.8**
- **SQL Server Express** (instancia `SQLEXPRESS`)

> No hace falta instalar ningún paquete NuGet: el proyecto no usa librerías externas.

---

## Estructura del repositorio

```
sys_comercio.sln              Solución de Visual Studio
sys_comercio/                Proyecto WinForms (forms, Conexion.cs, App.config)
icons/                       Imágenes originales (las que se usan están embebidas en los .resx)
BD/Query.sql                 Script de creación de la base (tablas, SP y triggers)
BD/comercio.mdf              Base de datos adjunta (SQL Server)
BD/comercio_log.ldf          Log de la base adjunta
```

---

## Puesta en marcha

### 1. Crear la base de datos

Abrí **SQL Server Management Studio** y corré el script `BD/Query.sql`. Crea la base
`comercio` con sus tablas, procedimientos almacenados y triggers.

> También podés usar los archivos `BD/comercio.mdf` y `BD/comercio_log.ldf` (base adjunta)
> en lugar de correr el script.

### 2. Configurar la conexión a la base

La cadena de conexión **no está en el repositorio**. Para configurarla en tu máquina:

1. Copiá `sys_comercio/App.config.local.example` como `sys_comercio/App.config.local`
2. Completá los datos de tu servidor:

```xml
<add key="CadenaConexion" value="Data Source=TU_SERVIDOR\SQLEXPRESS;Initial Catalog=comercio;User ID=sa;Password=TU_CLAVE" />
```

`App.config.local` está en el `.gitignore`: **no se sube nunca**.

La app busca la cadena en este orden:

1. `App.config.local` (junto al `.exe`) → tu configuración real.
2. `App.config` (versionado) → placeholder sin clave, por si se corre sin configurar.

Si no encuentra ninguna, muestra un aviso indicando que falta la configuración.

Además, en el mismo `App.config.local` se puede indicar dónde se guardan los respaldos:

```xml
<add key="CarpetaBackup" value="C:\Final\Backup\" />
```

Si se deja vacía, los `.bak` van a la carpeta de backup por defecto de la instancia de
SQL Server. La app muestra la ruta del archivo generado al terminar el respaldo.

### 4. Compilar y ejecutar

`F5` desde Visual Studio, o:

```bash
msbuild sys_comercio.sln /t:Build /p:Configuration=Debug
```

---

## Base de datos

`BD/Query.sql` define:

| Tabla            | Contenido                                  |
|------------------|--------------------------------------------|
| `usuarios`       | Usuarios del sistema (login)               |
| `clientes`       | Razón social de los clientes               |
| `productos`      | Código, descripción, stock y precio        |
| `ventas`         | Cabecera de venta (fecha, cliente, total)  |
| `detalle_venta`  | Ítems de cada venta                        |

Procedimientos: `spu_alta_*`, `spu_baja_*`, `spu_modificar_*`, `spu_mostrar_*`,
`sp_login`, `spu_backup`, etc.

Triggers: `baja_stock` (descuenta stock al insertar una línea) y `sube_stock`
(devuelve el stock al eliminar una línea).

**Usuario de prueba:** `Matias` / `456` (según el `exec sp_login` del script).

---

## Notas

- La contraseña de los usuarios se guarda en texto plano en la tabla `usuarios`
  (es un TP académico, no usar así en producción).
- `spu_backup` respalda la base desde la que se llama (`comercio`) y devuelve la ruta
  del `.bak` generado. El nombre es `yyyyMMdd.bak`; si ya existe, se le agrega un
  correlativo (`_1`, `_2`, ...) para no pisarlo.
