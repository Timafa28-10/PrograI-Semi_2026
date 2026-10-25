using System.Security.Claims;
using ConectaRH.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ConectaRH.Web.Controllers;

public abstract class BaseController : Controller
{
    protected int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>Envía al usuario a su pantalla inicial según el rol (Lógica de funcionamiento).</summary>
    protected IActionResult IrAInicio()
    {
        if (User.IsInRole(NombresRol.Administrador)) return RedirectToAction("Inicio", "Admin");
        if (User.IsInRole(NombresRol.Empresa)) return RedirectToAction("Inicio", "Empresa");
        if (User.IsInRole(NombresRol.Candidato)) return RedirectToAction("Inicio", "Candidato");
        return RedirectToAction("Index", "Home");
    }

    protected void Mensaje(string texto, bool error = false) =>
        TempData[error ? "Error" : "Ok"] = texto;
}
