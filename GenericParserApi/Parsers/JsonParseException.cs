namespace GenericParserApi.Parsers
{
    internal class JsonParseException : Exception
    {
        public JsonParseException(string? message)
            : base(message)
        {
        }

        public JsonParseException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}