# Openpay Platform

## Overview

Openpay Platform is a RESTful API built with ASP.NET Core that supports the settlement and fund transfer operations of Openpay. It allows registering disbursement orders for affiliate stores, including their execution schedule, transfer cap, status and operator user.

## Authorship

- Author: Carlos Gabriel Cespedes Lezcano
- Student code: U202416147
- Repository: https://github.com/Leikop/pc8168u20246147.git

## Features

- `POST /api/v1/disbursement-orders` to register a disbursement order.
- Unique order tracking code per disbursement order (`409 Conflict` on duplicates).
- Business rules for execution schedule, transfer cap, status, user and remarks (`400 Bad Request` with a localized message).
- Audit timestamps (`created_at`, `updated_at`).
- Localized responses in English (`en`, `en-US`) and Spanish (`es`, `es-PE`) through the `Accept-Language` header.
- OpenAPI documentation with Scalar.

## Architecture

The solution applies Domain-Driven Design with a layered architecture (Domain, Application, Infrastructure, Interfaces) in two bounded contexts:

- `Settlements`: `DisbursementOrder` aggregate, `OpenpayReference`, `StoreIdentifier` and `DisbursementStatus`.
- `Shared`: `DateInterval` and `FinancialAmount` value objects and the reusable base elements (repositories, unit of work, auditing, `Result` pattern and naming conventions).

## Technologies

- C# 14 and .NET 10
- ASP.NET Core Web API, OpenAPI and Scalar
- Entity Framework Core 10 with MySQL (`MySql.EntityFrameworkCore`)
- Humanizer

## Getting Started

1. Set your MySQL credentials in `appsettings.Development.json`. The `openpay_settlements` schema is created on startup.
2. Run the API:

```bash

dotnet restore
dotnet run --project pc8168u2024614
```

3. Open `http://localhost:5128/scalar`.
