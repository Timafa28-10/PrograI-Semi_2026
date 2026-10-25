using ConectaRH.Business.Models;
using ConectaRH.Data;
using ConectaRH.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConectaRH.Business.Services;

public class PostulacionService
{
    private readonly ConectaRhContext _db;
    public PostulacionService(ConectaRhContext db) => _db = db;

    public Task<bool> YaPostuladoAsync(int idCandidato, int idOferta) =>
        _db.Postulaciones.AnyAsync(p => p.IdCandidato == idCandidato && p.IdOferta == idOferta);

    /// <summary>Crea la postulación. El mensaje adicional del candidato se guarda en "observacion".</summary>
    public async Task<ResultadoOperacion> PostularAsync(int idCandidato, int idOferta, string? mensaje)
    {
        var oferta = await _db.Ofertas.FirstOrDefaultAsync(o => o.IdOferta == idOferta);
        if (oferta == null) return ResultadoOperacion.Falla("La oferta no existe.");
        if (oferta.Estado != EstadosOferta.Publicada || (oferta.FechaCierre.HasValue && oferta.FechaCierre.Value.Date < DateTime.Today))
            return ResultadoOperacion.Falla("Esta oferta ya no recibe postulaciones.");
        if (await YaPostuladoAsync(idCandidato, idOferta))
            return ResultadoOperacion.Falla("Ya te postulaste a esta oferta.");

        var texto = string.IsNullOrWhiteSpace(mensaje) ? null : mensaje.Trim();
        if (texto != null && texto.Length > 500) texto = texto[..500];

        var p = new Postulacion
        {
            IdCandidato = idCandidato,
            IdOferta = idOferta,
            FechaPostulacion = DateTime.Now,
            Estado = EstadosPostulacion.Pendiente,
            Observacion = texto
        };
        _db.Postulaciones.Add(p);
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            // La restricción UNIQUE (id_candidato, id_oferta) protege contra doble envío simultáneo
            return ResultadoOperacion.Falla("Ya te postulaste a esta oferta.");
        }
        return ResultadoOperacion.Ok(p.IdPostulacion);
    }

    public async Task<List<Postulacion>> ListarPorCandidatoAsync(int idCandidato) =>
        await _db.Postulaciones.Include(p => p.Oferta).ThenInclude(o => o.Empresa)
            .Where(p => p.IdCandidato == idCandidato)
            .OrderByDescending(p => p.FechaPostulacion).ToListAsync();

    public async Task<List<Postulacion>> ListarPorEmpresaAsync(int idEmpresa, int? idOferta)
    {
        var q = _db.Postulaciones
            .Include(p => p.Candidato).ThenInclude(c => c.Usuario)
            .Include(p => p.Candidato).ThenInclude(c => c.Formaciones)
            .Include(p => p.Candidato).ThenInclude(c => c.Experiencias)
            .Include(p => p.Oferta)
            .AsSplitQuery()
            .Where(p => p.Oferta.IdEmpresa == idEmpresa);
        if (idOferta.HasValue) q = q.Where(p => p.IdOferta == idOferta.Value);
        return await q.OrderByDescending(p => p.FechaPostulacion).ToListAsync();
    }

    /// <summary>La empresa solo puede revisar postulaciones de sus propias ofertas.</summary>
    public Task<Postulacion?> ObtenerDeEmpresaAsync(int idPostulacion, int idEmpresa) =>
        _db.Postulaciones.Include(p => p.Oferta)
            .FirstOrDefaultAsync(p => p.IdPostulacion == idPostulacion && p.Oferta.IdEmpresa == idEmpresa);

    public Task<Postulacion?> ObtenerDeCandidatoAsync(int idPostulacion, int idCandidato) =>
        _db.Postulaciones.FirstOrDefaultAsync(p => p.IdPostulacion == idPostulacion && p.IdCandidato == idCandidato);

    public async Task<ResultadoOperacion> CambiarEstadoAsync(int idPostulacion, int idEmpresa, string estado, string? observacion)
    {
        if (!EstadosPostulacion.Todos.Contains(estado)) return ResultadoOperacion.Falla("Estado no válido.");
        var p = await ObtenerDeEmpresaAsync(idPostulacion, idEmpresa);
        if (p == null) return ResultadoOperacion.Falla("Postulación no encontrada.");
        p.Estado = estado;
        if (!string.IsNullOrWhiteSpace(observacion))
            p.Observacion = observacion.Trim().Length > 500 ? observacion.Trim()[..500] : observacion.Trim();
        await _db.SaveChangesAsync();
        return ResultadoOperacion.Ok();
    }
}
