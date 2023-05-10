using Microsoft.AspNetCore.Mvc;
using Tienda.Domain.Interfaces;
using Tienda.Domain.Core;
using System.Net;

namespace Tienda.WebApi.Controllers
{
    [ApiController]
    public abstract class CoreApiController<TFilter, TResponse> : Controller
    {
        public readonly IConfiguration _configuration;
        public DefaultApiResponse<TFilter, TResponse> _response;
        protected Guid UserId { get; set; }
        protected bool IsAuthenticated { get; set; }

        protected CoreApiController(IConfiguration configuration)
        {
            _configuration = configuration;
            _response = new();
        }

        protected ActionResult CustomResponse((TFilter filter, TResponse results) response)
        {
            _response = new(response.filter, response.results);
            return Ok(_response);
        }

        protected ActionResult CustomPaginationResponse(TFilter filter)
        {
            return Ok(filter);
        }

        protected ActionResult ErrorResponse(string errormsg)
        {
            _response = new() { Errors = new List<string>() { errormsg } };
            return BadRequest(_response);
        }

        protected ActionResult ErrorResponse(List<string> lErrors)
        {
            _response = new() { Errors = lErrors };
            return BadRequest(_response);

        }
    }
}
