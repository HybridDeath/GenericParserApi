using GenericParserApi.Models;
using GenericParserApi.Parsers;

namespace GenericParserApi.Services
{
    public class ContentParserService
    {
        private readonly CsvParser _csvParser;
        private readonly InternalJsonParser _jsonParser;

        public ContentParserService()
        {
            _csvParser = new CsvParser();
            _jsonParser = new InternalJsonParser();
        }


        public object Parse(ContentType type, string content)
        {
            return type switch
            {
                ContentType.CSV => 
                    _csvParser.Parse(content),

                ContentType.INTERNAL_JSON =>
                    _jsonParser.Parse(content),

                _ =>
                    throw new NotSupportedException()
            };
        }
    }
}