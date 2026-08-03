using System.Text;

namespace GenericParserApi.Services
{
    public static class ContentDecoder
    {
        /// <summary>
        /// Decodes a base64-encoded string into its original UTF-8 string representation.
        /// </summary>
        /// <param name="base64Content">The base64-encoded string to decode.</param>
        /// <returns>The decoded UTF-8 string.</returns>
        public static string Decode(string base64Content)
        {
            byte[] bytes = Convert.FromBase64String(base64Content);

            return Encoding.UTF8.GetString(bytes);
        }
    }
}
