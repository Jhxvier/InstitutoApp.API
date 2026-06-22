using Microsoft.AspNetCore.Mvc;

namespace Academico.Api.Controllers
{
    [ApiController]
    [Route("api/Curso")]
    public class CursoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
