using System.Text.Json;

namespace covadis.Shared.Options
{
    public static class JsonOptions
    {
        public static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
