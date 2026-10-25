using Microsoft.AspNetCore.Mvc;

namespace ConectaRH.Web.Controllers;

public class HomeController : BaseController
{
    // Plantilla 1: pantalla de inicio
    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated == true) return IrAInicio();
        return View();
    }

    public IActionResult Error() => View();
}
