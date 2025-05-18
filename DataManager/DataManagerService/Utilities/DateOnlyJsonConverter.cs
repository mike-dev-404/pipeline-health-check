using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataManagerService.Utilities
{
    internal class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (DateOnly.TryParseExact(reader.GetString(), "M/d/yyyy", null, System.Globalization.DateTimeStyles.None, out var date))
                return date;
            throw new JsonException("Invalid date format for DateOnly.");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("M/d/yyyy"));
        }
    }
}
