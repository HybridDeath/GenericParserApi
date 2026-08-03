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
            #region Builder
            var builder = WebApplication.CreateBuilder(args);

            // ZALECANE: Przeniesienie konfiguracji portu do launchSettings.json
            // Ktoś może mieć już zaprojektowaną własną architekturę testującą, w której automatycznie modyfikuje launchSettings.json pod swoje wymagania.
            builder.WebHost.UseUrls("http://localhost:5000");
            builder.Services.ConfigureHttpJsonOptions(
                options =>
                {
                    // Ważna opcja, aby nie wysyłać nullable w response.
                    options.SerializerOptions.DefaultIgnoreCondition =
                        JsonIgnoreCondition.WhenWritingNull;

                    // Dwie poniższe linie są wymagane aby enumy były serializowane jako stringi w response.
                    options.SerializerOptions.Converters.Add(
                        new JsonStringEnumConverter<ContentType>()
                    );

                    options.SerializerOptions.Converters.Add(
                        new JsonStringEnumConverter<ParseStatus>()
                    );

                    // Można wyłączyć ale pomaga w testach.
                    options.SerializerOptions.WriteIndented = true;
                }
            );
            #endregion

            #region Application
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

                // ZALECANE: Przeniesienie new ContentParserService() do DI.
                // Dla tego przykładu nie jest to konieczne.
                // Ja osobiście wolałbym singleton, bo nasz ani nie trzyma stanu ani nie ma zależności.
                // Przykładowo dla rozwiązań z wykorzystaniem baz danych, mieliśmy coś w stylu new MainService(new DatabaseService()).
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

                // ZALECANA: Abstrakcja obu klas w coś typu ParseException, aby nie powtarzać kodu, i uniezależnić ich implementacje.
                // Dla CSV jest to mało ważne, ale dla JSON pomagałoby to w czytaniu błędu.
                // Ale mówiąc w skrócie, Program.cs nie powinien wiedzieć o istnieniu CsvParseException oraz JsonParseException, bo to już "wchodzenie piętro wyżej".
                catch (CsvParseException ex)
                {
                    return Results.BadRequest(new
                    {
                        error = ex.Message,
                        details = ex.InnerException?.Message
                    });
                }
                catch (JsonParseException ex)
                {
                    return Results.BadRequest(new
                    {
                        error = ex.Message,
                        details = ex.InnerException?.Message
                    });
                }
            });

            app.Run();
            #endregion
        }
    }
}