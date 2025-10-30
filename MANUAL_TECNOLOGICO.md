# Manual Tecnico  - Sistema de Control de Multas Ciudadanas

## 1. Información General del Proyecto

### 1.1 Descripción
Sistema backend para la gestión y control de multas ciudadanas, desarrollado con arquitectura multicapa en .NET 8. El sistema permite administrar infracciones ciudadanas, usuarios, pagos, acuerdos de pago, y genera reportes de inspectoría.

### 1.2 Tecnologías Principales
- **Framework**: .NET 8.0
- **Lenguaje**: C#
- **Arquitectura**: Clean Architecture / N-Capas
- **Bases de Datos**: SQL Server, PostgreSQL, MySQL (soporte multi-base de datos)
- **ORM**: Entity Framework Core 9.0.4
- **Autenticación**: JWT (JSON Web Tokens)
- **Validación**: FluentValidation 12.0.0
- **Mapeo de Objetos**: AutoMapper 14.0.0
- **Documentación API**: Swagger/OpenAPI
- **Contenedores**: Docker

---

## 2. Arquitectura del Sistema

### 2.1 Estructura de Capas

El proyecto sigue una arquitectura en capas claramente definida:

```
taller/
├── Web/                    # Capa de Presentación (API REST)
├── Business/              # Capa de Lógica de Negocio
├── Data/                  # Capa de Acceso a Datos
├── Entity/                # Capa de Entidades y DTOs
├── Utilities/             # Utilidades y Helpers
├── Helpers/               # Funciones auxiliares
├── Template/              # Plantillas (emails, PDFs)
├── Templates/             # Plantillas adicionales
├── Stategy/              # Patrones de estrategia
└── ControlDeComparendo.Tests/  # Pruebas unitarias
```

### 2.2 Proyectos Adicionales

```
Merge_Back/
├── taller/               # Sistema principal
├── Gateway/              # API Gateway
└── PublicAPI/            # API pública
```

---

## 3. Capa Web (Presentación)

### 3.1 Configuración del Proyecto

**Archivo**: [Web.csproj](Proyecto-BACK/Merge_Back/taller/Web/Web.csproj)

**Paquetes NuGet principales**:
- `Microsoft.AspNetCore.Authentication.JwtBearer` 8.0.15
- `Microsoft.AspNetCore.Authentication.Google` 8.0.15
- `FluentValidation.AspNetCore` 11.3.1
- `Swashbuckle.AspNetCore` 6.6.2
- `Microsoft.EntityFrameworkCore.SqlServer` 9.0.4
- `Npgsql.EntityFrameworkCore.PostgreSQL` 9.0.4
- `Pomelo.EntityFrameworkCore.MySql` 9.0.0

### 3.2 Configuración de la Aplicación

**Archivo**: [Program.cs](Proyecto-BACK/Merge_Back/taller/Web/Program.cs)

**Servicios configurados**:
1. Controllers y Swagger
2. FluentValidation
3. Servicios de aplicación personalizados
4. Configuración JWT
5. Base de datos dinámica (multi-provider)
6. Autenticación y autorización
7. CORS
8. Caché en memoria

**Middleware Pipeline**:
1. Archivos estáticos
2. Swagger (Dev/Prod)
3. CORS
4. Autenticación
5. Autorización
6. Controladores
7. Migraciones automáticas

### 3.3 Configuración (appsettings.json)

**Archivo**: [appsettings.json](Proyecto-BACK/Merge_Back/taller/Web/appsettings.json)

**Configuraciones clave**:

#### Bases de Datos
```json
"MigrationProvider": "SqlServer",
"ConnectionStrings": {
  "SqlServer": "Server=localhost,1433;Database=controlComparendo;...",
  "Postgres": "Host=localhost;Port=5433;Database=controlComparendo;...",
  "MySql": "Server=127.0.0.1;Port=3307;Database=controlComparendo;..."
}
```

#### JWT
```json
"Jwt": {
  "Issuer": "WebCDCP",
  "Audience": "WebCDCP",
  "AccessTokenExpirationMinutes": 15,
  "RefreshTokenExpirationDays": 7
}
```

