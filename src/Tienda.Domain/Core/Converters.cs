using System.Text.Json;

namespace Tienda.Domain.Core
{
    public static class Converters
    {
        public static T? ToType<T>(this object objValue)
        {
            return JsonSerializer.Deserialize<T>(objValue.ToJson(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public static T? ToType<T>(this string strValue)
        {
            return JsonSerializer.Deserialize<T>(strValue, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public static string ToJson(this object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
