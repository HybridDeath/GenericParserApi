namespace GenericParserApi.Models
{
    public record ParseResponse
    {
        public ParseStatus Status { get; init; }
        public int Count { get; init; }
        public object Data { get; init; } = new object();
    }
}