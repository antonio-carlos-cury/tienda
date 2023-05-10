using Tienda.Presentation.Extensions;

namespace Tienda.Presentation.Models.Core
{
    public class ErrorResponseModel
    {
        public string Verb { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseBody { get; set; }
        public Dictionary<string, string> ResponseHeader { get; set; }
        public Dictionary<string, string> RequestMessage { get; set; }
        public string RequestUrl { get; set; }

        internal async static Task<ErrorResponseModel> Instance(HttpClient client, HttpResponseMessage response, Exception ex)
        {
            ErrorResponseModel error = new()
            {
                RequestMessage = response.ToStringContent().GetDictionaryFromRequest(),
                RequestUrl = client.BaseAddress?.AbsoluteUri ?? "NULL",
                Verb = "GET",
                ResponseHeader = response.Headers.ToDictionary(),
                ResponseBody = await response.Content.ReadAsStringAsync(),
                ResponseMessage = $"({(int)response.StatusCode}) - {response.ReasonPhrase} [{ex}]"
            };

            return error;
        }
    }
}
