using ConectaRH.Business.Models;
using ConectaRH.Business.Services;
using ConectaRH.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConectaRH.Web.Controllers;

[Authorize(Roles = NombresRol.Empresa)]
public class EmpresaController : BaseController
{
    private readonly EmpresaService _empresas;
    private readonly OfertaService _ofertas;
    private readonly PostulacionService _postulaciones;
    private readonly IWebHostEnvironment _env;

    public EmpresaController(EmpresaService empresas, OfertaService ofertas, PostulacionService postulaciones, IWebHostEnvironment env)
    {
        _empresas = empresas;
        _ofertas = ofertas;
        _postulaciones = postulaciones;
        _env = env;
    }

    private Task<Empresa?> Actual() => _empresas.ObtenerPorUsuarioAsync(UsuarioId);

    // Inicio: ofertas publicadas por la empresa
    public async Task<IActionResult> Inicio()
    {
        var e = await Actual();
        if (e == null) return NotFound();
        ViewBag.Empresa = e;
        return View(await _ofertas.ListarPorEmpresaAsync(e.IdEmpresa));
    }

    // Plantilla 5: Mi empresa (Información general | Contacto | Ubicación)
    public async Task<IActionResult> MiEmpresa(string tab = "general")
    {
        var e = await Actual();
        if (e == null) return NotFound();
        ViewBag.Tab = tab is "contacto" or "ubicacion" ? tab : "general";
        return View(e);
    }

    [HttpGet]
    public async Task<IActionResult> EditarPerfil()
    {
        var e = await Actual();
        if (e == null) return NotFound();
        return View(new DatosEmpresaModel
        {
            NombreEmpresa = e.NombreEmpresa,
            Sector = e.Sector,
            Direccion = e.Direccion,
            Telefono = e.Telefono,
            Descripcion = e.Descripcion
        });
    }

    [HttpPost]
    public async Task<IActionResult> EditarPerfil(DatosEmpresaModel m)
    {
        if (!ModelState.IsValid) return View(m);
        var e = await Actual();
        if (e == null) return NotFound();
        var r = await _empresas.ActualizarAsync(e.IdEmpresa, m);
        if (!r.Exito) { ModelState.AddModelError("", r.Error!); return View(m); }
        Mensaje("Información de la empresa actualizada.");
        return RedirectToAction(nameof(MiEmpresa));
    }

    // Publicar Ofertas
    [HttpGet]
    public IActionResult PublicarOferta() => View(new OfertaModel());

    [HttpPost]
    public async Task<IActionResult> PublicarOferta(OfertaModel m)
    {
        if (!ModelState.IsValid) return View(m);
        var e = await Actual();
        if (e == null) return NotFound();
        var r = await _ofertas.CrearAsync(e.IdEmpresa, m);
        if (!r.Exito) { ModelState.AddModelError("", r.Error!); return View(m); }
        Mensaje("Oferta publicada.");
        return RedirectToAction(nameof(Inicio));
    }

    [HttpPost]
    public async Task<IActionResult> CambiarEstadoOferta(int id, string estado)
    {
        var e = await Actual();
        if (e == null) return NotFound();
        var r = await _ofertas.CambiarEstadoAsync(id, estado, e.IdEmpresa);
        Mensaje(r.Exito ? "Estado de la oferta actualizado." : r.Error!, !r.Exito);
        return RedirectToAction(nameof(Inicio));
    }

    // Revisar Candidatos
    public async Task<IActionResult> Postulaciones(int? idOferta)
    {
        var e = await Actual();
        if (e == null) return NotFound();
        ViewBag.IdOferta = idOferta;
        ViewBag.Ofertas = await _ofertas.ListarPorEmpresaAsync(e.IdEmpresa);
        return View(await _postulaciones.ListarPorEmpresaAsync(e.IdEmpresa, idOferta));
    }

    [HttpPost]
    public async Task<IActionResult> RevisarPostulacion(int id, string estado, string? observacion, int? idOferta)
    {
        var e = await Actual();
        if (e == null) return NotFound();
        var r = await _postulaciones.CambiarEstadoAsync(id, e.IdEmpresa, estado, observacion);
        Mensaje(r.Exito ? "Postulación actualizada." : r.Error!, !r.Exito);
        return RedirectToAction(nameof(Postulaciones), new { idOferta });
    }

    public async Task<IActionResult> DescargarCv(int id)
    {
        var e = await Actual();
        if (e == null) return NotFound();
        var p = await _postulaciones.ObtenerDeEmpresaAsync(id, e.IdEmpresa); // solo ofertas propias
        if (p == null) return NotFound();

        var carpeta = OfertasController.CarpetaCv(_env);
        if (!Directory.Exists(carpeta)) return NotFound();
        var archivo = Directory.GetFiles(carpeta, $"{id}.*").FirstOrDefault();
        if (archivo == null) return NotFound();

        var tipo = Path.GetExtension(archivo).ToLowerInvariant() switch
        {
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            _ => "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
        };
        return PhysicalFile(archivo, tipo, $"CV-postulacion-{id}{Path.GetExtension(archivo)}");
    }
}
