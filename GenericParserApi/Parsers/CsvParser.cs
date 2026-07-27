namespace GenericParserApi.Parsers
{
    public class CsvParser : IContentParser
    {
        /// <summary>
        /// Parses CSV content into a list of dictionaries, where each dictionary represents a row with column headers as keys.
        /// </summary>
        /// <param name="content">CSV content to be parsed.</param>
        /// <returns>A list of dictionaries representing the parsed CSV data.</returns>
        public object Parse(string content)
        {
            // Na początek dzielimy zawartość na linie, usuwając puste linie.
            var lines = content
                .Split(
                    Environment.NewLine,
                    StringSplitOptions.RemoveEmptyEntries
                );

            // Jeśli nie ma wystarczającej liczby linii, zwracamy ekwiwalent pustej listy z tym T.
            if (lines.Length < 2)
            {
                return new List<Dictionary<string, string>>();
            }

            // Pierwsza linia zawiera nagłówki, zwraca string[]?, musi być nullable, bo również może być pusta.
            // Można to wykonać bez LINQ, ale to rozwiązanie jest bardziej czytelne.
            var headers = lines[0]
                .Split(',')
                .Select(x => x.Trim())
                .ToArray();

            // Tworzenie samej listy wynikowej ze słownikami.
            // Na tym etapie jest to uproszczona implementacja dynamicznego rekordu.
            var result = new List<Dictionary<string, string>>();

            // Foreach każdej linii. Trzeba uważać na 1 indeks, bo to nagłówki, a nie dane.
            foreach (var line in lines.Skip(1))
            {
                // Rozdzielamy na podstawie przecinka, i zwracamy string[]?
                var values = line
                    .Split(',')
                    .Select(x => x.Trim())
                    .ToArray();

                // Jeśli ilość danych w krotce nie zgadza się z ilością nagłówków, to rzucamy wyjatek.
                // Ponieważ jest to operacja na danych, nie wykonujemy jej w walidatorze tylko tu.
                if (values.Length != headers.Length)
                {
                    throw new CsvParseException(
                        $"CSV row has {values.Length} columns but expected {headers.Length}.", new($"Failed at line: {line.Trim()}")
                    );
                }

                var row = new Dictionary<string, string>();

                // Wrzucamy do dicta, i potem do listy.
                for (int i = 0; i < headers.Length; i++)
                {
                    row[headers[i]] = values[i];
                }

                result.Add(row);
            }


            return result;
        }
    }
}