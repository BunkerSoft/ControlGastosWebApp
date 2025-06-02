# ControlGastosWebApp modificacion 02062025

Aplicación ASP.NET Core MVC para la gestión de gastos personales y empresariales, con autenticación, control de fondos, presupuestos y reportes.

## Características principales
- Registro y autenticación de usuarios (Identity).
- Gestión de fondos monetarios.
- Registro de movimientos (ingresos/gastos) asociados a fondos y categorías.
- Control y seguimiento de presupuestos mensuales/anuales por tipo de gasto.
- Administración de categorías y tipos de gasto.
- Registro de depósitos a fondos.
- Reportes y visualización de datos (integración con DevExpress opcional).

## Estructura del proyecto
- **Controllers/**: Controladores MVC para cada entidad principal (Gasto, FondoMonetario, Presupuesto, etc.).
- **Models/**: Modelos de datos (Movimiento, FondoMonetario, Presupuesto, TipoGasto, ApplicationUser, etc.).
- **Data/**: Contexto de base de datos (`AppDbContext`) y clase de inicialización de datos (`SeedData`).
- **Views/**: Vistas Razor para CRUD y visualización de datos.
- **Services/**: Servicios de lógica de negocio (ej. PresupuestoService).
- **wwwroot/**: Archivos estáticos (CSS, JS, librerías).

## Requisitos
- .NET 8.0
- SQL Server (local o Azure SQL)
- Paquetes NuGet principales:
  - Microsoft.EntityFrameworkCore.SqlServer
  - Microsoft.AspNetCore.Identity.EntityFrameworkCore
  - DevExpress.AspNetCore.Core (opcional)
  - DevExpress.AspNetCore.Reporting (opcional)

## Configuración
1. **Cadena de conexión**: Edita `appsettings.json` con tu cadena de conexión SQL Server/Azure SQL.
2. **Migraciones y base de datos**:
   - Ejecuta `dotnet ef database update` para crear la base de datos.
3. **Usuario administrador**: Al iniciar la app, se crea un usuario admin por defecto (`admin@controlgastos.com` / `Admin123!`).

## Ejecución
```
dotnet run
```
La aplicación estará disponible en `https://localhost:5001` (o el puerto configurado).

## Notas sobre DevExpress
- Si no usas reportes DevExpress, puedes eliminar los paquetes y referencias relacionadas.
- Si usas reportes, asegúrate de tener el controlador `CustomWebDocumentViewerController` en la carpeta `Controllers`.

## Estructura de carpetas
```
ControlGastosWebApp/
├── Controllers/
├── Data/
├── Models/
├── Services/
├── Views/
├── wwwroot/
├── appsettings.json
├── ControlGastosWebApp.csproj
└── Program.cs
```

## Licencia
Proyecto de ejemplo para gestión de gastos. Uso libre para fines educativos o personales.
