using ConectaRH.Business.Models;
using ConectaRH.Data;
using ConectaRH.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConectaRH.Business.Services;

public class EmpresaService
{
    private readonly ConectaRhContext _db;
    public EmpresaService(ConectaRhContext db) => _db = db;

    public Task<Empresa?> ObtenerPorUsuarioAsync(int idUsuario) =>
        _db.Empresas.Include(e => e.Usuario).FirstOrDefaultAsync(e => e.IdUsuario == idUsuario);

    public Task<Empresa?> ObtenerAsync(int idEmpresa) =>
        _db.Empresas.Include(e => e.Usuario).FirstOrDefaultAsync(e => e.IdEmpresa == idEmpresa);

    public async Task<ResultadoOperacion> ActualizarAsync(int idEmpresa, DatosEmpresaModel m)
    {
        var e = await _db.Empresas.Include(x => x.Usuario).FirstOrDefaultAsync(x => x.IdEmpresa == idEmpresa);
        if (e == null) return ResultadoOperacion.Falla("Empresa no encontrada.");
        e.NombreEmpresa = m.NombreEmpresa.Trim();
        e.Usuario.NombreUsuario = e.NombreEmpresa;
        e.Sector = Limpiar(m.Sector);
        e.Direccion = Limpiar(m.Direccion);
        e.Telefono = Limpiar(m.Telefono);
        e.Descripcion = Limpiar(m.Descripcion);
        await _db.SaveChangesAsync();
        return ResultadoOperacion.Ok();
    }

    /// <summary>Listado para el administrador (Gestión de clientes).</summary>
    public async Task<List<Empresa>> ListarAsync(string? buscar)
    {
        var q = _db.Empresas.Include(e => e.Usuario).AsQueryable();
        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            q = q.Where(e => e.NombreEmpresa.Contains(t) || e.Usuario.Correo.Contains(t));
        }
        return await q.OrderBy(e => e.NombreEmpresa).ToListAsync();
    }

    private static string? Limpiar(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
}
