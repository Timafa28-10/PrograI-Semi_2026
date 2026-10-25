using ConectaRH.Business.Models;
using ConectaRH.Business.Services;
using ConectaRH.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConectaRH.Web.Controllers;

[Authorize(Roles = NombresRol.Administrador)]
public class AdminController : BaseController
{
    private readonly AdminService _admin;
    private readonly EmpresaService _empresas;
    private readonly OfertaService _ofertas;
    private readonly AuthService _auth;
    private readonly PostulacionService _postulaciones;

    public AdminController(AdminService admin, EmpresaService empresas, OfertaService ofertas, AuthService auth, PostulacionService postulaciones)
    {
        _admin = admin;
        _empresas = empresas;
        _ofertas = ofertas;
        _auth = auth;
        _postulaciones = postulaciones;
    }

    // Inicio
    public async Task<IActionResult> Inicio() => View(await _admin.ObtenerResumenAsync());

    // Reportes
    public async Task<IActionResult> Reportes() => View(await _admin.ObtenerResumenAsync());

    // Plantilla 10: lista de usuarios (Gestionar Usuarios)
    public async Task<IActionResult> Usuarios(string? q, string? rol)
    {
        ViewBag.Q = q;
        ViewBag.Rol = rol;
        return View(await _admin.ListarUsuariosAsync(q, rol));
    }

    // Plantilla 9: perfil de usuario
    public async Task<IActionResult> PerfilUsuario(int id, string tab = "general")
    {
        var u = await _admin.ObtenerUsuarioAsync(id);
        if (u == null) return NotFound();
        ViewBag.Tab = tab is "actividad" or "configuracion" ? tab : "general";
        if (u.Candidato != null)
            ViewBag.Postulaciones = await _postulaciones.ListarPorCandidatoAsync(u.Candidato.IdCandidato);
        if (u.Empresa != null)
            ViewBag.Ofertas = await _ofertas.ListarPorEmpresaAsync(u.Empresa.IdEmpresa);
        return View(u);
    }

    [HttpPost]
    public async Task<IActionResult> CambiarEstadoUsuario(int id, string? volver)
    {
        var r = await _admin.CambiarEstadoUsuarioAsync(id, UsuarioId);
        Mensaje(r.Exito ? "Estado de la cuenta actualizado." : r.Error!, !r.Exito);
        if (volver == "usuarios") return RedirectToAction(nameof(Usuarios));
        return RedirectToAction(nameof(PerfilUsuario), new { id, tab = "configuracion" });
    }

    [HttpGet]
    public IActionResult NuevoUsuario(string? tipo) =>
        View(new RegistroModel { TipoUsuario = tipo is NombresRol.Empresa or NombresRol.Administrador ? tipo : NombresRol.Candidato });

    [HttpPost]
    public async Task<IActionResult> NuevoUsuario(RegistroModel m)
    {
        if (!ModelState.IsValid) return View(m);
        var r = await _auth.RegistrarAsync(m, permitirAdmin: true);
        if (!r.Exito) { ModelState.AddModelError("", r.Error!); return View(m); }
        Mensaje("Usuario creado.");
        return RedirectToAction(nameof(PerfilUsuario), new { id = r.Id });
    }

    // Plantilla 11: roles y permisos
    public async Task<IActionResult> Roles(string? q)
    {
        ViewBag.Q = q;
        return View(await _admin.ListarRolesAsync(q));
    }

    // Plantilla 6: gestión de clientes (empresas)
    public async Task<IActionResult> Clientes(string? q)
    {
        ViewBag.Q = q;
        return View(await _empresas.ListarAsync(q));
    }

    // Gestionar Ofertas
    public async Task<IActionResult> Ofertas(string? q)
    {
        ViewBag.Q = q;
        return View(await _ofertas.ListarTodasAsync(q));
    }

    [HttpPost]
    public async Task<IActionResult> CambiarEstadoOferta(int id, string estado)
    {
        var r = await _ofertas.CambiarEstadoAsync(id, estado, null);
        Mensaje(r.Exito ? "Estado de la oferta actualizado." : r.Error!, !r.Exito);
        return RedirectToAction(nameof(Ofertas));
    }

    // Plantilla 12: configuración de la empresa
    [HttpGet]
    public async Task<IActionResult> Configuracion(int? id, string tab = "general", bool editar = false)
    {
        var lista = await _empresas.ListarAsync(null);
        var e = id.HasValue ? await _empresas.ObtenerAsync(id.Value) : lista.FirstOrDefault();
        ViewBag.Tab = tab is "contacto" or "ubicacion" or "notificaciones" or "seguridad" ? tab : "general";
        ViewBag.Editar = editar;
        ViewBag.Empresas = lista;
        return View(e);
    }

    [HttpPost]
    public async Task<IActionResult> Configuracion(int id, DatosEmpresaModel m)
    {
        if (!ModelState.IsValid)
        {
            Mensaje("Revisa los datos: el nombre de la empresa es obligatorio.", true);
            return RedirectToAction(nameof(Configuracion), new { id, editar = true });
        }
        var r = await _empresas.ActualizarAsync(id, m);
        Mensaje(r.Exito ? "Información actualizada." : r.Error!, !r.Exito);
        return RedirectToAction(nameof(Configuracion), new { id });
    }
}