#### Sesiones de Usuario
```json
"Auth": {
  "IdleMinutes": 1,
  "AbsoluteMinutes": 120
}
```

#### reCAPTCHA
```json
"Recaptcha": {
  "SecretKey": "6LcEs7grAAAAAPaoAy-k5kxlpNhWfsEihOph6bJ_",
  "MinScore": 0.5
}
```

#### SMTP (Correo electrónico)
```json
"SmtpSettings": {
  "Host": "smtp.gmail.com",
  "Port": 587,
  "EnableSsl": true,
  "Email": "camiloandreslosada901@gmail.com"
}
```

#### Scheduler (Tareas programadas)
```json
"Scheduler": {
  "TzId": "America/Bogota",
  "Hour": 16,
  "Minute": 21
}
```

#### Acuerdos de Pago
```json
"PaymentAgreementInterestOptions": {
  "MonthlyRate": 0.02,
  "GracePeriodDays": 30,
  "DailyDivisor": 30
}
```

### 3.4 Controladores

**Ubicación**: [Web/Controllers/](Proyecto-BACK/Merge_Back/taller/Web/Controllers/)

#### Controladores Principales

1. **AuthController.cs** - Autenticación
2. **LoginController.cs** - Login y gestión de sesiones
3. **TouristicAttractionsController.cs** - Atracciones turísticas
4. **VerificationController.cs** - Verificaciones

#### Controladores de Entidades
**Ubicación**: [Web/Controllers/Implements/Entities/](Proyecto-BACK/Merge_Back/taller/Web/Controllers/Implements/)

- Gestión de documentos de infracción
- Gestión de tipos de infracción
- Gestión de pagos
- Gestión de acuerdos de pago
- Gestión de usuarios
- Gestión de notificaciones

#### Controladores de Seguridad
**Ubicación**: [Web/Controllers/Implements/Security/](Proyecto-BACK/Merge_Back/taller/Web/Controllers/Implements/)

- Gestión de usuarios
- Gestión de roles
- Gestión de permisos
- Gestión de módulos y formularios

---

## 4. Capa Business (Lógica de Negocio)

### 4.1 Configuración del Proyecto

**Archivo**: [Business.csproj](Proyecto-BACK/Merge_Back/taller/Business/Business.csproj)

**Paquetes principales**:
- `AutoMapper` 14.0.0
- `FluentValidation` 12.0.0
- `Google.Apis.Auth` 1.69.0
- `Microsoft.Playwright` 1.54.0 (automatización web)

**Referencias de proyecto**:
- Data
- Entity
- Helpers
- Template
- Utilities

### 4.2 Servicios de Negocio

#### Servicios de Entidades
**Ubicación**: [Business/Services/Entities/](Proyecto-BACK/Merge_Back/taller/Business/Services/Entities/)

1. **DocumentInfractionServices.cs** - Gestión de documentos de infracciones
2. **FineCalculationDetailService.cs** - Cálculo de detalles de multas
3. **InspectoraReportService.cs** - Reportes de inspectoría
4. **PaymentAgreementServices.cs** - Acuerdos de pago e intereses
5. **TypeInfractionService.cs** - Tipos de infracciones
6. **TypePaymentServices.cs** - Tipos de pago
7. **UserInfractionServices.cs** - Infracciones de usuarios
8. **UserNotificationService.cs** - Notificaciones a usuarios
9. **ValueSmldvService.cs** - Valores de SMLDV

#### Servicios de Seguridad
**Ubicación**: [Business/Services/Security/](Proyecto-BACK/Merge_Back/taller/Business/Services/Security/)

1. **AuthService.cs** - Autenticación (login, registro, Google Auth)
2. **AuthSessionService.cs** - Gestión de sesiones de usuario
3. **UserService.cs** - CRUD de usuarios
4. **PersonService.cs** - Gestión de personas
5. **RolService.cs** - Gestión de roles
6. **RolUserService.cs** - Asignación de roles a usuarios
7. **RolFormPermissionService.cs** - Permisos de formularios por rol
8. **FormService.cs** - Gestión de formularios
9. **ModuleService.cs** - Gestión de módulos
10. **FormModuleService.cs** - Relación formularios-módulos
11. **PermissionService.cs** - Gestión de permisos


