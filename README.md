# CDB Investment Simulator

This project was developed as part of a backend technical challenge to simulate the gross and net return of a CDB (Certificado de Depósito Bancário) investment.

The application exposes an HTTP API that receives the initial investment amount and the investment term in months and returns the gross and net investment values.

The solution follows a layered architecture separating the HTTP layer, application logic, and business rules.

---

# Project Structure

The backend is organized as follows:

```text
backend
├─ src
│  ├─ CdbCalculator.Api
│  ├─ CdbCalculator.Application
│  └─ CdbCalculator.Domain
│
└─ tests
   └─ CdbCalculator.UnitTests
```

---

## Layer Responsibilities

**CdbCalculator.Api**

Responsible for:

- HTTP endpoints
- request routing
- middleware configuration
- Swagger documentation
- dependency injection configuration

**CdbCalculator.Application**

Responsible for:

- application use cases
- input validation
- orchestration between layers

**CdbCalculator.Domain**

Responsible for:

- financial calculations
- core business rules

**CdbCalculator.UnitTests**

Responsible for:

- unit tests for domain and application layers

---

# Technologies

Backend:

- .NET 8
- ASP.NET Core Minimal API
- Swagger / OpenAPI
- xUnit

Code Quality:

- .NET analyzers
- SonarLint

---

# Business Rules

The system simulates a CDB investment based on the formula provided in the challenge.

## Formula

**VF = VI × [1 + (CDI × TB)]**

Where:

| Symbol | Description |
|------|-------------|
| VF | Final value |
| VI | Initial investment value |
| CDI | Monthly CDI rate |
| TB | Bank percentage over CDI |

For this exercise the following fixed values are used:

| Variable | Value |
|---------|------|
| CDI | 0.9% |
| TB | 108% |

The investment grows using **monthly compound interest**, meaning the result of each month becomes the base for the next month.

---

# Income Tax Rules

Income tax is applied only to the **earnings portion** of the investment.

| Investment Period | Tax Rate |
|------------------|---------|
| Up to 6 months | 22.5% |
| Up to 12 months | 20% |
| Up to 24 months | 17.5% |
| Above 24 months | 15% |

Calculation:

Earnings = GrossAmount - InitialAmount
Tax = Earnings × TaxRate
NetAmount = GrossAmount - Tax

---

# API Endpoint

## Create CDB Simulation

POST /api/investments/cdb/simulations

Creates a simulation of a CDB investment using the provided initial amount and investment term.

### Request Parameters

| Field | Type | Description |
|------|------|-------------|
| initialAmount | decimal | Initial investment amount. Must be greater than zero |
| months | integer | Investment duration in months. Must be greater than 1 |

### Request Example

```json
{
  "initialAmount": 1000.00,
  "months": 12
}
```

### Response Example

```json
{
  "grossAmount": 1123.66,
  "netAmount": 1098.93
}
```

| Field       | Description                     |
| ----------- | ------------------------------- |
| grossAmount | Total value before taxes        |
| netAmount   | Final value after tax deduction |

---

# Error Handling

The API implements a global exception handling middleware.

Errors are returned using the ProblemDetails format defined by RFC 7807.

Example error response:

```json
{
  "type": "about:blank",
  "title": "Invalid request",
  "status": 400,
  "detail": "Initial amount must be greater than zero.",
  "instance": "/api/investments/cdb/simulations"
}
```

Possible responses:

| Status Code | Description               |
| ----------- | ------------------------- |
| 400         | Validation error          |
| 500         | Unexpected internal error |

---

# API Documentation

Swagger documentation is automatically generated.

After starting the application, access:

**https://localhost:{port}/swagger**

## Swagger provides:

- endpoint description
- request and response schemas
- example payloads
- HTTP response codes

---

# Running the Application

## Requirements

The following tools are required to run the backend locally:

| Tool | Version |
|-----|--------|
| .NET SDK | 8.0 or higher |
| Git | Any recent version |

Verify the .NET installation:

**dotnet --version**

---

## Clone the Repository

Clone the repository locally:

**git clone <repository-url>**

Navigate to the project directory.

---

## Restore Dependencies

Restore all project dependencies:

**dotnet restore**

---

## Build the Solution

Compile the entire solution:

**dotnet build**

The build should complete without warnings or errors.

---

## Run the API

Start the Web API:

**dotnet run --project backend/src/CdbCalculator.Api**

Once the application starts, the API will be available locally.

By default the API will be accessible at:

**https://localhost:7190**

Swagger documentation can be accessed at:

**https://localhost:7190/swagger**

---

# Running Tests

Execute the full unit test suite:

**dotnet test**

The tests validate the main business rules of the system, including:

- CDB gross value calculation
- income tax calculation
- request validation
- application service orchestration

---

# Test Coverage

Unit tests were implemented for the logical layers of the system.

Coverage metrics:

| Metric | Result |
|------|------|
| Line Coverage | 96% |
| Branch Coverage | 90% |

Coverage reports were generated using:

- Coverlet
- ReportGenerator

The achieved coverage satisfies the challenge requirement of **more than 90% coverage in the logical layer**.

---

# Code Quality

The solution follows common backend development best practices, including:

- SOLID principles
- layered architecture
- dependency injection
- centralized error handling
- standardized HTTP responses

Static analysis tools used:

- .NET analyzers
- SonarLint

The project compiles with strict validation enabled:

**dotnet build -warnaserror**

This ensures that no compiler warnings remain in the solution.

---

# Design Decisions

Several design decisions were made to improve maintainability and clarity.

---

## Layered Architecture

The system separates HTTP concerns, application orchestration, and business logic into independent layers.

---

## Minimal API

Minimal API was used to keep the HTTP layer concise and focused.

---

## Dependency Injection

Application services are registered through extension methods to keep the startup configuration organized.

---

## Global Exception Handling

A custom middleware centralizes error handling and ensures consistent HTTP responses.

---

## ProblemDetails

Error responses follow the ProblemDetails format defined by RFC 7807.

---

# Author

This project was developed as part of a technical challenge.
