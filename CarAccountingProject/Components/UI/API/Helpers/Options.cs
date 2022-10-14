using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace API.Helpers
{
    public static class Options
    {
        public static JsonSerializerOptions JsonOptions()
        {
            return new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
        }
    }
}