using GenericParserApi.Models;

namespace GenericParserApi.Parsers
{
    public interface IContentParser
    {
        ParserResult Parse(string content);
    }
}
