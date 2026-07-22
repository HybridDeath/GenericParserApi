using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ConsoleTester
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var csv =
            """
                Imię,Wiek,Stanowisko
                Żaneta,25,Programistka
                Adam,23,Menedżer HR
                Paweł,32,Kierownik działu wdrażającego
            """;

            string csvBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(csv));

            HttpClient client = new();

            Uri localhostUri = new("http://localhost:5000/api/v1/parse-content");
            var request = new
            {
                type = "CSV",
                content = csvBase64
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