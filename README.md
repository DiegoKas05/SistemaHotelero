# HOTEL BLUE

Este proyecto esta desarrollado en C# con Asp.NET. 
Esta desarrollado para la gestion de hotel. Permite gestionar la recepcion de habitacion y salida del hotel, 
Pedir servicio a la habitacion ya que cuenta con tienda de productos de alimentacion, 
Permite administrar el mantenimiento de habitaciones, Permite la gestion de empleados,
Permite generar reportes de recepcion de clientes y de los productos consumidos por el mismo.


## Tabla de contenido

- Descripción de proyecto 
- Requisitos técnicos
- Pasos para instalar y ejecutar el sistema.


## Requisitos 
- Instalar Visual Studio
	- Descargar de https://visualstudio.microsoft.com/es/downloads/
	- Instalar en windows	
- Instalar Sql server management
	Descargar de https://learn.microsoft.com/es-es/ssms/download-sql-server-management-studio-ssms
-	Instalar en windows
- Implementar y configurar la dependencia mssql1
- Crear archivo de configuracion para la base de datos appsettings.json


## Instalacion del sistema
Para ejecutarlo localmente debe hacer los siguientes
	

- Ir al repositorio  en Github https://github.com/Lisseth-Montoya/so-hotel-blue/tree/dev-vero y 
 descargar el repositorio utilizando Gitbash 

  ```
  git clone git@github.com:DiegoKas05/SistemaHotelero.git
  ```

 - Abril visual studio y cargar el proyecto SistemaHotelero
	
 - Configurar Archivo appsettings.json.
	- Agregar la conexion a SQL Server. Ejemplo:

```
  "ConexionSQL": "Server=DESKTOP-4FM0COK\\SQLEXPRESS;Database=HOTEL_BLUE;User ID=sa;Password=1234;Trusted_Connection=true;Encrypt=false;MultipleActiveResultSets=true",
  ```

 - Migrar la base de datos desde visual hacia SQL server 
	- Ir a visual studio y abrir Package Manager Console. En el menu hacer click en Tools -> NuGet Package Manager -> Package Manager Console y ejecutar los comandos a continuacion.
	
```
  add-migration hotel-blue
  update-database

  ```


- Verificar conección con SQL server 
	- Ir a SQL sever management -> Hacer click en conectar -> Ingresar usuario y contraseña
	- Una vez conectado ir a la seccion de Bases de Datos y buscar la base de datos con nombre HOTEL-BLUE
	- Verificar que la base de datos contiene todas las tablas segun la definicion del requerimiento del sistema
	
- Iniciar el Sistema
	- En visual studios ir al boton Iniciar en color verde
	- Se abrira una pagina en el navegador con el titulo del sistema Hotel Blue
	- Iniciar Session en el sistema utilizando su correo electronico y su contraseña
	- Si no cuenta con una cuenta en el sistema, debe registrarse haciendo uso del boton Registrarme
	- Una vez inicie sesion exitosamente en el sistema, podra ver un mensaje de bienvenida ¡Hola, veronica@gmail.com!
	- Tendra acceso a los diferentes modulos del sistema segun su rol y niveles de acceso.
	