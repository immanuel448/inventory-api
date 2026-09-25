# Inventory API

REST API desarrollada con **ASP.NET Core 8** para administrar productos de un sistema de inventario.

El proyecto implementa operaciones CRUD, validaciones, acceso a datos mediante Entity Framework Core y SQL Server, inyección de dependencias, manejo centralizado de excepciones y documentación mediante Swagger/OpenAPI.

## Tecnologías

* C#
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Swagger / OpenAPI
* Git / GitHub

## Características

* CRUD de productos.
* Validación de datos mediante Data Annotations.
* Entity Framework Core para acceso a datos.
* SQL Server como base de datos.
* Inyección de dependencias (DI).
* Arquitectura basada en Controllers, Services, DTOs y Models.
* Operaciones asíncronas con `async/await`.
* Eliminación lógica de productos mediante `IsActive`.
* Manejo centralizado de excepciones.
* Logging de errores.
* Documentación de endpoints mediante Swagger/OpenAPI.

## Estructura del proyecto

```text
InventoryApi
├── Controllers
│   └── ProductsController.cs
├── Services
│   ├── IProductService.cs
│   └── ProductService.cs
├── DTOs
│   ├── CreateProductDto.cs
│   ├── UpdateProductDto.cs
│   └── ProductDto.cs
├── Models
│   └── Product.cs
├── Data
│   └── AppDbContext.cs
├── Exceptions
│   └── ExceptionHandlingMiddleware.cs
├── Mappings
├── Program.cs
├── appsettings.json
└── InventoryApi.csproj
```

## Arquitectura

El proyecto utiliza una separación básica de responsabilidades:

```text
HTTP Request
     │
     ▼
ProductsController
     │
     ▼
IProductService
     │
     ▼
ProductService
     │
     ▼
AppDbContext
     │
     ▼
SQL Server
```

### Controllers

Reciben las peticiones HTTP y devuelven las respuestas correspondientes.

### Services

Contienen la lógica relacionada con los productos y las operaciones con Entity Framework Core.

### DTOs

Definen los datos que recibe y devuelve la API, evitando exponer directamente las entidades de base de datos como contratos de la API.

### Models

Representan las entidades utilizadas por Entity Framework Core.

### Data

Contiene el `AppDbContext`, encargado de la comunicación con SQL Server.

### Exceptions

Contiene el middleware encargado de gestionar excepciones no controladas de forma centralizada.

## Base de datos

La aplicación utiliza SQL Server y Entity Framework Core.

La cadena de conexión se encuentra en:

```text
appsettings.json
```

Ejemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=InventoryDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> Para un entorno real se recomienda utilizar una configuración segura para las credenciales y no almacenar información sensible directamente en el repositorio.

## Configuración

### 1. Clonar el repositorio

```bash
git clone <URL_DEL_REPOSITORIO>
```

### 2. Abrir el proyecto

Abrir:

```text
InventoryApi.sln
```

o el archivo:

```text
InventoryApi.csproj
```

### 3. Configurar SQL Server

Verificar que SQL Server esté disponible y que la cadena de conexión de `appsettings.json` corresponda al entorno local.

### 4. Aplicar las migraciones

Desde la consola de Package Manager de Visual Studio:

```powershell
Update-Database
```

También se pueden utilizar los comandos de Entity Framework Core CLI:

```bash
dotnet ef database update
```

### 5. Ejecutar la API

Desde Visual Studio ejecutar el proyecto con:

```text
https
```

Swagger se abrirá automáticamente en el navegador en el entorno de desarrollo.

## Endpoints

La API utiliza la ruta base:

```text
/api/Products
```

| Método | Endpoint             | Descripción                         |
| ------ | -------------------- | ----------------------------------- |
| GET    | `/api/Products`      | Obtiene todos los productos activos |
| GET    | `/api/Products/{id}` | Obtiene un producto por ID          |
| POST   | `/api/Products`      | Crea un producto                    |
| PUT    | `/api/Products/{id}` | Actualiza un producto               |
| DELETE | `/api/Products/{id}` | Desactiva un producto               |

## Ejemplo POST

```http
POST /api/Products
```

```json
{
  "name": "Teclado mecánico",
  "description": "Teclado mecánico compacto con conexión USB",
  "price": 899.99,
  "stock": 15
}
```

Respuesta:

```text
201 Created
```

## Ejemplo PUT

```http
PUT /api/Products/1
```

```json
{
  "name": "Teclado mecánico actualizado",
  "description": "Teclado mecánico compacto con conexión USB-C",
  "price": 999.99,
  "stock": 20
}
```

Respuesta:

```text
204 No Content
```

## Eliminación lógica

El endpoint `DELETE` no elimina físicamente el registro de la base de datos.

En su lugar, modifica:

```text
IsActive = false
```

Los productos desactivados dejan de aparecer en las consultas normales de productos activos.

## Validaciones

Los datos recibidos mediante `POST` y `PUT` cuentan con validaciones como:

* Nombre obligatorio.
* Nombre máximo de 150 caracteres.
* Descripción máxima de 500 caracteres.
* Precio mayor a 0.
* Stock igual o mayor a 0.

Las validaciones son procesadas automáticamente por ASP.NET Core mediante `[ApiController]`.

## Manejo de errores

Las excepciones no controladas se gestionan mediante un middleware centralizado:

```text
ExceptionHandlingMiddleware
```

Esto evita tener bloques `try/catch` repetidos en cada controlador y permite devolver una respuesta consistente cuando ocurre un error inesperado.

## Inyección de dependencias

La aplicación utiliza el sistema de Dependency Injection incluido en ASP.NET Core.

La dependencia principal sigue este flujo:

```text
ProductsController
        ↓
IProductService
        ↓
ProductService
        ↓
AppDbContext
```

Los servicios se registran en `Program.cs`.

## Documentación

La API utiliza **Swagger/OpenAPI** para documentar y probar los endpoints.

Los endpoints incluyen documentación XML para mostrar una descripción directamente en Swagger.

## Estado del proyecto

Proyecto desarrollado como práctica de desarrollo backend con **C# y .NET**, enfocado en la construcción de una API REST utilizando buenas prácticas básicas de separación de responsabilidades, acceso a datos, validación y manejo de errores.
