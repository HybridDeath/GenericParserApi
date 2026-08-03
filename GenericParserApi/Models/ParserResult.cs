namespace GenericParserApi.Models
{
    /// <summary>
    /// Internal DTO used to return the result of parsing a file. It contains the count of processed records and the parsed data.
    /// </summary>
    public record ParserResult
    {
        /// <summary>
        /// Gets the count of processed records.
        /// </summary>
        public int Count { get; init; }
        /// <summary>
        /// Gets the parsed data. Output may very depending on your parser implementation.
        /// </summary>
        public object? Data { get; init; }
    }
}
