# Vesta Logistics

Sistema **SaaS Multitenant** de control de inventarios y facturación, desarrollado en **ASP.NET Core MVC** con arquitectura N-Tier.

---

## Stack tecnológico

| Componente        | Tecnología                          |
|-------------------|-------------------------------------|
| Framework         | ASP.NET Core MVC                    |
| Lenguaje          | C# (.NET 10)                        |
| Base de datos     | SQL Server                          |
| ORM               | Entity Framework Core (Database First) |
| Persistencia      | Stored Procedures existentes       |

---

## Arquitectura (N-Tier)

La solución está organizada en cuatro proyectos de capas:

```
┌─────────────────────────────────────────────────────────┐
│  VestaLogistics.Web          (Presentación)             │
│  Controllers, Views, wwwroot, CSS institucional         │
└───────────────────────────┬────────────────────────────┘
                            │
┌───────────────────────────▼────────────────────────────┐
│  VestaLogistics.Business  (Lógica de negocio)           │
│  Servicios, validaciones, IVA/multas, TipoCambioService  │
└───────────────────────────┬────────────────────────────┘
                            │
┌───────────────────────────▼────────────────────────────┐
│  VestaLogistics.Data       (Acceso a datos)              │
│  DbContext, Repositorios, Stored Procedures              │
└───────────────────────────┬────────────────────────────┘
                            │
┌───────────────────────────▼────────────────────────────┐
│  VestaLogistics.Entities   (Modelos compartidos)         │
│  POCOs, IEntityWithEmpresa (multitenancy)               │
└─────────────────────────────────────────────────────────┘
```

- **VestaLogistics.Web**: UI, layout con identidad visual (Azul Vesta, Dorado, Teal), configuración de DI y tenant.
- **VestaLogistics.Business**: Reglas de negocio, cálculos y esqueleto del servicio de tipo de cambio (BCCR).
- **VestaLogistics.Data**: `VestaLogisticsDbContext`, Global Query Filter por `EmpresaID`, `IRepository<T>`, `EmpresaRepository` (SPs).
- **VestaLogistics.Entities**: Entidades que mapean tablas y la interfaz `IEntityWithEmpresa` para multitenancy.

Además, la solución incluye **VestaLogisticsDB** (proyecto de base de datos con tablas y Stored Procedures) y **App** (proyecto legacy si aplica).

---

## Requisitos previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB, Express o completo)
- Base de datos **VestaLogistics** creada y scripts de **VestaLogisticsDB** desplegados (esquemas, tablas, SPs)

---

## Configuración y ejecución

### 1. Clonar / abrir la solución

```bash
cd VestaLogistics
```

Abrir `VestaLogistics.slnx` en Visual Studio o en Cursor/VS Code.

### 2. Cadena de conexión

En **VestaLogistics.Web**, editar `appsettings.json` (o `appsettings.Development.json`) y ajustar la conexión a tu instancia de SQL Server:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=VestaLogistics;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

Ejemplo para SQL Server con usuario/contraseña:

```json
"DefaultConnection": "Server=.;Database=VestaLogistics;User Id=sa;Password=TuPassword;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

### 3. Compilar

```bash
dotnet build VestaLogistics.Web/VestaLogistics.Web.csproj
```

### 4. Ejecutar la aplicación web

```bash
dotnet run --project VestaLogistics.Web/VestaLogistics.Web.csproj
```

O desde Visual Studio: establecer **VestaLogistics.Web** como proyecto de inicio y pulsar F5.

La aplicación se abrirá en `https://localhost:5001` o `http://localhost:5000` (según `launchSettings.json`).

---

## Multitenancy

- El **Global Query Filter** en `VestaLogisticsDbContext` filtra automáticamente por `EmpresaID` en todas las entidades que implementan `IEntityWithEmpresa`.
- El tenant actual se resuelve con **ITenantContext**, implementado en Web como **TenantContext**:
  - Claim `EmpresaID` del usuario autenticado.
  - Cabecera HTTP `X-Tenant-ID`.
- Para que el filtro aplique correctamente, cada request debe tener un tenant definido (claims o header).

---

## Identidad visual (CSS)

En `VestaLogistics.Web/wwwroot/css/site.css` están definidas las variables:

| Nombre      | Hex       | Uso principal        |
|------------|-----------|----------------------|
| Azul Vesta | `#1A2F50` | Navbar, footer, títulos |
| Dorado     | `#C5A062` | Acentos, enlaces     |
| Teal       | `#337085` | Texto secundario, botones, bordes |

---

## API BCCR (tipo de cambio)

El servicio **TipoCambioService** en **VestaLogistics.Business** está preparado para integrar el API del Banco Central de Costa Rica. Actualmente devuelve valores placeholder. Próximos pasos:

- Configurar URL e indicadores del BCCR (ej. 317 compra, 318 venta).
- Inyectar `IHttpClientFactory` y consumir el servicio web del BCCR.

Documentación de referencia: [BCCR - Indicadores económicos](https://www.bccr.fi.cr/seccion-indicadores-economicos/servicio-web).

---

## Estructura de carpetas (resumen)

```
VestaLogistics/
├── README.md
├── VestaLogistics.slnx
├── VestaLogistics.Entities/       # POCOs, IEntityWithEmpresa
├── VestaLogistics.Data/           # DbContext, repositorios, SPs
├── VestaLogistics.Business/       # Servicios (TipoCambio, etc.)
├── VestaLogistics.Web/            # MVC, Views, wwwroot, DI
├── VestaLogisticsDB/              # Proyecto SQL (tablas, SPs)
└── App/                           # Proyecto legacy (opcional)
```

---

## Licencia

Uso interno / proyecto PR0005 - Inmobiliarios.
