using ConectaRH.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConectaRH.Data;

/// <summary>Crea los 3 roles y la cuenta de administrador inicial (un usuario con rol "administrador").</summary>
public static class DbSeeder
{
    public static async Task SembrarAsync(ConectaRhContext db, string adminNombre, string adminCorreo, string adminHash)
    {
        var roles = new[]
        {
            (NombresRol.Candidato, "Crear y editar su perfil, formación académica y experiencia laboral; consultar ofertas, postularse y consultar el estado de sus postulaciones."),
            (NombresRol.Empresa, "Registrar su empresa, publicar y administrar ofertas de empleo, y revisar los perfiles de las personas postuladas."),
            (NombresRol.Administrador, "Acceso completo: gestionar usuarios, empresas, ofertas de empleo y roles de la plataforma.")
        };

        foreach (var (nombre, permisos) in roles)
        {
            if (!await db.Roles.AnyAsync(r => r.NombreRol == nombre))
                db.Roles.Add(new Rol { NombreRol = nombre, DescripcionPermisos = permisos });
        }
        await db.SaveChangesAsync();

        if (!await db.Usuarios.AnyAsync(u => u.Rol.NombreRol == NombresRol.Administrador))
        {
            var rolAdmin = await db.Roles.FirstAsync(r => r.NombreRol == NombresRol.Administrador);
            db.Usuarios.Add(new Usuario
            {
                IdRol = rolAdmin.IdRol,
                NombreUsuario = adminNombre,
                Correo = adminCorreo.ToLowerInvariant(),
                ContrasenaHash = adminHash,
                FechaRegistro = DateTime.Now,
                Estado = EstadosUsuario.Activo
            });
            await db.SaveChangesAsync();
        }
    }
}
