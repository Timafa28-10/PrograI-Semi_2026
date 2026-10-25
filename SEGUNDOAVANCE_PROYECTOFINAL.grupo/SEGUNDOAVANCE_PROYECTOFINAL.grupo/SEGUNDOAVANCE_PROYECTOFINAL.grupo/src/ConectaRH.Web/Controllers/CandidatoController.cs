using ConectaRH.Business.Models;
using ConectaRH.Business.Services;
using ConectaRH.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConectaRH.Web.Controllers;

[Authorize(Roles = NombresRol.Candidato)]
public class CandidatoController : BaseController
{
    private readonly CandidatoService _candidatos;
    private readonly OfertaService _ofertas;
    private readonly PostulacionService _postulaciones;

    public CandidatoController(CandidatoService candidatos, OfertaService ofertas, PostulacionService postulaciones)
    {
        _candidatos = candidatos;
        _ofertas = ofertas;
        _postulaciones = postulaciones;
    }

    private Task<Candidato?> Actual() => _candidatos.ObtenerPorUsuarioAsync(UsuarioId);

    // Inicio: resumen del candidato
    public async Task<IActionResult> Inicio()
    {
        var c = await Actual();
        if (c == null) return NotFound();
        ViewBag.Postulaciones = await _postulaciones.ListarPorCandidatoAsync(c.IdCandidato);
        ViewBag.Recientes = (await _ofertas.BuscarPublicadasAsync(null)).Take(5).ToList();
        return View(c);
    }

    // Plantilla 4: Mi perfil (Datos personales | Formación académica | Experiencia laboral)
    public async Task<IActionResult> Perfil(string tab = "datos")
    {
        var c = await Actual();
        if (c == null) return NotFound();
        ViewBag.Tab = tab is "formacion" or "experiencia" ? tab : "datos";
        return View(c);
    }

    [HttpGet]
    public async Task<IActionResult> EditarPerfil()
    {
        var c = await Actual();
        if (c == null) return NotFound();
        return View(new DatosCandidatoModel
        {
            NombreCompleto = c.NombreCompleto,
            FechaNacimiento = c.FechaNacimiento,
            Telefono = c.Telefono,
            Direccion = c.Direccion,
            ResumenProfesional = c.ResumenProfesional
        });
    }

    [HttpPost]
    public async Task<IActionResult> EditarPerfil(DatosCandidatoModel m)
    {
        if (!ModelState.IsValid) return View(m);
        var r = await _candidatos.ActualizarDatosAsync(UsuarioId, m);
        if (!r.Exito) { ModelState.AddModelError("", r.Error!); return View(m); }
        Mensaje("Perfil actualizado.");
        return RedirectToAction(nameof(Perfil));
    }

    [HttpPost]
    public async Task<IActionResult> AgregarFormacion(FormacionModel m)
    {
        var c = await Actual();
        if (c == null) return NotFound();
        if (!ModelState.IsValid) Mensaje("Completa el nivel académico y la institución.", true);
        else
        {
            var r = await _candidatos.AgregarFormacionAsync(c.IdCandidato, m);
            Mensaje(r.Exito ? "Formación agregada." : r.Error!, !r.Exito);
        }
        return RedirectToAction(nameof(Perfil), new { tab = "formacion" });
    }

    [HttpPost]
    public async Task<IActionResult> EliminarFormacion(int id)
    {
        var c = await Actual();
        if (c == null) return NotFound();
        await _candidatos.EliminarFormacionAsync(c.IdCandidato, id);
        return RedirectToAction(nameof(Perfil), new { tab = "formacion" });
    }

    [HttpPost]
    public async Task<IActionResult> AgregarExperiencia(ExperienciaModel m)
    {
        var c = await Actual();
        if (c == null) return NotFound();
        if (!ModelState.IsValid) Mensaje("Completa el puesto y la empresa.", true);
        else
        {
            var r = await _candidatos.AgregarExperienciaAsync(c.IdCandidato, m);
            Mensaje(r.Exito ? "Experiencia agregada." : r.Error!, !r.Exito);
        }
        return RedirectToAction(nameof(Perfil), new { tab = "experiencia" });
    }

    [HttpPost]
    public async Task<IActionResult> EliminarExperiencia(int id)
    {
        var c = await Actual();
        if (c == null) return NotFound();
        await _candidatos.EliminarExperienciaAsync(c.IdCandidato, id);
        return RedirectToAction(nameof(Perfil), new { tab = "experiencia" });
    }

    // Buscar Empleos
    public async Task<IActionResult> Ofertas(string? q)
    {
        ViewBag.Q = q;
        return View(await _ofertas.BuscarPublicadasAsync(q));
    }

    // Consultar Estado
    public async Task<IActionResult> Postulaciones()
    {
        var c = await Actual();
        if (c == null) return NotFound();
        return View(await _postulaciones.ListarPorCandidatoAsync(c.IdCandidato));
    }
}
