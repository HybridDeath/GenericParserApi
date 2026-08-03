# GenericParserApi

GenericParserApi is an ASP.NET Core Minimal API application that receives Base64 encoded data, parses it depending on the specified content type, and returns a unified JSON response.

## Supported Formats:

```
"CSV"
"INTERNAL_JSON"
```

## Requirements:

- .NET 10 SDK (or newer)

## Running the application:

Using .NET CLI (easiest):
- Open ps or cmd,
- Navigate to the project directory:
```bash
cd GenericParserApi
```
- Run the application:
```bash
dotnet run
```

## Usage:

By default, the API runs on:
```
http://localhost:5000
```

Required header for every request:
```
Content-Type: application/json
```

The available endpoint is:
```
/api/v1/parse-content
```

The API expects a JSON payload in the request to match:
```json
{
  "type": "CSV | INTERNAL_JSON",
  "content": "..."
}
```

Where:
- `type` specifies the input format,
- `content`contains Base64 encoded raw data.

To send the request with the payload specified you can either:
- use the additional `ConsoleTester` solution, which I've included in the repository,
- write/use your own app to send your own request,
- use `curl` (Linux) or `curl.exe` (Windows)
