using CsvHelper;
using GenericParserApi.Models;
using System.Globalization;

namespace GenericParserApi.Parsers
{
    public class CsvParser : IContentParser
    {
        /// <summary>
        /// Parses CSV content into a list of dictionaries, where each dictionary represents a row with column headers as keys.
        /// </summary>
        /// <param name="content">CSV content to be parsed.</param>
        /// <returns>A list of dictionaries representing the parsed CSV data.</returns>
        public ParserResult Parse(string content)
        {
            try
            {
                using var reader = new StringReader(content);
                using var csv = new CsvReader(
                    reader,
                    CultureInfo.InvariantCulture
                );

                var records = new List<Dictionary<string, string>>();

                if (!csv.Read())
                {
                    throw new CsvParseException(
                        "CSV content is empty."
                    );
                }

                csv.ReadHeader();

                var headers = csv.HeaderRecord ??
                    throw new CsvParseException(
                        "CSV does not contain headers."
                    );

                while (csv.Read())
                {
                    if (csv.Parser.Count != headers.Length)
                    {
                        throw new CsvParseException(
                            $"CSV row contains {csv.Parser.Count} fields but expected {headers.Length}.",
                            csv.Parser.RawRecord
                        );
                    }

                    var row = new Dictionary<string, string>();

                    foreach (var header in headers)
                    {
                        row[header] = csv.GetField(header) ?? string.Empty;
                    }

                    records.Add(row);
                }

                return new ParserResult
                {
                    Count = records.Count,
                    Data = records
                };
            }
            catch (CsvHelperException ex)
            {
                throw new CsvParseException(
                    "Invalid CSV format.",
                    ex
                );
            }
        }
    }
}