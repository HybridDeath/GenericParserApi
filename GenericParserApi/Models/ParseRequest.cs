namespace GenericParserApi.Models
{
    /// <summary>
    /// Internal request DTO used to pass its fields to the parser.
    /// </summary>
    public record ParseRequest
    {
        /// <summary>
        /// Gets the content type of the data to be parsed.
        /// </summary>
        public ContentType Type { get; init; }

        /// <summary>
        /// Gets the content to be parsed.
        /// </summary>
        // Utrudniamy przekazanie null za pomocą string.Empty.
        public string Content { get; init; } = string.Empty;
    }
}