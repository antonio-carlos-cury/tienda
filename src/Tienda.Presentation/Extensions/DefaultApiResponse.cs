using Tienda.Presentation.Models.Core;

namespace Tienda.Presentation.Extensions
{
    public class DefaultApiResponse<F, M>
    {
        public F Filter { get; set; }
        public M Results { get; set; }

        public List<ErrorResponseModel> Errors { get; set; }
        public bool Success { get; set; }

    }
}
