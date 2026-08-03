using GenericParserApi.Models;
using System.Text.Json;

namespace GenericParserApi.Parsers
{
    public class InternalJsonParser : IContentParser
    {
        public ParserResult Parse(string content)
        {
            // Dodatkowe sprawdzenie, mimo iż nasz walidator już sprawdza nasz content.
            // Zapobiega wystąpieniu NullArgumentException.
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new JsonParseException("JSON content is empty.");
            }

            try
            {
                using var document = JsonDocument.Parse(content);

                int count = document.RootElement.ValueKind switch
                {
                    JsonValueKind.Array => document.RootElement.GetArrayLength(),
                    JsonValueKind.Object => 1,

                    _ => throw new JsonParseException("INTERNAL_JSON must contain an object or an array.")
                };

                return new ParserResult
                {
                    Count = count,
                    Data = document.RootElement.Clone()
                };
            }
            catch (JsonException ex)
            {
                // ZALECANE: Przepakowanie JsonException w niestandardowy wyjątek, który przepakuje te dane na lepszą strukturę bardziej przyjazną dla użytkownika.
                // Ponieważ obecnie jest to coś w stylu: "':' is invalid...", a wolimy coś w rodzaju reprezentacji tego co dokładnie jest nie tak z danym JSONem, na wzór np. interpretera Python, który mówi: "SyntaxError: invalid syntax" i podaje dokładnie, gdzie jest problem.
                throw new JsonParseException(
                    "Invalid INTERNAL_JSON format.",
                    ex
                );
            }
        }
    }
}