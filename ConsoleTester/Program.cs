using System.Net.Http.Json;
using System.Text;

namespace ConsoleTester
{
    internal class Program
    {
        static async Task Main(string[]? args)
        {
            if (args != null && args.Length > 0)
            {
                Console.WriteLine("Console arguments provided but not supported.");
            }

            #region Test CSV
            var csv =
            """
            Imię,Wiek,Stanowisko
            Żaneta,25,Programistka
            Adam,23,Menedżer HR
            Paweł,32,Kierownik działu wdrażającego
            """;

            string csvBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(csv));
            Console.WriteLine("Converted CSV to base64: " + csvBase64);
            #endregion

            #region Test JSON
            var json =
            """
            {
              "id":
              "name": "Test object",
              "active": true,
              "metadata": {
                "createdBy": "ConsoleTester",
                "tags": [
                  "test",
                  "json",
                  "internal"
                ]
              },
              "values": [
                10,
                20,
                30
              ]
            }
            """;

            string jsonBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            Console.WriteLine("Converted JSON to base64: " + jsonBase64);
            #endregion

            HttpClient client = new();

            Uri localhostUri = new("http://localhost:5000/api/v1/parse-content");
            var request = new
            {
                Type = "INTERNAL_JSON",
                Content = jsonBase64
            };

            try
            {
                var response = await client.PostAsJsonAsync(
                    localhostUri,
                    request
                );
                Console.WriteLine(response);

                Console.WriteLine();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}