using GenericParserApi.Models;

namespace GenericParserApi.Validators
{
    public static class RequestValidator
    {
        // ZALECANE: Zakładamy tylko, że content jest !nullable. Nigdzie nie sprawdzamy jego długości, co powoduje lukę w zabezpieczeniach dla ataków DoS.
        // Ktoś mógłby łatwo zawiesić API wysyłając content >1MB
        public static string? Validate(ParseRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Content))
            {
                return "Content was null/empty.";
            }

            if (!Enum.IsDefined(request.Type))
            {
                return "Unsupported request type.";
            }

            return null;
        }
    }
}