#### Servicios PDF
**Ubicación**: [Business/Services/PDF/](Proyecto-BACK/Merge_Back/taller/Business/Services/PDF/)

Generación de documentos PDF para el sistema.

---

## 5. Capa Data (Acceso a Datos)

### 5.1 Configuración del Proyecto

**Archivo**: [Data.csproj](Proyecto-BACK/Merge_Back/taller/Data/Data.csproj)

**Paquetes principales**:
- `AutoMapper` 14.0.0
- `Microsoft.EntityFrameworkCore` 9.0.4
- `Microsoft.EntityFrameworkCore.SqlServer` 9.0.4
- `Npgsql.EntityFrameworkCore.PostgreSQL` 9.0.4

**Referencia de proyecto**:
- Entity
- Utilities

### 5.2 Estructura

```
Data/
├── Interfaces/         # Interfaces de repositorios
├── Repository/        # Implementación de repositorios
└── Services/          # Servicios de datos
```

### 5.3 Patrón Repository

El proyecto utiliza el patrón Repository para abstraer el acceso a datos, permitiendo:
- Desacoplamiento de la lógica de negocio
- Facilidad para cambiar el proveedor de base de datos
- Mejor testabilidad

---

## 6. Capa Entity (Entidades y DTOs)

### 6.1 Configuración del Proyecto

**Archivo**: [Entity.csproj](Proyecto-BACK/Merge_Back/taller/Entity/Entity.csproj)

**Paquetes principales**:
- `Microsoft.EntityFrameworkCore` 9.0.4
- `Microsoft.AspNetCore.Authentication` 2.3.0
- `Microsoft.AspNetCore.Http.Abstractions` 2.3.0
- `Newtonsoft.Json` 13.0.3
- `Pomelo.EntityFrameworkCore.MySql` 9.0.0

### 6.2 Estructura

```
Entity/
├── Domain/
│   ├── Enums/          # Enumeraciones
│   ├── Interfaces/     # Interfaces de dominio
│   └── Models/         # Modelos de dominio
│       ├── Base/       # Modelos base
│       └── Implements/ # Implementaciones
│           ├── Entities/        # Entidades de negocio
│           └── ModelSecurity/   # Entidades de seguridad
├── DTOs/               # Data Transfer Objects
├── ConfigurationsBase/ # Configuraciones de EF Core
├── Migrations/         # Migraciones de base de datos
├── Init/              # Datos iniciales
└── Infrastructure/     # Infraestructura de datos
```

### 6.3 Enumeraciones

**Ubicación**: [Entity/Domain/Enums/](Proyecto-BACK/Merge_Back/taller/Entity/Domain/Enums/)

1. **DatabaseType.cs** - Tipos de base de datos (SqlServer, PostgreSQL, MySql)
2. **DeleteType.cs** - Tipos de eliminación (lógica/física)
3. **EstadoMulta.cs** - Estados de multa
4. **GetAllType.cs** - Tipos de consulta
5. **TipoUsuario.cs** - Tipos de usuario

### 6.4 Entidades Principales

#### Entidades de Negocio
**Ubicación**: [Entity/Domain/Models/Implements/Entities/](Proyecto-BACK/Merge_Back/taller/Entity/Domain/Models/Implements/)

1. **DocumentInfraction** - Documentos de infracciones
2. **TypeInfraction** - Tipos de infracciones
3. **UserInfraction** - Infracciones de usuarios
4. **TypePayment** - Tipos de pago
5. **PaymentAgreement** - Acuerdos de pago
6. **FineCalculationDetail** - Detalles de cálculo de multas
7. **InspectoraReport** - Reportes de inspectoría
8. **AddFines** - Adición de multas

#### Entidades de Seguridad
**Ubicación**: [Entity/Domain/Models/Implements/ModelSecurity/](Proyecto-BACK/Merge_Back/taller/Entity/Domain/Models/Implements/)

