using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text;

namespace Tienda.Presentation.Extensions
{
    public static class Converters
    {
        public static T? ToType<T>(this object objValue)
        {
            return JsonSerializer.Deserialize<T>(objValue.ToJson(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public static T? ToType<T>(this string jsonValue)
        {
            return JsonSerializer.Deserialize<T>(jsonValue, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public static string ToJson(this object obj, bool encode = false)
        {
            var res = JsonSerializer.Serialize(obj);

            if (encode)
            {
                var encoder = JavaScriptEncoder.Create(new TextEncoderSettings());
                res = encoder.Encode(res);
            }

            return res;
        }

        public static StringContent ToStringContent(this object bodyContent)
        {
            return new StringContent(bodyContent.ToJson(), Encoding.UTF8, "application/json");
        }

        public static Dictionary<string, string> ToDictionary(this HttpResponseHeaders headers)
        {
            var results = new Dictionary<string, string>();
            foreach (var item in headers)
            {
                if (!results.ContainsKey(item.Key))
                {
                    results.Add(item.Key, string.Join(',', item.Value));
                }
            }
            return results;
        }
        public static Dictionary<string, string> GetDictionaryFromRequest(this StringContent requestContent)
        {
            if (requestContent is null)
            {
                return new();
            }


            var results = new Dictionary<string, string>();
            byte[] bytes = requestContent.ReadAsStream().ReadFully();
            var requestBody = ReplaceInvalidText(Encoding.UTF8.GetString(bytes));
            var items = requestBody.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < items.Length; i++)
            {
                int indexOf = items[i].IndexOf(':');
                var value = items[i][(indexOf + 1)..] is null ? "null" : items[i][(indexOf + 1)..];
                if (!results.ContainsKey(items[i][..indexOf]))
                {
                    results.Add(items[i][..indexOf], value);
                }
            }

            requestBody = ReplaceInvalidText(requestContent.ToJson());
            items = requestBody.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < items.Length; i++)
            {
                int indexOf = items[i].IndexOf(':');
                string value = items[i][(indexOf + 1)..] is null ? "null" : items[i][(indexOf + 1)..];
                if (!results.ContainsKey(items[i][..indexOf]))
                {
                    results.Add(items[i][..indexOf], value);
                }
            }

            return results;

        }

        private static string ReplaceInvalidText(string jsonValue)
        {
            List<string> notAllowed = new() { "\"Key\":", "Value", "{", "\"Headers\":", "\"", "\\", "}", "[]", "[", "]" };

            foreach (string te in notAllowed)
            {
                jsonValue = jsonValue.ReplaceAll(te, string.Empty);
            }

            return jsonValue.ReplaceAll(",:", ":");
        }
        public static string ReplaceAll(this string vl, string oldStr, string newStr)
        {
            while (vl.Contains(oldStr))
            {
                vl = vl.Replace(oldStr, newStr);
            }
            return vl;
        }

        public static byte[] ReadFully(this Stream stream)
        {
            using MemoryStream ms = new();
            stream.CopyTo(ms);
            return ms.ToArray();
        }

        public static bool IsNull(this string strValue)
        {
            return strValue is null || string.IsNullOrEmpty(strValue) || string.IsNullOrWhiteSpace(strValue);
        }
    }
}
