# 🏛️ Vesta Logistics

**Plataforma SaaS Multitenant de Control de Inventarios y Facturación Inmobiliaria**

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET_Core_MVC-0058B0?style=for-the-badge&logo=aspnet&logoColor=white)

---

# 📖 Resumen Ejecutivo

**Vesta Logistics** es una plataforma **SaaS empresarial** diseñada para resolver los desafíos de **trazabilidad de inventarios, proveeduría y facturación** dentro del sector de **construcción y bienes raíces**.

El sistema permite gestionar múltiples empresas desde una sola plataforma mediante una arquitectura **Multitenant**, garantizando **aislamiento total de datos por empresa**, trazabilidad de operaciones y una experiencia optimizada para equipos administrativos y personal de campo.

La solución está construida con **ASP.NET Core MVC, C#, SQL Server y Entity Framework Core**, siguiendo una arquitectura **N-Tier** y principios **SOLID**, lo que permite escalabilidad, mantenibilidad y alto rendimiento en operaciones críticas.

---

# 🎯 Objetivos del Sistema

El propósito principal de **Vesta Logistics** es:

- Centralizar la gestión de **inventarios de materiales de construcción**
- Automatizar el **proceso de facturación y compras**
- Mejorar la **trazabilidad logística**
- Reducir **errores humanos en cálculos financieros**
- Permitir la **gestión remota desde obras o proyectos**

---

# ✨ Características Principales

## 🏢 Arquitectura Multitenant

El sistema permite que **múltiples empresas utilicen la misma aplicación** sin mezclar datos.

Se implementa mediante:

- `EmpresaID`
- **Global Query Filters** en Entity Framework Core
- Interfaces como `IEntityWithEmpresa`

Esto garantiza **aislamiento total de datos entre empresas**.

---

## 📦 Control de Inventario en Tiempo Real

El sistema permite registrar:

- Entradas de inventario mediante **facturas de compra**
- Salidas de inventario hacia **proyectos de construcción**
- Control de stock disponible
- Historial de movimientos

Cada movimiento queda registrado para **auditoría completa del inventario**.

---

## 🧾 Facturación Automatizada

El sistema calcula automáticamente:

- **IVA (13%)**
- Subtotales
- Totales de factura
- Multas por entregas tardías

Esto se realiza mediante **reglas de negocio en C# (POO)** para evitar errores manuales.

---

## 💱 Facturación Multimoneda

Integración con el **Banco Central de Costa Rica (BCCR)** mediante servicio **SOAP**, permitiendo:

- Actualización diaria de:
  - Tipo de cambio compra
  - Tipo de cambio venta
- Facturación en:
  - CRC
  - USD

---

## 🔐 Seguridad y Control de Acceso

Implementación de **RBAC (Role-Based Access Control)** con:

- Gestión de usuarios
- Roles y permisos
- Encriptación de contraseñas (`PasswordHash`)
- Bitácoras de acceso
- Registro de eventos del sistema

---

# 💻 Stack Tecnológico

| Capa / Componente | Tecnología | Propósito |
|---|---|---|
| **Framework Base** | ASP.NET Core MVC | Estructura de aplicación web |
| **Lenguaje Backend** | C# (.NET 10) | Lógica de negocio y cálculos |
| **Base de Datos** | SQL Server | Persistencia de datos |
| **ORM** | Entity Framework Core | Mapeo objeto-relacional |
| **Persistencia Avanzada** | Stored Procedures | Optimización de consultas |
| **Frontend** | HTML5, CSS3, Bootstrap 5 | Interfaz responsiva |
| **API Externa** | BCCR SOAP API | Tipos de cambio |

---

# 🏗️ Arquitectura del Sistema (N-Tier)


┌─────────────────────────────────────────────────────────┐
│ VestaLogistics.Web (Presentación / UI) │
│ Controllers, Views, wwwroot, Inyección de Dependencias │
└───────────────────────────┬────────────────────────────┘
│
┌───────────────────────────▼────────────────────────────┐
│ VestaLogistics.Business (Lógica de Negocio / BLL) │
│ Servicios, validaciones, reglas financieras │
└───────────────────────────┬────────────────────────────┘
│
┌───────────────────────────▼────────────────────────────┐
│ VestaLogistics.Data (Acceso a Datos / DAL) │
│ DbContext, repositorios, stored procedures │
└───────────────────────────┬────────────────────────────┘
│
┌───────────────────────────▼────────────────────────────┐
│ VestaLogistics.Entities (Modelos / Entidades) │
│ POCO Models, IEntityWithEmpresa │
└─────────────────────────────────────────────────────────┘


---

# 🗂️ Estructura del Proyecto


VestaLogistics
│
├── VestaLogistics.Web
│ ├── Controllers
│ ├── Views
│ ├── wwwroot
│ └── Program.cs
│
├── VestaLogistics.Business
│ ├── Services
│ ├── BusinessRules
│ └── APIIntegrations
│
├── VestaLogistics.Data
│ ├── DbContext
│ ├── Repositories
│ └── StoredProcedures
│
├── VestaLogistics.Entities
│ ├── Models
│ └── Interfaces
│
└── Database
├── Tables
├── StoredProcedures
└── Scripts


---

# 🎨 Identidad Visual

La interfaz utiliza una paleta corporativa diseñada para transmitir **confianza y eficiencia logística**.

| Color | Código | Uso |
|------|------|------|
| Azul Vesta | `#1A2F50` | Navbar / títulos |
| Dorado | `#C5A062` | Detalles |
| Teal | `#337085` | Botones principales |
| Crema | `#F3EFE9` | Fondo |

---

# ⚙️ Requisitos del Sistema

Para ejecutar el proyecto localmente se requiere:

- .NET SDK **10.0**
- **Visual Studio 2022**
- **SQL Server**
- **SQL Server Management Studio**

---

# 🚀 Instalación

### 1️⃣ Clonar repositorio

```bash
git clone https://github.com/andyszccr/VestaLogistics.git
cd VestaLogistics
2️⃣ Crear la base de datos
CREATE DATABASE VestaLogisticsDB

Ejecutar posteriormente los scripts de:

tablas
stored procedures
datos iniciales
3️⃣ Configurar conexión

Editar appsettings.json

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=VestaLogisticsDB;Trusted_Connection=True;TrustServerCertificate=True"
}
4️⃣ Ejecutar aplicación
dotnet build
dotnet run
📊 Beneficios del Sistema

✔ Control total del inventario
✔ Automatización financiera
✔ Reducción de errores humanos
✔ Arquitectura SaaS escalable
✔ Sistema auditable
✔ Acceso desde obra o proyectos

🔮 Futuras Mejoras
API REST pública
Dashboard de analytics
Integración con Power BI
Aplicación móvil
Notificaciones en tiempo real
👨‍💻 Autor

Andy Zúñiga

Software Developer | Backend | Data Analytics

Tecnologías principales:

C#
.NET
SQL Server
Power BI
Arquitectura de Software
