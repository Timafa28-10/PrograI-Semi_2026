using ConectaRH.Business.Models;
using ConectaRH.Data;
using ConectaRH.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConectaRH.Business.Services;

public class OfertaService
{
    private readonly ConectaRhContext _db;
    public OfertaService(ConectaRhContext db) => _db = db;

    /// <summary>Ofertas visibles para los candidatos: solo las publicadas y vigentes.</summary>
    public async Task<List<OfertaEmpleo>> BuscarPublicadasAsync(string? buscar)
    {
        var hoy = DateTime.Today;
        var q = _db.Ofertas.Include(o => o.Empresa)
            .Where(o => o.Estado == EstadosOferta.Publicada && (o.FechaCierre == null || o.FechaCierre >= hoy));
        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            q = q.Where(o => o.Titulo.Contains(t) || o.Empresa.NombreEmpresa.Contains(t) || (o.Ubicacion != null && o.Ubicacion.Contains(t)));
        }
        return await q.OrderByDescending(o => o.FechaPublicacion).ToListAsync();
    }

    public Task<OfertaEmpleo?> ObtenerAsync(int idOferta) =>
        _db.Ofertas.Include(o => o.Empresa).FirstOrDefaultAsync(o => o.IdOferta == idOferta);

    public async Task<List<OfertaEmpleo>> ListarPorEmpresaAsync(int idEmpresa) =>
        await _db.Ofertas.Include(o => o.Postulaciones)
            .Where(o => o.IdEmpresa == idEmpresa)
            .OrderByDescending(o => o.FechaPublicacion).ToListAsync();

    public async Task<ResultadoOperacion> CrearAsync(int idEmpresa, OfertaModel m)
    {
        if (m.FechaCierre.HasValue && m.FechaCierre.Value.Date < DateTime.Today)
            return ResultadoOperacion.Falla("La fecha de cierre no puede ser anterior a hoy.");
        var o = new OfertaEmpleo
        {
            IdEmpresa = idEmpresa,
            Titulo = m.Titulo.Trim(),
            Descripcion = m.Descripcion.Trim(),
            Requisitos = m.Requisitos.Trim(),
            Ubicacion = string.IsNullOrWhiteSpace(m.Ubicacion) ? null : m.Ubicacion.Trim(),
            FechaPublicacion = DateTime.Now,
            FechaCierre = m.FechaCierre,
            Estado = EstadosOferta.Publicada
        };
        _db.Ofertas.Add(o);
        await _db.SaveChangesAsync();
        return ResultadoOperacion.Ok(o.IdOferta);
    }

    /// <summary>Cambia el estado. Si idEmpresa tiene valor, solo se permite sobre ofertas propias (la empresa); el administrador pasa null.</summary>
    public async Task<ResultadoOperacion> CambiarEstadoAsync(int idOferta, string estado, int? idEmpresa)
    {
        if (!EstadosOferta.Todos.Contains(estado)) return ResultadoOperacion.Falla("Estado no válido.");
        var o = await _db.Ofertas.FirstOrDefaultAsync(x => x.IdOferta == idOferta && (idEmpresa == null || x.IdEmpresa == idEmpresa));
        if (o == null) return ResultadoOperacion.Falla("Oferta no encontrada.");
        o.Estado = estado;
        await _db.SaveChangesAsync();
        return ResultadoOperacion.Ok();
    }

    /// <summary>Listado completo para el administrador (Gestionar Ofertas).</summary>
    public async Task<List<OfertaEmpleo>> ListarTodasAsync(string? buscar)
    {
        var q = _db.Ofertas.Include(o => o.Empresa).Include(o => o.Postulaciones).AsQueryable();
        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var t = buscar.Trim();
            q = q.Where(o => o.Titulo.Contains(t) || o.Empresa.NombreEmpresa.Contains(t));
        }
        return await q.OrderByDescending(o => o.FechaPublicacion).ToListAsync();
    }
}
