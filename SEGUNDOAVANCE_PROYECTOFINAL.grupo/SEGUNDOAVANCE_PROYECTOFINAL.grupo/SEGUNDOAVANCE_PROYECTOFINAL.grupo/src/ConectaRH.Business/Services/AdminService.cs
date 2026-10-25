using ConectaRH.Business.Models;
using ConectaRH.Data;
using ConectaRH.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConectaRH.Business.Services;

/// <summary>Funciones del administrador: gestionar usuarios, roles y reportes.</summary>
public class AdminService
{
    private readonly ConectaRhContext _db;
    public AdminService(ConectaRhContext db) => _db = db;

    public async Task<List<Usuario>> ListarUsuariosAsync(string? buscar, string? rol = null)
    {
        var q = _db.Usuarios.Include(u => u.Rol).AsQueryable();
        if (!string.IsNullOrWhiteSpace(rol))
            q = q.Where(u => u.Rol.NombreRol == rol);
        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            q = q.Where(u => u.NombreUsuario.Contains(t) || u.Correo.Contains(t));
        }
        return await q.OrderBy(u => u.NombreUsuario).ToListAsync();
    }

    public Task<Usuario?> ObtenerUsuarioAsync(int idUsuario) =>
        _db.Usuarios.Include(u => u.Rol).Include(u => u.Candidato).Include(u => u.Empresa)
            .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

    public async Task<ResultadoOperacion> CambiarEstadoUsuarioAsync(int idUsuario, int idAdminActual)
    {
        if (idUsuario == idAdminActual) return ResultadoOperacion.Falla("No puedes desactivar tu propia cuenta.");
        var u = await _db.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == idUsuario);
        if (u == null) return ResultadoOperacion.Falla("Usuario no encontrado.");
        u.Estado = u.Estado == EstadosUsuario.Activo ? EstadosUsuario.Inactivo : EstadosUsuario.Activo;
        await _db.SaveChangesAsync();
        return ResultadoOperacion.Ok();
    }

    /// <summary>Roles con la cantidad de usuarios que los tienen asignados.</summary>
    public async Task<List<(Rol Rol, int Usuarios)>> ListarRolesAsync(string? buscar)
    {
        var q = _db.Roles.AsQueryable();
        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            q = q.Where(r => r.NombreRol.Contains(t) || r.DescripcionPermisos.Contains(t));
        }
        var roles = await q.OrderBy(r => r.IdRol).ToListAsync();
        var conteo = await _db.Usuarios.GroupBy(u => u.IdRol).Select(g => new { g.Key, N = g.Count() }).ToDictionaryAsync(x => x.Key, x => x.N);
        return roles.Select(r => (r, conteo.TryGetValue(r.IdRol, out var n) ? n : 0)).ToList();
    }

    public async Task<ResumenReportes> ObtenerResumenAsync()
    {
        var roles = await _db.Usuarios.GroupBy(u => u.Rol.NombreRol).Select(g => new { g.Key, N = g.Count() }).ToDictionaryAsync(x => x.Key, x => x.N);
        int R(string k) => roles.TryGetValue(k, out var n) ? n : 0;
        return new ResumenReportes
        {
            TotalUsuarios = roles.Values.Sum(),
            Candidatos = R(NombresRol.Candidato),
            Empresas = R(NombresRol.Empresa),
            Administradores = R(NombresRol.Administrador),
            OfertasPublicadas = await _db.Ofertas.CountAsync(o => o.Estado == EstadosOferta.Publicada),
            OfertasPausadas = await _db.Ofertas.CountAsync(o => o.Estado == EstadosOferta.Pausada),
            OfertasCerradas = await _db.Ofertas.CountAsync(o => o.Estado == EstadosOferta.Cerrada),
            Postulaciones = await _db.Postulaciones.CountAsync(),
            PostulacionesPendientes = await _db.Postulaciones.CountAsync(p => p.Estado == EstadosPostulacion.Pendiente),
            PostulacionesAceptadas = await _db.Postulaciones.CountAsync(p => p.Estado == EstadosPostulacion.Aceptada)
        };
    }
}
