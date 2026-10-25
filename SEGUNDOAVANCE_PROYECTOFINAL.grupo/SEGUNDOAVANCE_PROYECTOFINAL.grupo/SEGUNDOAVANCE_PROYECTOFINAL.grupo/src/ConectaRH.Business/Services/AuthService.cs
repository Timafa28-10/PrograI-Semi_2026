using ConectaRH.Business.Models;
using ConectaRH.Data;
using ConectaRH.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConectaRH.Business.Services;

/// <summary>Registro de usuarios e inicio de sesión (contraseñas con hash BCrypt, nunca en texto plano).</summary>
public class AuthService
{
    private readonly ConectaRhContext _db;
    public AuthService(ConectaRhContext db) => _db = db;

    public static string GenerarHash(string contrasena) => BCrypt.Net.BCrypt.HashPassword(contrasena);

    /// <param name="permitirAdmin">Solo true cuando lo invoca un administrador autenticado (crear otra cuenta de administrador).</param>
    public async Task<ResultadoOperacion> RegistrarAsync(RegistroModel m, bool permitirAdmin = false)
    {
        var tipo = m.TipoUsuario?.Trim().ToLowerInvariant();
        var valido = tipo == NombresRol.Candidato || tipo == NombresRol.Empresa || (permitirAdmin && tipo == NombresRol.Administrador);
        if (!valido)
            return ResultadoOperacion.Falla("Tipo de usuario no válido.");

        var correo = m.Correo.Trim().ToLowerInvariant();
        if (await _db.Usuarios.AnyAsync(u => u.Correo == correo))
            return ResultadoOperacion.Falla("Ya existe una cuenta con ese correo.");

        var rol = await _db.Roles.FirstOrDefaultAsync(r => r.NombreRol == tipo);
        if (rol == null) return ResultadoOperacion.Falla("El rol no está configurado.");

        var nombre = m.NombreCompleto.Trim();
        var usuario = new Usuario
        {
            IdRol = rol.IdRol,
            NombreUsuario = nombre,
            Correo = correo,
            ContrasenaHash = GenerarHash(m.Contrasena),
            FechaRegistro = DateTime.Now,
            Estado = EstadosUsuario.Activo
        };

        // Un único SaveChanges: usuario y perfil se guardan juntos (transacción implícita)
        if (tipo == NombresRol.Candidato)
            usuario.Candidato = new Candidato { NombreCompleto = nombre };
        else if (tipo == NombresRol.Empresa)
            usuario.Empresa = new Empresa { NombreEmpresa = nombre };
        // administrador: solo usuario con rol "administrador" (no tiene tabla propia)

        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync();
        return ResultadoOperacion.Ok(usuario.IdUsuario);
    }

    /// <summary>Devuelve el usuario si las credenciales son válidas y la cuenta está activa.</summary>
    public async Task<(Usuario? Usuario, string? Error)> ValidarAsync(string correo, string contrasena)
    {
        correo = correo.Trim().ToLowerInvariant();
        var u = await _db.Usuarios.Include(x => x.Rol).FirstOrDefaultAsync(x => x.Correo == correo);
        if (u == null || !BCrypt.Net.BCrypt.Verify(contrasena, u.ContrasenaHash))
            return (null, "Correo o contraseña incorrectos.");
        if (u.Estado != EstadosUsuario.Activo)
            return (null, "Tu cuenta está inactiva. Contacta al administrador.");
        return (u, null);
    }
}
