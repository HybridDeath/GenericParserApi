namespace GenericParserApi.Parsers
{
    public interface IContentParser
    {
        object Parse(string content);
    }
}