1. **User** - Usuarios del sistema
2. **Person** - Personas
3. **Rol** - Roles
4. **RolUser** - Relación Usuario-Rol
5. **Module** - Módulos del sistema
6. **Form** - Formularios
7. **FormModule** - Relación Formulario-Módulo
8. **Permission** - Permisos
9. **RolFormPermission** - Permisos por rol y formulario

### 6.5 Modelos Base

**BaseModel** y **BaseModelGeneric**: Proporcionan propiedades comunes como:
- Id
- CreatedAt
- UpdatedAt
- DeletedAt (para borrado lógico)
- State

### 6.6 Interfaces de Dominio

1. **IApplicationDbContext** - Interfaz del contexto de base de datos
2. **IAuditService** - Servicio de auditoría
3. **IDbContextFactory** - Fábrica de contextos de base de datos
4. **IHasId** - Interface para entidades con Id
5. **ISupportLogicalDelete** - Soporte para borrado lógico

---

## 7. Capa Utilities

### 7.1 Estructura

```
Utilities/
├── Custom/           # Utilidades personalizadas
└── Exceptions/       # Excepciones personalizadas
```

### 7.2 Funcionalidades

- Manejo de excepciones personalizadas
- Utilidades de validación
- Helpers de formato y conversión
- Constantes del sistema

---

## 8. Capa Helpers

Funciones auxiliares para:
- Operaciones comunes
- Formateo de datos
- Conversiones
- Validaciones adicionales

---

## 9. Capa Template

Gestión de plantillas para:
- Emails (notificaciones, verificación, recuperación de contraseña)
- PDFs (comparendos, reportes, acuerdos de pago)
- Reportes del sistema

---

## 10. Seguridad

### 10.1 Autenticación JWT

**Configuración**:
- Issuer: WebCDCP
- Audience: WebCDCP
- Access Token: 15 minutos
- Refresh Token: 7 días

### 10.2 Gestión de Sesiones

**Archivo**: [AuthSessionService.cs](Proyecto-BACK/Merge_Back/taller/Business/Services/Security/AuthSessionService.cs)

**Características**:
- Control de sesiones activas por usuario
- Timeout de inactividad: 1 minuto (configurable)
- Timeout absoluto: 120 minutos
- Validación de tokens

### 10.3 OAuth 2.0 - Google

Integración con Google Authentication mediante:
- `Microsoft.AspNetCore.Authentication.Google`
- `Google.Apis.Auth`

### 10.4 reCAPTCHA

Protección contra bots con Google reCAPTCHA v3:
- Score mínimo: 0.5
- Validación en endpoints críticos

### 10.5 CORS

Configuración de orígenes permitidos:
- Frontend: http://localhost:4200

---

## 11. Base de Datos

### 11.1 Soporte Multi-Base de Datos

El sistema soporta 3 proveedores de bases de datos:

1. **SQL Server** (puerto 1433)
2. **PostgreSQL** (puerto 5433)
3. **MySQL** (puerto 3307)

### 11.2 Migraciones

**Estrategia**: Migraciones automáticas al iniciar la aplicación

**Configuración**:
```json
"MigrateOnStartupTargets": ["SqlServer", "Postgres", "MySql"]
```

**Proveedor por defecto**: SqlServer

### 11.3 Entity Framework Core

**Versión**: 9.0.4

**Características utilizadas**:
- Code First
- Migrations
- Fluent API
- Data Annotations
- Change Tracking
- Lazy/Eager Loading

---

## 12. Patrones de Diseño Utilizados

### 12.1 Repository Pattern
Abstracción del acceso a datos en la capa Data.

### 12.2 Strategy Pattern
**Carpeta**: [Stategy/](Proyecto-BACK/Merge_Back/taller/Stategy/)

Implementación de diferentes estrategias de negocio.

### 12.3 Dependency Injection
Inyección de dependencias nativa de .NET Core en todos los servicios.

### 12.4 Unit of Work
Gestión transaccional en las operaciones de base de datos.

### 12.5 DTO Pattern
Separación entre modelos de dominio y objetos de transferencia de datos.

### 12.6 Factory Pattern
Creación dinámica de contextos de base de datos según el proveedor.

---

## 13. Integración de Servicios Externos



### 13.1 SMTP - Gmail

