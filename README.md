
# Blazor Server Demo — Integrating with Accessplanit API v2

A commercial demonstration showing how a **.NET 9 Blazor Server** application integrates with **Accessplanit API v2** to read and write training management data (e.g., courses, delegates, users), including best practices for authentication, querying, and error handling.\
This demonstration also includes an example of how to build your own customer portal and shopping basket, including a full-page basket that shows how to **amend basket item costs** and **set a basket's billing target**.
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
- [What the Demo Includes](#what-the-demo-includes)
- [Accessplanit API Basics](#accessplanit-api-basics)
- [Basket API](#basket-api)
- [Sessions and Inactivity Timeout](#sessions-and-inactivity-timeout)
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
- A **customer portal** (invoices, courses, eLearning, bookings and account details) and **shopping basket** (side panel and full page).
- **Basket features** for adding delegates, applying discount codes, changing the **billing target** and amending **item costs**.

> Accessplanit’s v2 feeds (launched 2025) provide broader platform coverage and field access compared to v1. \[Overview]

---

## Architecture
**Tech stack:**
- **Frontend:** Blazor Server (.NET 9) + Bootstrap
- **Backend:** .NET API Client
- **Auth:** Bearer token or API key, supplied by Accessplanit (stored securely) \[Developer Resources]
- **Data:** Typed DTOs for Users, Companies, Courses, Delegates (aligned with v2 feed packages) \[Modules]

**Flow overview:**
1. User signs in to the demo using the API v2 login endpoint.
2. See **AccessPlanitTokenClient.cs** on how to obtain an API Bearer Token or use an API Key.
3. Server-side services add the **Accessplanit** auth header (Bearer token or API key) and call v2 endpoints. \[Developer Resources]
4. Responses are shaped using `$select/$filter` and presented in Blazor components. Grids sort and page in the browser.
5. Baskets are handled by the separate basket API (see [Basket API](#basket-api)).

---

## Live Demo / Repository
- **Repository:** https://github.com/accessplanit/api-v2-demo

---

## Prerequisites
- Microsoft Visual Studio 2022 (17.12 or later) **(Minimum)** or Microsoft Visual Studio Code
- **.NET 9 SDK**
- An **Accessplanit v2 API** subscription and credentials (token or API key). Contact your Accessplanit CSM if you need access. \[Modules]

---

## Getting Started
```bash
# Clone
git clone https://github.com/accessplanit/api-v2-demo.git
cd api-v2-demo/CommercialSite.WebApp

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
- `SuperUserAPIKey` — API key if using API key auth (keep secret). \[Developer Resources] The demo signs in with the service user's username and password by default. To use an API key instead, call `GetAPIKeyAsToken()` in place of `GetToken()`.

Configure via `appsettings.json` in CommercialSite.WebApp
- `Session:IdleTimeoutMinutes` — Minutes of inactivity before a user is signed out (default `15`).
- `Session:WarningSeconds` — How long the "are you still there?" warning shows before sign-out (default `60`).

> **Credentials & roles:** You’ll be provided a **username/ID** and **password** for an account with required roles; use these either to **generate a token** or to log in and create an **API key**. \[Developer Resources]

## Running the App
Open a terminal and navigate to the folder **api-v2-demo\CommercialSite.WebApp**
```bash
# Development (Blazor Server)
dotnet run 
```
Open `https://localhost:7228` (or `http://localhost:5252`), or the address shown in the console.

---

## What the Demo Includes
| Page | Route | What it shows |
|---|---|---|
| Sign in | `/login` | Signing in through the API v2 login endpoint, on its own layout. |
| My Portal | `/` | Invoices, eLearning, courses, web courses and bookings for the signed-in user. |
| Courses | `/courses` | Searching courses, viewing dates and adding a date to the basket. |
| My Account | `/account` | Reading and updating the signed-in user's details. |
| Shopping Basket | `/basket` | A full-page basket for **amending item costs** and **setting the billing target**. |

The side basket (the **Basket** button in the header) covers the everyday basket actions. The full-page basket adds the two features that show off the basket API:
- **Billing target** — choose who the basket is billed to from the individuals and companies the API returns on the basket (`AvailableBillingUsers` and `AvailableBillingCompanies`).
- **Item costs** — change the net and tax per delegate for a basket item, and reset it to the original.

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

> `FluentUrlBuilder` can build all of these. The demo's own pages use `$select` and `$filter`, and sort and page the grids in the browser.

**API Packages (common modules)**
- **Users & Accounts:** Users, Companies, Company Groups
- **Courses & Delegates:** Course Dates, Delegates, Course Templates
- **Invoices & Transactions:** Invoices, Transactions, Invoice Items \[Modules]

**Versioning**
Accessplanit **v2** (launched 2025) offers broader platform coverage and field access compared to **v1**. \[Overview]

---

## Usage Examples
The API clients in the **APIClient** project wrap the v2 endpoints. Each model maps to an API module, and the client builds the URL from the module name.

### 1) Query with `$select` and `$filter`
```csharp
var invoices = await invoiceClient.GetByCriteria<Invoice>(
    [new Filter("targetid", FilterExpressions.Equals, userID)],
    ["id", "reference", "amount", "outstanding", "status"]);
```
This sends a **GET** to the Invoice module with `$select` and `$filter=targetid eq '<userID>'`.

### 2) Update a user (PUT)
```csharp
await userClient.UpdateUser(userID, new Dictionary<string, object>
{
    { "Forenames", "Alex" },
    { "Surname", "Doe" },
    { "Email", "alex.doe@example.com" }
});
```
Only the properties that changed are sent. See `User.GetUpdatedProperties` and the My Account page.

### 3) Create and delete
`Create<T>` sends a **POST** with the model as JSON, and `Delete` sends a **DELETE** for an ID. Both are on `AccessPlanitAPIClientBase`.

> `FluentUrlBuilder` assembles the query string (`$select`, `$filter`, `$orderby`, `$top`, `$skip`) for every request.

---

## Basket API
Baskets use a separate service under `/apibasket/v2/basket` (see `BaseBasketAddress` in AccessPlanitAPIConfig). The client is `AccessPlanitAPIBasketClient`.

| Action | Verb and route | Body |
|---|---|---|
| Get a user's basket | `GET /{userID}` | |
| Add an item or discount code | `POST /{userID}/{itemID}/{IDType}` | |
| Remove an item | `DELETE /{basketID}/{itemID}` | |
| Add delegates to an item | `PUT /{basketID}/{itemID}/users` | JSON array of user IDs |
| Remove a delegate | `DELETE /{basketID}/{itemID}/{userID}` | |
| Set the billing target | `PUT /{basketID}/billingtarget` | `{ "Type": "Company", "ID": "...", "Name": "...", "TaxExempt": false }` |
| Amend an item's cost | `PATCH /{basketID}/{itemID}/cost` | `{ "NetPerUnit": 66.00, "TaxPerUnit": 13.00 }` |
| Complete the basket | `POST /{basketID}/complete` | |

Things to know:
- Every update returns the whole updated basket, which the demo uses to replace its copy.
- A basket item's `UnitNet`, `UnitTax` and `UnitGross` cover the **whole line** (all delegates). The cost endpoint takes amounts **per unit**, so the full-page basket divides by quantity when it shows the edit form.
- An item's quantity follows its delegate count. Sending an empty delegate list resets the quantity to 0, so the demo never sends one.
- The currency symbol on a basket item can come back HTML-encoded (for example `&#163;`).

---

## Sessions and Inactivity Timeout
- The API token and the signed-in user's ID are kept in the browser's local storage, so a page reload restores the session (a new token is requested on restore).
- Users are signed out after a period of inactivity (`Session:IdleTimeoutMinutes`, default 15 minutes), with a warning countdown beforehand. Activity in any open tab counts.
- A reload after the idle period does not restore the session. The user is returned to the sign-in page.
- The countdown runs in the browser. For a hard guarantee, also expire the API tokens on a similar schedule.

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



