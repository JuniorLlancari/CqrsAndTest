# CQRS Demo Application

Este repositorio contiene una aplicación de ejemplo que implementa arquitectura CQRS con .NET 8. Está organizada en varios proyectos que separan dominio, aplicación, persistencia, capas externas y una API web.

Proyectos principales
- `CQRS.Api` — API REST que expone los endpoints.
- `CQRS.Application` — Casos de uso, comandos, handlers y validaciones.
- `CQRS.Domain` — Entidades y eventos de dominio.
- `CQRS.Persistence` — Implementación del acceso a datos (DbContext y repositorios).
- `CQRS.External` — Integraciones externas (por ejemplo Application Insights).
- `CQRS.Common` — Constantes y utilidades compartidas.

Resumen funcional
La aplicación demuestra una implementación de CQRS con manejo de eventos de dominio, validación, manejo centralizado de excepciones y telemetría (Application Insights). Incluye ejemplos de comandos/handlers y controladores HTTP para manejar entidades como `Alumno` y `Curso`.

Requisitos
- .NET 8 SDK
- Terraform (para aprovisionar infraestructura remota)

Pasos para levantar la aplicación
1. Aprovisionar infraestructura 
   - Desde la carpeta donde estén tus archivos de Terraform ejecuta:
     - `terraform init`
     - `terraform plan`
     - `terraform apply` (revisar antes de confirmar)
   - Esto asumirá que tienes configuradas las credenciales necesarias para el proveedor (por ejemplo Azure).


2. Compilar y ejecutar localmente
   - Desde la raíz del repositorio:
     - `dotnet build` (o `dotnet build ./src/CQRS.Api/CQRS.Api.csproj`)
     - `dotnet run --project ./src/CQRS.Api/CQRS.Api.csproj`
   - La API por defecto se ejecutará en la URL que aparezca en la salida (ver `Properties/launchSettings.json` para puertos por defecto).



Observabilidad y manejo de errores
- La aplicación registra errores y envía métricas/eventos a Application Insights cuando está configurado.
- Existe un manejador global de excepciones `GlobalExceptionHandler` en `src/CQRS.Api/ExceptionHandlers` que genera respuestas estándar de error.

Consejos
- Revisa `src/CQRS.Application/Exceptions/ValidationException.cs` para entender el formato de errores de validación.

