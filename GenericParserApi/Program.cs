using GenericParserApi.Models;
using GenericParserApi.Parsers;
using GenericParserApi.Services;
using GenericParserApi.Validators;
using System.Text.Json.Serialization;

namespace GenericParserApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://localhost:5000");
            builder.Services.ConfigureHttpJsonOptions(
                options =>
                {
                    // Ważna opcja, aby nie wysyłać nullable w response.
                    options.SerializerOptions.DefaultIgnoreCondition =
                        JsonIgnoreCondition.WhenWritingNull;

                    options.SerializerOptions.Converters.Add(
                        // W tym wypadku T jest ContentType. Zapewnia większą elastyczność.
                        new JsonStringEnumConverter<ContentType>()
                    );

                    options.SerializerOptions.Converters.Add(
                        // A w tym wypadku gwarantuje lepszą serializację.
                        new JsonStringEnumConverter<ParseStatus>()
                    );

                    // Można wyłączyć ale pomaga w testach.
                    options.SerializerOptions.WriteIndented = true;
                }
            );

            var app = builder.Build();
            app.MapPost("/api/v1/parse-content", (ParseRequest request) =>
            {
                var validationResult = RequestValidator.Validate(request);
                if (validationResult != null)
                {
                    return Results.BadRequest(new
                    {
                        error = validationResult
                    });
                }

                string decodedContent;
                try
                {
                    decodedContent = ContentDecoder.Decode(request.Content);
                }
                catch (FormatException)
                {
                    return Results.BadRequest(new
                    {
                        error = "Invalid base64 content."
                    });
                }

                var parserService = new ContentParserService();
                try
                {
                    var parserResult = parserService.Parse(
                        request.Type,
                        decodedContent
                    );

                    return Results.Ok(
                        new ParseResponse
                        {
                            Status = ParseStatus.Success,
                            ProcessedCount = parserResult.Count,
                            Data = parserResult.Data
                        }
                    );
                }
                catch (CsvParseException ex)
                {
                    return Results.BadRequest(new
                    {
                        error = ex.Message,
                        details = ex.InnerException?.Message
                    });
                }
            });

            app.Run();
        }
    }
}