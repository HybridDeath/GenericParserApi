var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000");

var app = builder.Build();

app.MapPost("/api/v1/parse-content", () =>
    {
        return "Hello World!";
    }
);

app.Run();
