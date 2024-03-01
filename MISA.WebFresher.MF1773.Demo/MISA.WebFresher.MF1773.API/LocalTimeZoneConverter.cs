using System.Text.Json;
using System.Text.Json.Serialization;

namespace MISA.WebFresher.MF1773.Demo.API
{
    public class LocalTimeZoneConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader render, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(render.GetString() ?? DateTime.Now.ToString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (value.Kind == DateTimeKind.Unspecified)
            {
                writer.WriteStringValue(DateTime.SpecifyKind(value, DateTimeKind.Local));
            }
            else
            {
                writer.WriteStringValue(value);
            }
        }
    }
}
