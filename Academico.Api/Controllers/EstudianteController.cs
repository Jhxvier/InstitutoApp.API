using Microsoft.AspNetCore.Mvc;

namespace Academico.Api.Controllers
{
    [ApiController]
    [Route("api/Estudiante")]
    public class EstudianteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
