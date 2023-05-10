using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tienda.Presentation.Extensions;
using Tienda.Presentation.Models.Core;

namespace Tienda.Presentation.Controllers
{
    public abstract class CoreController<TFilter, TResponse> : Controller
    {
        protected readonly AppSettings _settings;
        public static HttpClient _client = new(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.All, UseProxy = false }) { Timeout = TimeSpan.FromMinutes(2) };
        public DefaultApiResponse<TFilter, TResponse>? _response;
        internal string ViewName { get; set; }

        public CoreController(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }



        public virtual async Task<IActionResult> PostToApiJsonAsync(string controller, string action, object body)
        {
            var response = await FromApi(Method.Post, controller, action, body.ToStringContent(), true);
            return response;
        }

        public virtual async Task<IActionResult> PostToApiAsync(string controller, string action, object body)
        {
            return await PostToApiAsync(controller, action, body.ToStringContent());
        }
        public virtual async Task<IActionResult> PostToApiAsync(string controller, string action, StringContent bodyContent)
        {
            return await FromApi(Method.Post, controller, action, bodyContent);
        }

        public async Task<IActionResult> PostAsync(string controller, string action, object body)
        {
            return await PostToApiAsync(controller, action, body.ToStringContent());
        }
        public async Task<IActionResult> GetAsync(string controller, string action)
        {
            return await FromApi(Method.Get, controller, action, null);
        }

        public async Task<IActionResult> DeleteAsync(string controller, string action, object body)
        {
            string url = ApiUrl(controller, action);
            HttpRequestMessage request = new()
            {
                Content = body.ToStringContent(),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url)
            };
            var response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return await ErrorResponse(response, url, "DELETE", body.ToStringContent());

            var model = await response.Content.ReadAsStringAsync();
            _response = model.ToType<DefaultApiResponse<TFilter, TResponse>>();

            return Json(_response);
        }

        #region Config methods
        private async Task<IActionResult> FromApi(Method method, string controller, string action, StringContent? bodyContent, bool isJson = false)
        {
            string url = ApiUrl(controller, action);
            var response = method == Method.Get ? await _client.GetAsync(url) : await _client.PostAsync(url, bodyContent);

            if (!response.IsSuccessStatusCode)
                return await ErrorResponse(response, url, method.ToString(), bodyContent);

            var model = await response.Content.ReadAsStringAsync();
            _response = model.ToType<DefaultApiResponse<TFilter, TResponse>>();

            return isJson ? Json(_response) : ViewName.IsNull() ? View(_response) : RedirectToAction(ViewName);
        }

        private async Task<IActionResult> ErrorResponse(HttpResponseMessage response, string url, string verb, StringContent requestMessage)
        {
            string bodyResponse = await response.Content.ReadAsStringAsync();

            ErrorResponseModel error = new()
            {
                RequestMessage = requestMessage.GetDictionaryFromRequest(),
                RequestUrl = url,
                Verb = verb,
                ResponseHeader = response.Headers.ToDictionary(),
                ResponseBody = bodyResponse,
                ResponseMessage = $"({(int)response.StatusCode} - {response.ReasonPhrase})"
            };

            return View("Error", error);
        }

        private string ApiUrl(string controller, string action)
        {
            var url = string.Concat(_settings.DefaultApiUri.TrimEnd(new char[] { '/' }), "/", controller, "/", action);
            return url;
        }

        private enum Method { Get, Post }
        #endregion
    }
}
