# Sr. Infrastructure / DevOps Engineer — Screening Exercise

**Time-boxed: 3–4 hours, hard cap.**

## Purpose

This is a short, focused screen — not a full build. We want a clean signal on
container hygiene and CI/CD fundamentals in a couple of hours. **Please stop at
~3 hours** even if something is unfinished, and tell us in your README what you
would have done next. Stubbing with a written plan beats a half-broken
everything.

You may use any tooling, AI assistants included. We read your README and your
commit history as closely as we read your code.

## Context

Our API is **.NET 10** (C#, EF Core, PostgreSQL). A **React 18 + Vite** UI talks to
it through a small .NET **BFF** (`Bff`, a YARP reverse proxy). CI runs on **GitHub
Actions**. This repo gives you a minimal, runnable slice: the API project, the BFF,
the React UI, a solution file, and unit tests. The request chain is:

```
Browser → UI (React/Vite) → BFF (/api → YARP) → API (.NET 10) → PostgreSQL
```

See [`README.md`](./README.md) for how it's laid out and how to build each piece.

## The task

### 1. Containerize (≈1.5h)
- Write multi-stage Dockerfiles for the **.NET 10 API**, the **BFF**, and the
  **React UI** (non-root user; restored/published layers cached sensibly). For the
  UI, build with Node and serve the static output — and route `/api` to the BFF.
- Provide a `docker-compose.yml` that runs **API + BFF + UI + PostgreSQL** with
  health checks and a named network, and starts cleanly with one command. Opening
  the UI in a browser should list the seeded widgets (UI → BFF → API → Postgres).

### 2. CI pipeline (≈1.5h)
- A GitHub Actions workflow that, on push/PR: restores, builds, and runs the
  **unit tests** for the .NET solution, and builds the **React UI**.
- Builds the images tagged with **`branch + short SHA`** (the API at minimum;
  the UI/BFF too is a plus).
- Generates an **SBOM** and runs a vulnerability scan (**Trivy or Grype**) that
  **fails the job on high/critical**.
- Uploads test results, the SBOM, and the scan report as artifacts.

### 3. A short README (≈15–30m)
- One command to run it locally, and how to run the tests.
- One paragraph of trade-offs: what you stubbed, what you'd harden with more time,
  and any security choices.

## Explicitly out of scope

Kubernetes/Helm, observability dashboards, Vercel, Keycloak/OIDC, secret stores,
and full integration tests are **not** expected here. Don't spend time on them.

## What we score

| | |
|---|---|
| **Strong** | One-command bring-up that actually works; health checks gate dependencies. Multi-stage, non-root images; small final image; layer caching considered. Scan gate genuinely fails on a high/critical finding (not just runs). Honest, concise README with real trade-offs. |
| **Weak** | Compose that needs manual steps or fails on a fresh clone. Single-stage / root images; secrets baked into the image. Scan present but non-gating, or artifacts not published. No README, or one that overstates completeness. |

## How to submit

Submit your work as a **pull request from your own fork** of this repository:

1. **Fork** this repo to your own GitHub account.
2. Do your work on a branch in your fork, committing as you go (we like seeing the
   history — small, real commits are a plus).
3. Open a **pull request back to this repository** when you're done.
4. In the PR description, include a short note on **time spent** and **what you'd do
   next** if you stopped at the cap.

Please **don't email a zip** — we only review fork + PR submissions, so we can see
your commit history and run your branch directly.

Note your actual time spent — we respect the cap and read for judgment, not volume.

Good luck — we're excited to see how you think.
