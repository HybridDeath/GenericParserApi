namespace GenericParserApi.Models
{
    public record ParseResponse
    {
        public ParseStatus Status { get; init; }

        public int ProcessedCount { get; init; }

        public object? Data { get; init; }

        public string? Error { get; init; }
    }
}