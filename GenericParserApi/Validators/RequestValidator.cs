using GenericParserApi.Models;

namespace GenericParserApi.Validators
{
    public static class RequestValidator
    {
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
