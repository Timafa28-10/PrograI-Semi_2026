using ConectaRH.Business.Models;
using ConectaRH.Data;
using ConectaRH.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConectaRH.Business.Services;

/// <summary>Perfil del candidato: datos personales, formación académica y experiencia laboral.</summary>
public class CandidatoService
{
    private readonly ConectaRhContext _db;
    public CandidatoService(ConectaRhContext db) => _db = db;

    public Task<Candidato?> ObtenerPorUsuarioAsync(int idUsuario) =>
        _db.Candidatos
            .Include(c => c.Usuario)
            .Include(c => c.Formaciones)
            .Include(c => c.Experiencias)
            .AsSplitQuery()
            .FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);

    public Task<Candidato?> ObtenerAsync(int idCandidato) =>
        _db.Candidatos
            .Include(c => c.Usuario)
            .Include(c => c.Formaciones)
            .Include(c => c.Experiencias)
            .AsSplitQuery()
            .FirstOrDefaultAsync(c => c.IdCandidato == idCandidato);

    public async Task<ResultadoOperacion> ActualizarDatosAsync(int idUsuario, DatosCandidatoModel m)
    {
        var c = await _db.Candidatos.Include(x => x.Usuario).FirstOrDefaultAsync(x => x.IdUsuario == idUsuario);
        if (c == null) return ResultadoOperacion.Falla("Perfil no encontrado.");
        c.NombreCompleto = m.NombreCompleto.Trim();
        c.Usuario.NombreUsuario = c.NombreCompleto;
        c.FechaNacimiento = m.FechaNacimiento;
        c.Telefono = Limpiar(m.Telefono);
        c.Direccion = Limpiar(m.Direccion);
        c.ResumenProfesional = Limpiar(m.ResumenProfesional);
        await _db.SaveChangesAsync();
        return ResultadoOperacion.Ok();
    }

    public async Task<ResultadoOperacion> AgregarFormacionAsync(int idCandidato, FormacionModel m)
    {
        if (m.FechaInicio.HasValue && m.FechaFin.HasValue && m.FechaFin < m.FechaInicio)
            return ResultadoOperacion.Falla("La fecha de fin no puede ser anterior a la de inicio.");
        _db.Formaciones.Add(new FormacionAcademica
        {
            IdCandidato = idCandidato,
            NivelAcademico = m.NivelAcademico.Trim(),
            Institucion = m.Institucion.Trim(),
            FechaInicio = m.FechaInicio,
            FechaFin = m.FechaFin
        });
        await _db.SaveChangesAsync();
        return ResultadoOperacion.Ok();
    }

    public async Task EliminarFormacionAsync(int idCandidato, int idFormacion)
    {
        var f = await _db.Formaciones.FirstOrDefaultAsync(x => x.IdFormacion == idFormacion && x.IdCandidato == idCandidato);
        if (f == null) return;
        _db.Formaciones.Remove(f);
        await _db.SaveChangesAsync();
    }

    public async Task<ResultadoOperacion> AgregarExperienciaAsync(int idCandidato, ExperienciaModel m)
    {
        if (m.FechaInicio.HasValue && m.FechaFin.HasValue && m.FechaFin < m.FechaInicio)
            return ResultadoOperacion.Falla("La fecha de fin no puede ser anterior a la de inicio.");
        _db.Experiencias.Add(new ExperienciaLaboral
        {
            IdCandidato = idCandidato,
            Puesto = m.Puesto.Trim(),
            EmpresaAnterior = m.EmpresaAnterior.Trim(),
            FechaInicio = m.FechaInicio,
            FechaFin = m.FechaFin,
            Descripcion = Limpiar(m.Descripcion)
        });
        await _db.SaveChangesAsync();
        return ResultadoOperacion.Ok();
    }

    public async Task EliminarExperienciaAsync(int idCandidato, int idExperiencia)
    {
        var x = await _db.Experiencias.FirstOrDefaultAsync(e => e.IdExperiencia == idExperiencia && e.IdCandidato == idCandidato);
        if (x == null) return;
        _db.Experiencias.Remove(x);
        await _db.SaveChangesAsync();
    }

    private static string? Limpiar(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim();
}
