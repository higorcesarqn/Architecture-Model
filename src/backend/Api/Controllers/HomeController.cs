using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// redireciona para a pagina inicial do swagger ao iniciar a api.
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous, Route("/")]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}