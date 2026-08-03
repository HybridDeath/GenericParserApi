namespace GenericParserApi.Parsers
{
    internal class CsvParseException : Exception
    {
        public string? RawRecord { get; }

        public CsvParseException(
            string message,
            string? rawRecord = null)
            : base(message)
        {
            RawRecord = rawRecord;
        }

        public CsvParseException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }
    }
}