Envío de correos electrónicos:
- Notificaciones
- Recuperación de contraseña
- Confirmación de registro
- Alertas de pagos





---

## 14. Validaciones

### 14.1 FluentValidation

**Versión**: 12.0.0

**Validadores implementados**:

1. **DocumentInfraction Validator**
   - Validación de documentos de infracciones

2. **InspectoraReport Validator**
   - Validación de reportes de inspectoría

**Ubicación**: Business/validaciones/

**Características**:
- Validaciones fluidas y legibles
- Mensajes personalizados
- Validaciones asíncronas
- Validaciones condicionales

---

## 15. Mapeo de Objetos

### 15.1 AutoMapper

**Versión**: 14.0.0

**Ubicación**: [Web/AutoMapper/](Proyecto-BACK/Merge_Back/taller/Web/AutoMapper/)

**Perfiles de mapeo**:
- Entity → DTO
- DTO → Entity
- Entity → ViewModel
- Relaciones complejas

---

## 16. Funcionalidades del Sistema

### 16.1 Gestión de Infracciones

1. Registro de comparendos
2. Tipos de infracciones
3. Documentos adjuntos
4. Estados de multas
5. Cálculo de valores (SMLDV)

### 16.2 Gestión de Pagos

1. Tipos de pago
2. Acuerdos de pago
3. Cálculo de intereses (2% mensual)
4. Descuentos por pronto pago
5. Período de gracia (30 días)

### 16.3 Gestión de Usuarios

1. Registro y autenticación
2. Roles y permisos
3. Sesiones activas
4. Perfil de usuario
5. Notificaciones

### 16.4 Reportes

1. Reportes de inspectoría
2. Reportes de multas
3. Reportes de pagos
4. Estadísticas del sistema

### 16.5 Sistema de Permisos

**Modelo**: RBAC (Role-Based Access Control)

**Niveles**:
- Usuario → Rol(es)
- Rol → Permisos sobre Formularios
- Formularios → Módulos

---

## 17. Middleware

### 17.1 Middleware Implementados

**Ubicación**: [Web/Middleware/](Proyecto-BACK/Merge_Back/taller/Web/Middleware/)

Posibles middlewares personalizados:
- Validación de sesiones
- Logging
- Manejo de excepciones
- Rate limiting

---

## 18. Workers y Background Services

### 18.1 WebBackgroundService

**Ubicación**: [Web/WebBackgroundService/](Proyecto-BACK/Merge_Back/taller/Web/WebBackgroundService/)

**Tareas programadas**:
- Verificación de acuerdos de pago vencidos
- Cálculo automático de intereses
- Envío de notificaciones programadas
- Horario configurado: 16:21 (America/Bogota)

---

## 19. Pruebas

### 19.1 Proyecto de Pruebas

**Nombre**: ControlDeComparendo.Tests

**Ubicación**: [ControlDeComparendo.Tests/](Proyecto-BACK/Merge_Back/taller/ControlDeComparendo.Tests/)

**Framework de pruebas**: xUnit / NUnit / MSTest

**Alcance**:
- Pruebas unitarias de servicios
- Pruebas de validadores
- Pruebas de lógica de negocio
- Mocks de repositorios

---

## 20. Docker

### 20.1 Dockerfile

**Ubicación**: [Web/Dockerfile](Proyecto-BACK/Merge_Back/taller/Web/Dockerfile)

**Características**:
- Target OS: Linux
- Multi-stage build
- Optimización de capas

### 20.2 Docker Ignore

**Archivo**: .dockerignore

Exclusión de archivos innecesarios en la imagen Docker.

---

## 21. Swagger / OpenAPI

### 21.1 Configuración

**Disponible en**: `/swagger`

**Versión de API**: v1

**Entornos activos**: Development y Production

**Características**:
- Documentación automática de endpoints
- Pruebas interactivas
- Esquemas de modelos
- Autenticación JWT integrada

---

## 22. Infraestructura

### 22.1 Servicios de Infraestructura

**Ubicación**: [Web/Infrastructure/](Proyecto-BACK/Merge_Back/taller/Web/Infrastructure/)

Configuración de:
- DbContext Factory
- Migraciones automáticas
- Conexión a múltiples bases de datos
- Caché

