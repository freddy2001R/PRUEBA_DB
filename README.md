Proyecto API de Gestión de DataSets y Procedimientos
Este repositorio contiene una API web desarrollada en ASP.NET Core para gestionar DataSets, Procedimientos, Usuarios y Roles, incluyendo autenticación mediante tokens JWT y autorización basada en roles.

Tareas Implementadas
Tarea 1: Obtener DataSets asociados a un UserID.

Endpoint: GET /api/Users/{userId}/DataSetsAssociated

Tarea 2: Crear nuevo DataSet.

Endpoint: POST /api/DataSets

Tarea 3: Obtener DataSets de un Procedure con tipo de Field.

Endpoint: GET /api/DataSets/ByProcedure/{procedureId}

Tarea 4: Crear un Procedure con autorización de rol Admin.

Endpoint: POST /api/Procedures

Guía Rápida de Configuración y Ejecución
1. Prerrequisitos
Visual Studio 2022 (o más reciente) con SDK de .NET compatible.

SQL Server LocalDB o instancia de SQL Server.

Paquetes NuGet: Microsoft.EntityFrameworkCore.SqlServer, Microsoft.AspNetCore.Authentication.JwtBearer, BCrypt.Net-Next.

2. Configuración Inicial
Abrir PRUEBA.sln en Visual Studio.

Verificar ConnectionStrings:DefaultConnection en appsettings.json.

Actualizar JwtSettings:Secret en appsettings.json con clave segura (32+ caracteres).

3. Preparar Base de Datos
Establecer GeneradorHashes como proyecto de inicio.

Ejecutarlo (Ctrl+F5) y copiar hashes para admin123 y user123.

Pegar hashes en PRUEBA/Data/ApplicationDbContext.cs para usuarios admin y user1.

En la Consola del Administrador de Paquetes, seleccionar PRUEBA y ejecutar:

powershell
Copiar
Editar
dotnet ef database drop --force
Add-Migration InitialSetup
Update-Database
4. Ejecutar la API
Establecer PRUEBA como proyecto de inicio.

Presionar F5 o Ctrl+F5 para iniciar.

Abrir Swagger UI en navegador (https://localhost:7038/swagger).

Si da 404, revisar que la URL sea /swagger.

5. Uso Básico en Swagger UI
POST /api/Auth/login con admin/admin123.

Copiar token de la respuesta 200 OK.

Si da error 500, verificar JwtSettings:Secret, hashes en ApplicationDbContext.cs y BCrypt.Net.BCrypt.Verify en AuthController.cs.

Clic en "Authorize" en Swagger UI.

Pegar token con prefijo Bearer (con espacio).

Los endpoints protegidos se habilitan para pruebas.
