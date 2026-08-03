using GenericParserApi.Models;
using GenericParserApi.Parsers;

namespace GenericParserApi.Services
{
    public class ContentParserService
    {
        // ZALECANE: Przeniesienie obu parserów do DI i dodawanie ich do konstruktora.
        // DODATKOWO: Dodanie interfejsu dla zamiany ContentType na dany parser, aby nie używać switchexpr.
        private readonly CsvParser _csvParser;
        private readonly InternalJsonParser _jsonParser;

        public ContentParserService()
        {
            _csvParser = new CsvParser();
            _jsonParser = new InternalJsonParser();
        }

        public ParserResult Parse(ContentType type, string content)
        {
            return type switch
            {
                ContentType.CSV => _csvParser.Parse(content),
                ContentType.INTERNAL_JSON =>_jsonParser.Parse(content),

                // Teoretycznie nigdy nie powinien wystąpić, bo wcześniej dokonujemy walidację samego JSON, gdzie Enum.IsDefined().
                _ => throw new NotSupportedException()
            };
        }
    }
}