---

## 23. Configuraciones Personalizadas

### 23.1 Web Configurations

**Ubicación**: [Web/Configurations/](Proyecto-BACK/Merge_Back/taller/Web/Configurations/)

Extensiones de configuración para:
- Servicios de aplicación
- CORS
- Autenticación JWT
- Base de datos
- Validadores

---

## 24. Flujo de Datos

### 24.1 Request Pipeline

```
HTTP Request
    ↓
Controller (Web)
    ↓
Validator (FluentValidation)
    ↓
Service (Business)
    ↓
Repository (Data)
    ↓
DbContext (Entity)
    ↓
Database
    ↓
Entity
    ↓
AutoMapper (DTO)
    ↓
HTTP Response
```

---

## 25. Modelos de Datos Principales

### 25.1 Sistema de Infracciones

**UserInfraction** (Infracción de Usuario)
- Id del usuario
- Tipo de infracción
- Fecha
- Estado
- Valor SMLDV
- Documentos asociados

**DocumentInfraction** (Documento de Infracción)
- Referencia a UserInfraction
- Tipo de documento
- Ruta del archivo
- Fecha de carga

**TypeInfraction** (Tipo de Infracción)
- Código
- Descripción
- Valor base en SMLDV

**FineCalculationDetail** (Detalle de Cálculo de Multa)
- Valor base
- Descuentos aplicados
- Intereses calculados
- Total a pagar

### 25.2 Sistema de Pagos

**PaymentAgreement** (Acuerdo de Pago)
- Usuario
- Infracción
- Número de cuotas
- Tasa de interés (2% mensual)
- Fecha de inicio
- Estado

**TypePayment** (Tipo de Pago)
- Efectivo
- Transferencia
- Tarjeta
- Acuerdo de pago

### 25.3 Sistema de Seguridad

**User** (Usuario)
- Username
- Email
- Password (hasheado)
- Estado
- Roles asociados

**Person** (Persona)
- Datos personales
- Relación con User

**Rol** (Rol)
- Nombre
- Descripción
- Permisos

**RolFormPermission** (Permiso)
- Rol
- Formulario
- Permisos (CRUD)

---

## 26. Estados del Sistema

### 26.1 Estados de Multa (EstadoMulta)

1. **Pendiente**: Multa registrada, pendiente de pago
2. **Pagada**: Multa completamente pagada
3. **En Acuerdo**: Con acuerdo de pago activo
4. **Vencida**: Pasó el período de gracia sin pago
5. **Coactivo**: En proceso coactivo

---

## 27. Lógica de Negocio Crítica

### 27.1 Cálculo de Intereses

**Servicio**: PaymentAgreementServices

**Fórmula**:
- Tasa mensual: 2%
- Días de gracia: 30
- Divisor diario: 30
- Interés = Valor * (0.02 / 30) * días_mora

### 27.2 Descuentos

**Servicio**: DiscountService

**Reglas**:
- Pronto pago: Descuento según días desde infracción
- Pago total: Mayor descuento
- Acuerdos de pago: Sin descuento de pronto pago

### 27.3 Generación de Reportes

**Servicio**: InspectoraReportService

**Tipos**:
- Reportes diarios
- Reportes mensuales
- Reportes por usuario
- Reportes por tipo de infracción

---

## 28. Notificaciones

### 28.1 Canales

1. **Email** (SMTP)
   - Confirmación de registro
   - Recuperación de contraseña
   - Notificaciones de pagos
   - Recordatorios de vencimiento
   - Alertas administrativas

### 28.2 UserNotificationService

Gestión de notificaciones a usuarios dentro del sistema.

---

## 29. Consideraciones de Seguridad

### 29.1 Buenas Prácticas Implementadas

1. Contraseñas hasheadas
2. JWT con expiración
3. Refresh tokens
4. HTTPS redirection
5. CORS configurado
6. reCAPTCHA en formularios
7. Validación de entrada (FluentValidation)
8. SQL Injection protection (EF Core)
9. Control de sesiones
10. Borrado lógico de datos


---

## 30. Despliegue

### 30.1 Prerrequisitos

