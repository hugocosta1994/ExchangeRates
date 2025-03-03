# ExchangeRates

## Overview

ExchangeRates is a .NET 8 application that provides exchange rate information. It includes an API to fetch exchange rates from various sources, including an external API (Alpha Vantage).

## Projects

- **ExchangeRates.Models**: Contains the models for the application.
- **ExchangeRates.Data**: Contains the `ApplicationDbContext` for database interactions.
- **ExchangeRates.Services**: Contains the service for fetching exchange rates.
- **ExchangeRates.API**: Contains the API controllers for handling HTTP requests.
- **ExchangeRates.AlphaVantage**: Contains the service for fetching exchange rates from the Alpha Vantage API.

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022

### Setup

1. Clone the repository:

```bash
git clone https://github.com/hugocosta1994/ExchangeRates.git
cd ExchangeRates
```


2. Open the solution in Visual Studio 2022.

3. Restore the NuGet packages:
```bash
dotnet restore
```

4. Update the `appsettings.json` file in the `ExchangeRates.API` project with your database connection string:
```json
{
  "ConnectionStrings": {
	"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ExchangeRates;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```


5. Add your Alpha Vantage API key to the `appsettings.json` file in the `ExchangeRates.API` project:
````json
{
  "AlphaVantage": {
	"ApiKey": "your_api_key_here"
  }
}
````

### Note
For security reasons, it is recommended to store sensitive configuration data such as connection strings and API keys in Azure Key Vault instead of plain text in the `appsettings.json` file. 
You can integrate Azure Key Vault with your .NET application to securely manage and access these secrets.

### Differences in Code

To use Azure Key Vault, you need to make some changes to the code:

1. **Install the Azure Key Vault NuGet packages**:
   - `Azure.Extensions.AspNetCore.Configuration.Secrets`
   - `Azure.Identity`

2. **Update `Program.cs` to use Azure Key Vault**:


### Running the Application

1. Build the solution:
````bash
dotnet build
````

2. Run the application:
````bash
dotnet run --project ExchangeRates.API
````

3. The API will be available at `https://localhost:5001`.

## Usage

### API Endpoints

- **Get All Exchange Rates**: `GET /api/ExchangeRate/All`
- **Get Exchange Rate**: `GET /api/ExchangeRate?fromCurrency={fromCurrency}&toCurrency={toCurrency}`
- **Delete Exchange Rate**: `DELETE /api/ExchangeRate?fromCurrency={fromCurrency}&toCurrency={toCurrency}`

