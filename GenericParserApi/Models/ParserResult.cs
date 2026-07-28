namespace GenericParserApi.Models
{
    public record ParserResult
    {
        public int Count { get; init; }
        public object Data { get; init; } = default!;
    }
}
