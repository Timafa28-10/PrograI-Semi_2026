using System.Security.Claims;
using ConectaRH.Business.Models;
using ConectaRH.Business.Services;
using ConectaRH.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConectaRH.Web.Controllers;

public class CuentaController : BaseController
{
    private readonly AuthService _auth;
    public CuentaController(AuthService auth) => _auth = auth;

    // Plantilla 2: iniciar sesión
    [HttpGet]
    public IActionResult Login(string? returnUrl)
    {
        if (User.Identity?.IsAuthenticated == true) return IrAInicio();
        ViewBag.ReturnUrl = returnUrl;
        return View(new LoginModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel m, string? returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        if (!ModelState.IsValid) return View(m);

        var (usuario, error) = await _auth.ValidarAsync(m.Correo, m.Contrasena);
        if (usuario == null)
        {
            ModelState.AddModelError("", error ?? "No se pudo iniciar sesión.");
            return View(m);
        }

        await IniciarSesionAsync(usuario, m.Recordarme);
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return LocalRedirect(returnUrl);
        return IrAInicio();
    }

    // Plantilla 3: crear cuenta
    [HttpGet]
    public IActionResult Registro(string? tipo)
    {
        if (User.Identity?.IsAuthenticated == true) return IrAInicio();
        return View(new RegistroModel { TipoUsuario = tipo == NombresRol.Empresa ? NombresRol.Empresa : NombresRol.Candidato });
    }

    [HttpPost]
    public async Task<IActionResult> Registro(RegistroModel m)
    {
        if (!ModelState.IsValid) return View(m);

        var r = await _auth.RegistrarAsync(m);
        if (!r.Exito)
        {
            ModelState.AddModelError("", r.Error ?? "No se pudo crear la cuenta.");
            return View(m);
        }

        var (usuario, _) = await _auth.ValidarAsync(m.Correo, m.Contrasena);
        if (usuario != null)
        {
            await IniciarSesionAsync(usuario, true);
            Mensaje("¡Cuenta creada! Completa tu información.");
            return usuario.Rol.NombreRol == NombresRol.Empresa
                ? RedirectToAction("EditarPerfil", "Empresa")
                : RedirectToAction("EditarPerfil", "Candidato");
        }
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult Recuperar() => View();

    [HttpPost, Authorize]
    public async Task<IActionResult> Salir()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    private async Task IniciarSesionAsync(Usuario u, bool recordar)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, u.IdUsuario.ToString()),
            new(ClaimTypes.Name, u.NombreUsuario),
            new(ClaimTypes.Email, u.Correo),
            new(ClaimTypes.Role, u.Rol.NombreRol)
        };
        var id = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id),
            new AuthenticationProperties { IsPersistent = recordar });
    }
}
