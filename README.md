# ChallengeCore (Sistema de Retos, Perfil y Recompensas)

## 🏦 ChallengeCore – Backend (.NET 10)

    API REST desarrollada en .NET 10 que implementa un sistema de retos donde los usuarios pueden completar puntos y canjear recompensas.

### 🧱 Arquitectura del Proyecto

    ChallengeCore.sln
    │
    ├── 📂 ChallengeCore.Api            # Puntos de entrada (Controllers, Middleware)
    ├── 📂 ChallengeCore.Application    # Lógica de negocio, DTOs y Mapeos
    ├── 📂 ChallengeCore.Domain         # Entidades, Interfaces e Invariantes
    └── 📂 ChallengeCore.Infrastructure # Persistencia, Contexto de BD y Repositorios

### ⚙️ Configuración del Proyecto (appsettings.json)

    Para ejecutar la aplicación, asegúrate de configurar tu archivo appsettings.json con los siguientes parámetros:

### Base de datos (Configuracion de variables)
    ServerName: "",
    DatabaseName: "GamificationDb",
    TrustedConnection: true,
    TrustServerCertificate: true

### Feature Flag (EnableBonusChallenge)
El sistema permite habilitar funcionalidades dinámicamente

    true  => Se asigna automáticamente un reto especial al crear un nuevo usuario.
    false => El flujo de creación de usuario ignora el reto inicial.

## 🔄 Persistencia y Migraciones (Desde la raíz de la solución, ejecutar en la terminal)
Restaurar Dependencias

`dotnet restore`

Generar la migración inicial

`dotnet ef migrations add InitialCreate --project ChallengeCore.Infrastructure --startup-project ChallengeCore.Api`

Actualizar la base de datos

`dotnet ef database update --project ChallengeCore.Infrastructure --startup-project ChallengeCore.Api`

## ▶️ Ejecución de la Aplicación
Para poner en marcha el servidor de desarrollo

`levantar el proyecto ChallengeCore.Api como proyecto de inicio (https)`