1. .NET 8.0 SDK
2. Una de las siguientes bases de datos:
   - SQL Server 2019+
   - PostgreSQL 13+
   - MySQL 8+
3. Docker (opcional)

### 30.2 Configuración Inicial

1. Clonar repositorio
2. Configurar connection strings en appsettings.json
3. Configurar secretos SMTP, JWT, reCAPTCHA
4. Ejecutar migraciones: `dotnet ef database update`
5. Ejecutar aplicación: `dotnet run --project Web`

### 30.3 Variables de Entorno

Configurar en el servidor:
- `ASPNETCORE_ENVIRONMENT`
- Connection strings
- Claves de API
- Configuración SMTP

### 30.4 Docker Compose

Posible configuración de servicios:
- API (.NET)
- SQL Server
- PostgreSQL
- MySQL
- Redis (caché)
- Nginx (reverse proxy)

---

## 31. Mantenimiento

### 31.1 Logs

Implementar logging con:
- Consola (Development)
- Archivos (Production)
- Application Insights / Seq (Monitoreo)

### 31.2 Backups

**Base de datos**:
- Backups diarios automatizados
- Retención de 30 días
- Pruebas de restauración mensuales

**Archivos**:
- Documentos de infracciones
- Reportes generados
- Logs del sistema

### 31.3 Monitoreo

**Métricas clave**:
- Tiempo de respuesta de API
- Tasa de errores
- Uso de CPU/Memoria
- Conexiones a BD
- Tamaño de cola de trabajos

---

## 32. Diagrama de Arquitectura

```
┌─────────────────────────────────────────────────────┐
│                  Cliente (Frontend)                  │
│              http://localhost:4200                   │
└──────────────────────┬──────────────────────────────┘
                       │ HTTP/HTTPS
                       │ JWT Token
                       ↓
┌─────────────────────────────────────────────────────┐
│                   API Gateway                        │
│           (Opcional - API/API.csproj)                │
└──────────────────────┬──────────────────────────────┘
                       │
                       ↓
┌─────────────────────────────────────────────────────┐
│              Web Layer (Controllers)                 │
│  - AuthController                                    │
│  - LoginController                                   │
│  - Entities Controllers                              │
│  - Security Controllers                              │
│                                                       │
│  Middleware: Auth, CORS, Validation                  │
└──────────────────────┬──────────────────────────────┘
                       │
                       ↓
┌─────────────────────────────────────────────────────┐
│           Business Layer (Services)                  │
│  - AuthService                                       │
│  - PaymentAgreementServices                          │
│  - UserInfractionServices                            │
│  - DiscountService                                   │
│  - PDF Generation                                    │
│  - Email Service                                     │
└──────────────────────┬──────────────────────────────┘
                       │
                       ↓
┌─────────────────────────────────────────────────────┐
│           Data Layer (Repositories)                  │
│  - Repository Pattern                                │
│  - Unit of Work                                      │
└──────────────────────┬──────────────────────────────┘
                       │
                       ↓
┌─────────────────────────────────────────────────────┐
│           Entity Layer (EF Core)                     │
│  - DbContext Factory                                 │
│  - Entities                                          │
│  - Configurations                                    │
└──────────────────────┬──────────────────────────────┘
                       │
         ┌─────────────┼─────────────┐
         │             │             │
         ↓             ↓             ↓
   ┌──────────┐  ┌──────────┐  ┌──────────┐
   │SQL Server│  │PostgreSQL│  │  MySQL   │
   │ :1433    │  │  :5433   │  │  :3307   │
   └──────────┘  └──────────┘  └──────────┘

Servicios Externos:
┌──────────────┐  ┌──────────────┐
│ Google Auth  │  │ Gmail SMTP   │
└──────────────┘  └──────────────┘
```

---

## 33. Endpoints Principales

### 33.1 Autenticación

```
POST   /api/auth/login
POST   /api/auth/register
POST   /api/auth/refresh-token
POST   /api/auth/logout
POST   /api/auth/forgot-password
POST   /api/auth/reset-password
POST   /api/auth/google-login
```

### 33.2 Usuarios

