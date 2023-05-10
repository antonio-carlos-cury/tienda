using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tienda.Presentation.Extensions;
using Tienda.Presentation.Models;

namespace Tienda.Presentation.Controllers
{
    [Route("pessoas")]
    public class PersonsController : CoreController<PersonFilters, List<PersonViewModel>>
    {
        public PersonsController(IOptions<AppSettings> settings) : base(settings)
        {

        }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> Index()
        {
            return await PostToApiAsync("pessoas", "listar", PersonFilters.Default);
        }
    }
}
