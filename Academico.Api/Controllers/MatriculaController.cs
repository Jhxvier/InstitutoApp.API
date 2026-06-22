using Microsoft.AspNetCore.Mvc;

namespace Academico.Api.Controllers
{
    [ApiController]
    [Route("api/Matricula")]
    public class MatriculaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
