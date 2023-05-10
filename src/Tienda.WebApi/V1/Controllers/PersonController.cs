using Microsoft.AspNetCore.Mvc;
using Tienda.Domain.Entities;
using Tienda.Domain.Interfaces;
using Tienda.WebApi.Controllers;

namespace Tienda.WebApi.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/pessoas")]
    public class PersonController : CoreApiController<PersonFilter, IEnumerable<PersonEntity>>
    {
        protected readonly IPersonService _personService;
        public PersonController(IConfiguration configuration, IPersonService personService) : base(configuration){ _personService = personService; }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> Index()
        {
            try
            {
                return CustomResponse(await _personService.SearchAsync(PersonFilter.Default));
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        [Route("listar")]
        public async Task<IActionResult> Index([FromBody] PersonFilter filter)
        {
            try { 
                return CustomResponse(await _personService.SearchAsync(filter));
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }        

        [HttpPost]
        [Route("inserir")]
        public async Task<IActionResult> Insert([FromBody] PersonEntity entity)
        {
            try
            {
                _ = await _personService.InsertAsync(entity);
                return CustomResponse((PersonFilter.Default, new List<PersonEntity>() { entity }));
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        [Route("editar")]
        [DisableRequestSizeLimit()]
        public async Task<IActionResult> Edit([FromBody] PersonEntity entity)
        {
            try
            {
                return CustomResponse(await _personService.UpdateAsync(entity));
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }
    }
}
