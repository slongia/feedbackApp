# FeedbackApp

ASP.NET Core 9 Razor Pages app with PostgreSQL and end-to-end CI/CD to Azure App Service (staging + production).

---

## Tech Stack

- .NET 9 / ASP.NET Core Razor Pages
- Entity Framework Core (`BillingDbContext`)
- PostgreSQL (local + Azure)
- GitHub Actions CI/CD
- Azure App Service (Web App + staging slot)

---

## 1. Local Setup

### Prerequisites

- .NET 9 SDK
- PostgreSQL installed locally (running on **localhost:5433**)
- Git

### Clone

```bash
git clone https://github.com/<your-account>/feedback.git
cd feedback
```

### Configure Local Database

1. Create the dev database in Postgres:

   ```sql
   CREATE DATABASE feedback_dev;
   ```

2. Set the connection string in `src/feedbackApp/appsettings.Development.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5433;Database=feedback_dev;Username=<dev_user>;Password;<dev_password>"
   }
   ```

### Apply Migrations Locally

```bash
cd src/feedbackApp
dotnet ef database update
```

---

## 2. Run & Test Locally

From the repo root:

```bash
# Restore + build + test
dotnet restore
dotnet build --configuration Release
dotnet test  --configuration Release

# Run the web app
dotnet run --project src/feedbackApp/feedbackApp.csproj
```

Browse to `https://localhost:5001` (or the URL printed in the console).

**Features:**

- `Pages/Customer/*` – full CRUD UI for Customers
- Server-side validation with DataAnnotations
- Unique email constraint enforced at DB level via EF Core migrations

---

## 3. Azure Setup

### Azure Resources

You should have:

- **App Service**: `feedback`
  - **Staging slot**: `staging`
- **Azure Database for PostgreSQL** (or equivalent), with two DBs:
  - `feedback_staging`
  - `feedback_prod`

Each DB user must have permissions to create/alter tables in the `public` schema.

### App Service Configuration

**Staging slot (`feedback` → `staging`):**

- Application setting:
  - `ASPNETCORE_ENVIRONMENT = Staging`
- Connection string:
  - Name: `DefaultConnection`
  - Value: Npgsql connection string to `feedback_staging`

**Production (`feedback` main slot):**

- Application setting:
  - `ASPNETCORE_ENVIRONMENT = Production`
- Connection string:
  - Name: `DefaultConnection`
  - Value: Npgsql connection string to `feedback_prod`

---

## 4. GitHub Secrets

In **GitHub → Settings → Secrets and variables → Actions**, add:

- `AZURE_FEEDBACKAPP_PUBLISH_PROFILE_STAGING` – publish profile of the **staging slot**
- `AZURE_FEEDBACKAPP_PUBLISH_PROFILE_PROD` – publish profile of the **production slot**
- `AZURE_POSTGRES_CONNECTION_STAGING` – Npgsql connection string for `feedback_staging`
- `AZURE_POSTGRES_CONNECTION_PROD` – Npgsql connection string for `feedback_prod`

---

## 5. CI/CD Pipeline (GitHub Actions)

Workflow file: `.github/workflows/ci-cd.yml`

**Triggers:**

- Push or PR to `dev` and `main`.

**Jobs Overview:**

1. `build_and_test`
   - `dotnet restore`
   - `dotnet build`
   - `dotnet test`

2. `migrate_staging_db` (branch `dev`)
   - Uses `AZURE_POSTGRES_CONNECTION_STAGING` via `ConnectionStrings__DefaultConnection`
   - Runs `dotnet ef database update` against staging DB

3. `deploy_staging` (branch `dev`)
   - Publishes `src/feedbackApp/feedbackApp.csproj`
   - Deploys to Azure Web App **staging slot** using `AZURE_FEEDBACKAPP_PUBLISH_PROFILE_STAGING`

4. `migrate_prod_db` (branch `main`)
   - Uses `AZURE_POSTGRES_CONNECTION_PROD`
   - Runs `dotnet ef database update` against prod DB

5. `deploy_production` (branch `main`)
   - Publishes `src/feedbackApp/feedbackApp.csproj`
   - Deploys to **production** App Service using `AZURE_FEEDBACKAPP_PUBLISH_PROFILE_PROD`

**Guarantee:**  
Database migrations are applied **before** each deployment, so the web app and DB schema stay in sync.

---

## 6. Branch Workflow

- **Local dev**: work on a feature branch → merge into `dev`
- **Staging**:
  - Push/merge to `dev`
  - GitHub Actions:
    - build + test
    - migrate staging DB
    - deploy to staging slot
- **Production**:
  - After verifying staging, merge `dev` → `main`
  - GitHub Actions:
    - build + test
    - migrate prod DB
    - deploy to production slot

---

## 7. Useful Commands

```bash
# Create a new EF Core migration
cd src/feedbackApp
dotnet ef migrations add <MigrationName>

# Apply migrations to the current connection
dotnet ef database update

# Run only the web project
dotnet run --project src/feedbackApp/feedbackApp.csproj
```

---

Happy shipping 🚀
