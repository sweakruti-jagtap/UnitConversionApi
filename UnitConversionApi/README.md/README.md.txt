# Unit Conversion API

## Overview

Unit Conversion API is a RESTful ASP.NET Core Web API that allows users to convert numerical values between different units of measurement.

The solution is designed with scalability and maintainability in mind, following a layered architecture and leveraging Dependency Injection, centralized exception handling, and unit testing.

### Supported Conversion Categories

* Length
* Weight / Mass
* Temperature
* Area
* Volume
* Time

### Example Conversions

* Meter → Foot
* Kilometer → Mile
* Kilogram → Pound
* Celsius → Fahrenheit
* Acre → Square Meter
* Gallon → Liter
* Day → Hour

---

## Technology Stack

* ASP.NET Core (.NET 8 / Latest Stable Version)
* C#
* Swagger / OpenAPI
* Dependency Injection
* xUnit
* Moq
* Middleware-based Exception Handling

---

## Solution Structure


UnitConversionApi
│
├── Controllers
│   └── ConversionController.cs
│
├── Interfaces
│   └── IConversionService.cs
│
├── Middleware
│   └── ExceptionMiddleware.cs
│
├── Models
│   ├── ConversionRequest.cs
│   ├── ConversionResponse.cs
│   └── ErrorResponse.cs
│
├── Repositories
│   └── UnitRepository.cs
│
├── Services
│   └── ConversionService.cs
│
└── Program.cs

UnitConversionApi.Tests
│
├── Controllers
├── Middleware
└── Services


## Running the Application Locally

### Prerequisites

* .NET SDK 8.0 or later
* Visual Studio 2022/2026 (or Visual Studio Code)

### Steps

1. Clone the repository

git clone <repository-url>

2. Navigate to the project directory

cd UnitConversionApi

3. Restore NuGet packages

dotnet restore

4. Build the solution

dotnet build


5. Run the application

dotnet run

6. Open Swagger UI

https://localhost:<port>/swagger


Swagger provides interactive API documentation and allows testing endpoints directly from the browser.

---

## API Endpoint

### Convert Units

**POST**

/api/conversion

### Sample Request

{
  "value": 100,
  "fromUnit": "meter",
  "toUnit": "foot"
}


### Sample Response

{
  "originalValue": 100,
  "fromUnit": "meter",
  "toUnit": "foot",
  "convertedValue": 328.084
}

---

## Error Handling

The API uses a centralized exception handling middleware to ensure consistent error responses.

### Example Error Response

{
  "statusCode": 400,
  "message": "Unsupported conversion from 'meter' to 'banana'.",
  "details": null
}

---

## Unit Testing

The solution includes automated unit tests using xUnit and Moq.

### Test Coverage Includes

* Length conversions
* Weight conversions
* Temperature conversions
* Area conversions
* Volume conversions
* Time conversions
* Invalid unit validation
* Null request validation
* Controller behavior validation
* Middleware exception handling

### Running Tests

dotnet test

---

## Design Decisions

### Layered Architecture

The solution follows a layered architecture:

Controller → Service → Repository

This separation improves maintainability, readability, and testability.

### Dependency Injection

Services are registered using ASP.NET Core's built-in Dependency Injection container, making the application loosely coupled and easier to extend.

### Repository-Based Unit Definitions

Unit conversion factors are stored in a dedicated repository class. This keeps conversion logic separated from data and allows future migration to a database or external configuration source.

### Centralized Exception Handling

A custom middleware handles exceptions globally, ensuring consistent error responses and reducing duplicated try-catch blocks across controllers.

### Extensibility

The design allows additional conversion categories and units to be added with minimal changes to existing code.

---

## Trade-Offs

To keep the solution focused and simple:

* Conversion units and factors are hardcoded in memory.
* No database is used.
* Authentication and authorization are not implemented.
* Logging is limited to middleware-level exception handling.

These decisions reduce complexity while still providing a clean and extensible foundation.

---

## Future Improvements

Potential enhancements include:

* Database-backed unit management
* Dynamic unit configuration
* API versioning
* Serilog structured logging
* Docker containerization
* CI/CD pipeline integration
* Integration testing
* Authentication and authorization
* Caching frequently used conversions
* Support for additional conversion categories

---

## Key Features

* RESTful API Design
* Swagger/OpenAPI Documentation
* Dependency Injection
* Clean Architecture Principles
* Global Exception Handling
* Unit Tested Business Logic
* Extensible Conversion Framework
* Production-Oriented Project Structure
