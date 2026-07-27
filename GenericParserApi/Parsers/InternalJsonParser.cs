using System.Text.Json;

namespace GenericParserApi.Parsers
{
    public class InternalJsonParser : IContentParser
    {
        public object Parse(string content)
        {
            using var document = JsonDocument.Parse(content);

            return document.RootElement;
        }
    }
}
