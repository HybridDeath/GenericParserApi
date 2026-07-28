using GenericParserApi.Models;
using System.Text.Json;

namespace GenericParserApi.Parsers
{
    public class InternalJsonParser : IContentParser
    {
        public ParserResult Parse(string content)
        {
            // TODO
            using var document = JsonDocument.Parse(content);

            return new ParserResult
            {
                Count = int.MaxValue,
                Data = document.RootElement
            };
        }
    }
}
