
# Blazor Server Demo — Integrating with Accessplanit API v2

A commercial demonstration showing how a **.NET 8 Blazor Server** application integrates with **Accessplanit API v2** to read and write training management data (e.g., courses, delegates, users), build your own basket and trainer portal. Including best practices for authentication, querying, and error handling.\
**Note:** This demo does **not** include webhooks.

> Accessplanit v2 developer resources cover authentication (token or API keys), query parameters like `$select/$filter/$orderby/$top/$skip`, standard HTTP verbs, and response codes. See the official docs for details.\
> \- API v2 Developer Resources: https://accessplanit.atlassian.net/wiki/spaces/HG/pages/3215425537/API+v2+-+Developer+Resources \[docs]\
> \- Integrate with API v2 Feeds (Overview & v1 vs v2 differences): https://accessplanit.atlassian.net/wiki/spaces/HG/pages/3184951297/Integrate+with+the+accessplanit+API+v2+Feeds+-+Overview \[overview]\
> \- API packages and modules (Users, Courses, Delegates, Finance): https://www.accessplanit.com/users-api-module-information-accessplanit \[modules]\
> \- API Terms of Use (security, rate limits, key handling): https://www.accessplanit.com/api-terms-of-use \[terms]

---

## Table of Contents
- [About the Demo](#about-the-demo)
- [Architecture](#architecture)
- [Live Demo / Repository](#live-demo--repository)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Running the App](#running-the-app)
- [Accessplanit API Basics](#accessplanit-api-basics)
- [Usage Examples](#usage-examples)
- [Security & Compliance](#security--compliance)
- [Troubleshooting](#troubleshooting)
- [License](#license)

---

## About the Demo
This Blazor Server demo is built for **sales engineering and partner enablement**. It showcases:

- **Auth flows** using either **token-based** or **API key** authentication provided by Accessplanit v2. \[Developer Resources]
- **Query patterns** with `$select`, `$filter`, `$orderby` to retrieve precise datasets efficiently. \[Developer Resources]
- **Create/Update/Delete** patterns (POST/PUT/DELETE) and **upsert** where supported. \[Developer Resources]
- Clean separation of **UI** (Blazor components) and **API** access layer (typed `HttpClient` services).

> Accessplanit’s v2 feeds (launched 2025) provide broader platform coverage and field access compared to v1. \[Overview]

---

## Architecture
**Tech stack:**
- **Frontend:** Blazor Server + Bootstrap
- **Backend:** .NET API Client
- **Auth:** Bearer token or API key, supplied by Accessplanit (stored securely) \[Developer Resources]
- **Data:** Typed DTOs for Users, Companies, Courses, Delegates (aligned with v2 feed packages) \[Modules]

**Flow overview:**
1. User signs in to the demo using the API v2 login endpoint.
2. See **AccessPlanitTokenClient.cs** on how to obtain an API Bearer Token or use an API Key.
3. Server-side services add the **Accessplanit** auth header (Bearer token or API key) and call v2 endpoints. \[Developer Resources]
4. Responses are shaped using `$select/$filter`, paged via `$top/$skip`, and presented in Blazor components. \[Developer Resources]

---

## Live Demo / Repository
- **Repository:** https://github.com/accessplanit/api-v2-demo

---

## Prerequisites
- Microsoft Visual Studio 2022 **(Minimum)** or Microsoft Visual Studio Code
- **.NET 8 SDK**
- An **Accessplanit v2 API** subscription and credentials (token or API key). Contact your Accessplanit CSM if you need access. \[Modules]

---

## Getting Started
```bash
# Clone
git clone https://github.com/accessplanit/api-v2-demo.git
cd CommercialSite.WebApp

# Restore & build
dotnet restore
dotnet build
```

---

## Configuration
This demo implements a service user to communicate with the API, allowing restriction of data and actions via their roles and permissions.
Configure via AccessPlanitAPIConfig in Constants
- `RootURI` — Base URL for your tenant’s v2 feeds (provided by Accessplanit). \[Developer Resources]

Configure via AccessPlanitTokenClient in APIClient
- `SuperAPIUserID` — User ID of the service user you have set up to communicate with the API. \[Developer Resources]
- `SuperAPIPassword` — Password of the above user. \[Developer Resources]
- `SuperUserAPIKey` — API key if using API key auth (keep secret). \[Developer Resources]

> **Credentials & roles:** You’ll be provided a **username/ID** and **password** for an account with required roles; use these either to **generate a token** or to log in and create an **API key**. \[Developer Resources]

## Running the App
Open a terminal and navigate to the folder **api-v2-demo\CommercialSite.WebApp**
```bash
# Development (Blazor Server)
dotnet run 
```
Open `https://localhost:5001` (or as shown in console).

---

## Accessplanit API Basics
This demo implements a URL Builder found in Services > FluentUrlBuilder.cs
**HTTP Verbs & Responses** — v2 feeds support GET, POST, PUT, DELETE with documented response codes. \[Developer Resources]

**Selecting & Filtering Data**
- `$select`: choose specific fields to return
- `$filter`: filter records (e.g., by date/status)
- `$orderby`: sort records
- `$top`: page size
- `$skip`: pagination offset \[Developer Resources]

**API Packages (common modules)**
- **Users & Accounts:** Users, Companies, Company Groups
- **Courses & Delegates:** Course Dates, Delegates, Course Templates
- **Invoices & Transactions:** Invoices, Transactions, Invoice Items \[Modules]

**Versioning**
Accessplanit **v2** (launched 2025) offers broader platform coverage and field access compared to **v1**. \[Overview]

---

## Usage Examples
> Replace placeholders with your tenant-specific base URL and endpoints.

### 1) List upcoming course dates (paged & filtered)
**GET** `/courses/dates?$select=Id,Title,StartDate,Status&$filter=StartDate ge 2025-01-01&$orderby=StartDate asc&$top=25&$skip=0`\
Headers: `Authorization: Bearer <Accessplanit__Token>` **or** ` <Accessplanit__ApiKey>` \[Developer Resources]

### 2) Create a new delegate (POST)
**POST** `/delegates`
```json
{
  "UserId": 12345,
  "CourseDateId": 67890,
  "Status": "Registered",
  "Notes": "Demo enrolment via Blazor"
}
```

### 3) Update a user (PUT)
**PUT** `/users/12345`
```json
{
  "FirstName": "Alex",
  "LastName": "Doe",
  "Email": "alex.doe@example.com"
}
```

### 4) Upsert (Insert or Update)
**POST (Upsert)** `/users` — If record exists, update; otherwise insert. \[Developer Resources]

> The demo includes a **Query Builder** that assembles `$select/$filter/$orderby/$top/$skip` parameters and shows request/response pairs to aid learning and sales conversations. \[Developer Resources]

---

## Security & Compliance
- **Do not commit secrets** (tokens, API keys) to Git. Rotate credentials regularly. \[Developer Resources, Terms]
- Respect **API Terms of Use**, including secure handling of keys. \[Terms]
- Use HTTPS; limit CORS to trusted origins.

---

## Troubleshooting
- **401/403**: Check token/API key validity and roles/permissions. \[Developer Resources]
- **Empty results**: Verify `$filter` and `$select` fields exist for your module/package. \[Developer Resources]
- **Pagination**: Confirm `$top` and `$skip` usage; the UI surfaces total counts where available. \[Developer Resources]

---

## License
© accessplanit ltd. Provided under MIT License.
This is demo software and **not** production-ready. Review and comply with **Accessplanit’s API Terms of Use** before integrating. \[Terms]



