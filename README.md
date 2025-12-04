# ByteGuard Codex

**ByteGuard Codex** is an application security tool for managing OWASP ASVS-based and custom organizational ASVS standards and mapping them to your software projects.

> 🚧 ByteGuard Codex is currently **under active development** and not yet production-ready.  
> API schema, UI and data model may change between releases.

## 🔎 What is Codex?

ByteGuard Codex helps you:

- Manage one or more **ASVS versions** in a single place
- Construct your own **custom orgnizational ASVS version**
- Organize your **applications / projects** and link them to specific ASVS versions
- Track which **requirements are in scope** for each project
- Record **implementation / verification status** for each requirement
- Prepare ASVS-based **reports** for stakeholders

## 🧱 Architecture

Codex is a classic **SPA + API** web application:

- **Backend**

  - ASP.NET Core Web API
  - Exposes endpoints for ASVS versions, requirements, and projects
  - Uses a relational database (e.g. SQL Server / PostgreSQL) via EF Core or similar ORM

- **Frontend**
  - Vue 3 + Vite
  - TypeScript
  - Pinia for state management
  - Vue Router
  - Sass (SCSS) for styling

### Project layout (simplified)

```
/src
  /backend # .NET API
  /frontend # Vue 3 webapp
```

## 🚀 Getting Started

### Prerequisites

Make sure you have:

- [.NET SDK](https://dotnet.microsoft.com/) (10.0 or later)
- [Node.js](https://nodejs.org/) (LTS recommended)
- A package manager: `npm`, `yarn`, or `pnpm`

> At the moment ByteGuard Codex is set up to use a local SQLite database.

### 1. Clone the repository

```bash
git clone https://github.com/ByteGuard-HQ/byteguard-codex.git
cd byteguard-codex
```

### 2. Backend setup

From the repo root:

```bash
# Build the solution
cd src/backend/
dotnet build

# Update/create the database
cd ByteGuard.Codex.Infrastructure.Sqlite
dotnet ef database update --startup-project ../ByteGuard.Codex.Api/ByteGuard.Codex.Api.csproj

# Start the API
cd ..
dotnet run --project ByteGuard.Codex.Api/ByteGuard.Codex.Api.csproj
```

The API will be hosted at `https://localhost:7258`.  
API documentation is availavble with Scalar at `https://localhost:7258/scalar`

### 3. Frontend setup

From the repo root:

```bash
# Install packages
cd src/frontend/
npm install

# Run the webapp
npm run dev
```

The webapp will be hosted at `http://localhost:8080`.

## 🤝 Contributing

Contributions are very welcome!

- Want to become a collaborator? Give me a shout!
- Have a feature idea? Join the [Discord community server](https://discord.gg/XwjdR2jmVZ).
- Want to contribute code? Fork the repo, create a feature branch, and open pull request.

Please try to:

- Follow existing coding style and conventions.
- Add or update unit tests where relevant.
- Keep commits reasonably scoped and well-described.

### 👀 Looking for frontend contributors

**ByteGuard Codex** is built with Vue 3, TypeScript, Pinia and Sass.

If you enjoy frontend architecture, design systems, or just building clean UIs and want to help shape Codex, I’d love to collaborate.

- Give me a shout
- Or join the ByteGuard HQ Discord and say hi

## ⚠️ Disclaimer

**ByteGuard Codex** is an independent open-source project maintained by the ByteGuard community.  
It is **not affiliated with, endorsed by, or sponsored by OWASP** or the OWASP Foundation.

OWASP® and OWASP ASVS® are trademarks or registered trademarks of the OWASP Foundation.  
Any references to OWASP or OWASP ASVS in this project are for interoperability and informational purposes only.

## 📄 License

_ByteGuard Codex is Copyright © ByteGuard Contributors - Provided under the MIT license._
