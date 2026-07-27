namespace GenericParserApi.Models
{
    public record ParseRequest
    {
        public ContentType Type { get; init; }
        public string Content { get; init; } = string.Empty;
    }
}