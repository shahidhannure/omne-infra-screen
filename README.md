# Omne — Sr. Infrastructure / DevOps Screening Slice

This is the starter repository for the **2–3 hour screening exercise**. It is a
deliberately small, runnable slice of the Omne backend so you can focus on
containerization and CI/CD rather than on writing application code.

> Read the exercise brief in [`BRIEF.md`](./BRIEF.md) for the full task and what we
> score. This README only describes the code you've been given.

## What's here

```
screening-slice/
├── src/
│   ├── Api/          .NET 10 minimal API — EF Core + PostgreSQL, /health, /api/widgets
│   └── Bff/          .NET 10 YARP reverse proxy in front of the API (mirrors our React.BFF)
├── test/
│   └── Api.Tests/    xUnit unit tests (no database required — uses EF InMemory)
├── Directory.Build.props      shared build config (net10.0)
├── Directory.Packages.props   central package versions
├── nuget.config               public feed only — restores without Omne credentials
└── Omne.Screen.sln
```

The API mirrors how our real services are shaped:
- Connection string is read from `ConnectionStrings__OmneScreen` (env / config) — **never hard-coded**.
- `GET /health` pings the database and returns `503` when it can't connect.
- `GET /api/widgets` and `GET /api/widgets/stats` exercise EF Core + Postgres.

The BFF mirrors our `React.BFF`: a YARP reverse proxy that forwards `/api/**` to
the API. Its proxy target is overridable at runtime via
`ReverseProxy__Clusters__api-cluster__Destinations__api__Address`.

## Prerequisites

- .NET 10 SDK
- Docker (you'll be adding the container + compose setup)

## Build & test (without Docker)

```bash
dotnet build Omne.Screen.sln -c Release
dotnet test  Omne.Screen.sln -c Release
```

Run the API against a local Postgres:

```bash
ConnectionStrings__OmneScreen="Host=localhost;Port=5432;Database=omne_screen;Username=postgres;Password=postgres" \
  dotnet run --project src/Api
# then: curl localhost:5080/health   (port per launchSettings / your config)
```

## Your task (summary — see the brief for detail and scoring)

1. **Containerize** the API and the BFF (multi-stage, non-root).
2. **`docker-compose.yml`** bringing up API + BFF + PostgreSQL with health checks
   and a named network — one command to start.
3. **CI pipeline** (GitHub Actions preferred): build + run the unit tests, build
   the API image tagged with `branch + short SHA`, generate an **SBOM**, run a
   **vulnerability scan (Trivy/Grype) that fails the job on high/critical**, and
   upload test results + SBOM + scan report as artifacts.
4. A short **README** of your own: how to run it, and the trade-offs you made.

**Out of scope for this screen** (they belong to the longer exercise): Kubernetes /
Helm, observability dashboards, Vercel, Keycloak/OIDC, secret stores, full
integration tests. Don't spend time on them.

Please respect the ~3 hour cap and tell us what you'd do next where you stopped.