```
GET    /api/users
GET    /api/users/{id}
POST   /api/users
PUT    /api/users/{id}
DELETE /api/users/{id}
```

### 33.3 Infracciones

```
GET    /api/infractions
GET    /api/infractions/{id}
POST   /api/infractions
PUT    /api/infractions/{id}
DELETE /api/infractions/{id}
GET    /api/infractions/types
GET    /api/infractions/user/{userId}
```

### 33.4 Pagos

```
GET    /api/payments
POST   /api/payments
GET    /api/payment-agreements
POST   /api/payment-agreements
GET    /api/payment-agreements/{id}/calculate
```

### 33.5 Reportes

```
GET    /api/reports/inspector
GET    /api/reports/fines
GET    /api/reports/payments
POST   /api/reports/generate
```

---

## 34. Conclusiones

### 34.1 Fortalezas del Proyecto

1.  Arquitectura limpia y bien estructurada
2.  Soporte multi-base de datos
3.  Seguridad robusta (JWT, reCAPTCHA, sesiones)
4.  Validaciones con FluentValidation
5.  Documentación con Swagger
6.  Patrones de diseño apropiados
7.  Separación de responsabilidades
8.  Escalabilidad horizontal
9.  Preparado para Docker
10.  Pruebas unitarias incluidas

### 34.2 Tecnologías Modernas

- .NET 8 (LTS)
- Entity Framework Core 9
- JWT Authentication
- AutoMapper
- FluentValidation
- Swagger/OpenAPI
- Multi-database support

### 34.3 Casos de Uso

Este sistema es ideal para:
- Gestión municipal de multas ciudadanas
- Control de infracciones de convivencia
- Gestión de acuerdos de pago
- Reportes de inspectoría
- Notificaciones automatizadas
- Sanciones administrativas

---

## 35. Contacto y Soporte

**Proyecto**: Sistema de Control de Multas Ciudadanas (CDCP)

**Versión de Solución**: Visual Studio 2022

**Framework Target**: .NET 8.0

**Base de Datos**: controlComparendo

---

## 36. Anexos

### 36.1 Comandos Útiles

#### Ejecutar aplicación
```bash
dotnet run --project Proyecto-BACK/Merge_Back/taller/Web
```

#### Crear migración
```bash
dotnet ef migrations add NombreMigracion --project Entity --startup-project Web
```

#### Aplicar migración
```bash
dotnet ef database update --project Entity --startup-project Web
```

#### Ejecutar pruebas
```bash
dotnet test
```

#### Build del proyecto
```bash
dotnet build
```

#### Publicar
```bash
dotnet publish -c Release -o ./publish
```

#### Docker build
```bash
docker build -t control-multas-ciudadanas-api .
```

#### Docker run
```bash
docker run -p 8080:80 control-multas-ciudadanas-api
```

### 36.2 Puertos por Defecto

- API: https://localhost:5001 o http://localhost:5000
- SQL Server: 1433
- PostgreSQL: 5433
- MySQL: 3307
- Frontend: 4200

### 36.3 Credenciales por Defecto

**Base de datos**:
- Usuario: sa (SQL Server) / postgres (PostgreSQL) / root (MySQL)
- Password: Admin123.

⚠️ **IMPORTANTE**: Cambiar en producción

### 36.4 Aclaración Importante

Este sistema gestiona **multas ciudadanas**, que incluyen:
- Infracciones de convivencia ciudadana
- Violaciones a normas municipales
- Sanciones administrativas
- Multas por incumplimiento de regulaciones locales



### 36.5 Glosario

- **SMLDV**: Salario Mínimo Legal Diario Vigente
- **CDCP**: Control De Multas Ciudadanas y Pagos
- **RBAC**: Role-Based Access Control
- **JWT**: JSON Web Token
- **ORM**: Object-Relational Mapping
- **DTO**: Data Transfer Object
- **CORS**: Cross-Origin Resource Sharing
- **reCAPTCHA**: Sistema de verificación de Google
- **Multa Ciudadana**: Sanción administrativa por incumplimiento de normas municipales o de convivencia

---

**Fin del Manual Tecnográfico**

Versión: 1.0
Fecha: 2025-10-02
