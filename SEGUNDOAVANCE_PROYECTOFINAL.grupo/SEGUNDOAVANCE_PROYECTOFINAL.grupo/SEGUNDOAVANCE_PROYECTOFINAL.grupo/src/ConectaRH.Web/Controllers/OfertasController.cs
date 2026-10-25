using ConectaRH.Business.Services;
using ConectaRH.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConectaRH.Web.Controllers;

public class OfertasController : BaseController
{
    private const long MaxCvBytes = 3 * 1024 * 1024; // 3 MB
    private static readonly string[] ExtensionesCv = { ".pdf", ".doc", ".docx" };

    private readonly OfertaService _ofertas;
    private readonly CandidatoService _candidatos;
    private readonly PostulacionService _postulaciones;
    private readonly IWebHostEnvironment _env;

    public OfertasController(OfertaService ofertas, CandidatoService candidatos, PostulacionService postulaciones, IWebHostEnvironment env)
    {
        _ofertas = ofertas;
        _candidatos = candidatos;
        _postulaciones = postulaciones;
        _env = env;
    }

    public static string CarpetaCv(IWebHostEnvironment env) => Path.Combine(env.ContentRootPath, "App_Data", "cv");

    // Plantilla 7: detalle de la oferta
    [Authorize(Roles = NombresRol.Candidato + "," + NombresRol.Empresa)]
    public async Task<IActionResult> Detalle(int id)
    {
        var o = await _ofertas.ObtenerAsync(id);
        if (o == null) return NotFound();

        var esCandidato = User.IsInRole(NombresRol.Candidato);
        ViewBag.EsCandidato = esCandidato;
        ViewBag.YaPostulado = false;
        if (esCandidato)
        {
            var c = await _candidatos.ObtenerPorUsuarioAsync(UsuarioId);
            if (c != null) ViewBag.YaPostulado = await _postulaciones.YaPostuladoAsync(c.IdCandidato, id);
        }
        return View(o);
    }

    // Plantilla 8: postular a oferta
    [HttpGet, Authorize(Roles = NombresRol.Candidato)]
    public async Task<IActionResult> Postular(int id)
    {
        var o = await _ofertas.ObtenerAsync(id);
        if (o == null) return NotFound();
        var c = await _candidatos.ObtenerPorUsuarioAsync(UsuarioId);
        if (c != null && await _postulaciones.YaPostuladoAsync(c.IdCandidato, id))
        {
            Mensaje("Ya te postulaste a esta oferta.", true);
            return RedirectToAction("Postulaciones", "Candidato");
        }
        return View(o);
    }

    [HttpPost, Authorize(Roles = NombresRol.Candidato)]
    public async Task<IActionResult> Postular(int id, IFormFile? cv, string? mensaje)
    {
        var o = await _ofertas.ObtenerAsync(id);
        if (o == null) return NotFound();
        var c = await _candidatos.ObtenerPorUsuarioAsync(UsuarioId);
        if (c == null) return NotFound();

        string? ext = null;
        if (cv == null || cv.Length == 0)
            ModelState.AddModelError("", "Adjunta tu CV para enviar la postulación.");
        else
        {
            ext = Path.GetExtension(cv.FileName).ToLowerInvariant();
            if (!ExtensionesCv.Contains(ext)) ModelState.AddModelError("", "El CV debe ser PDF, DOC o DOCX.");
            else if (cv.Length > MaxCvBytes) ModelState.AddModelError("", "El CV no puede superar los 3 MB.");
        }
        if (!ModelState.IsValid) return View(o);

        var r = await _postulaciones.PostularAsync(c.IdCandidato, id, mensaje);
        if (!r.Exito)
        {
            Mensaje(r.Error!, true);
            return RedirectToAction("Postulaciones", "Candidato");
        }

        // El CV se guarda en App_Data/cv/{id_postulacion}.ext (fuera de wwwroot: no es público)
        var carpeta = CarpetaCv(_env);
        Directory.CreateDirectory(carpeta);
        await using (var fs = System.IO.File.Create(Path.Combine(carpeta, $"{r.Id}{ext}")))
            await cv!.CopyToAsync(fs);

        Mensaje("¡Postulación enviada! Puedes consultar su estado aquí.");
        return RedirectToAction("Postulaciones", "Candidato");
    }
}
