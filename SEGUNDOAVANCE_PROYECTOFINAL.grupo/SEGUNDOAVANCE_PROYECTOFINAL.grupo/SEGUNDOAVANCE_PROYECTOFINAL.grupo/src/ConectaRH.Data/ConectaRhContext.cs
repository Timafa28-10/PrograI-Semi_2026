using ConectaRH.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConectaRH.Data;

/// <summary>
/// Contexto de MySQL. Reproduce exactamente las 8 tablas del documento
/// "Tablas ConectaRH (formato corregido)": tipos, claves, UNIQUE e índices.
/// </summary>
public class ConectaRhContext : DbContext
{
    public ConectaRhContext(DbContextOptions<ConectaRhContext> options) : base(options) { }

    public DbSet<Rol> Roles => Set<Rol>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Candidato> Candidatos => Set<Candidato>();
    public DbSet<Empresa> Empresas => Set<Empresa>();
    public DbSet<FormacionAcademica> Formaciones => Set<FormacionAcademica>();
    public DbSet<ExperienciaLaboral> Experiencias => Set<ExperienciaLaboral>();
    public DbSet<OfertaEmpleo> Ofertas => Set<OfertaEmpleo>();
    public DbSet<Postulacion> Postulaciones => Set<Postulacion>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        // ---------- rol ----------
        mb.Entity<Rol>(e =>
        {
            e.ToTable("rol");
            e.HasKey(x => x.IdRol);
            e.Property(x => x.IdRol).HasColumnName("id_rol");
            e.Property(x => x.NombreRol).HasColumnName("nombre_rol").HasColumnType("varchar(30)").IsRequired();
            e.Property(x => x.DescripcionPermisos).HasColumnName("descripcion_permisos").HasColumnType("varchar(500)").IsRequired();
            e.HasIndex(x => x.NombreRol).IsUnique();
        });

        // ---------- usuario ----------
        mb.Entity<Usuario>(e =>
        {
            e.ToTable("usuario");
            e.HasKey(x => x.IdUsuario);
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario");
            e.Property(x => x.IdRol).HasColumnName("id_rol").IsRequired();
            e.Property(x => x.NombreUsuario).HasColumnName("nombre_usuario").HasColumnType("varchar(100)").IsRequired();
            e.Property(x => x.Correo).HasColumnName("correo").HasColumnType("varchar(150)").IsRequired();
            e.Property(x => x.ContrasenaHash).HasColumnName("contrasena_hash").HasColumnType("varchar(255)").IsRequired();
            e.Property(x => x.FechaRegistro).HasColumnName("fecha_registro").HasColumnType("datetime").IsRequired();
            e.Property(x => x.Estado).HasColumnName("estado").HasColumnType("varchar(20)").IsRequired();
            e.HasIndex(x => x.Correo).IsUnique();
            e.HasIndex(x => x.IdRol);
            e.HasOne(x => x.Rol).WithMany(r => r.Usuarios).HasForeignKey(x => x.IdRol).OnDelete(DeleteBehavior.Restrict);
        });

        // ---------- candidato ----------
        mb.Entity<Candidato>(e =>
        {
            e.ToTable("candidato");
            e.HasKey(x => x.IdCandidato);
            e.Property(x => x.IdCandidato).HasColumnName("id_candidato");
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario").IsRequired();
            e.Property(x => x.NombreCompleto).HasColumnName("nombre_completo").HasColumnType("varchar(150)").IsRequired();
            e.Property(x => x.FechaNacimiento).HasColumnName("fecha_nacimiento").HasColumnType("date");
            e.Property(x => x.Telefono).HasColumnName("telefono").HasColumnType("varchar(20)");
            e.Property(x => x.Direccion).HasColumnName("direccion").HasColumnType("varchar(200)");
            e.Property(x => x.ResumenProfesional).HasColumnName("resumen_profesional").HasColumnType("text");
            e.HasIndex(x => x.IdUsuario).IsUnique();
            e.HasOne(x => x.Usuario).WithOne(u => u.Candidato).HasForeignKey<Candidato>(x => x.IdUsuario).OnDelete(DeleteBehavior.Cascade);
        });

        // ---------- empresa ----------
        mb.Entity<Empresa>(e =>
        {
            e.ToTable("empresa");
            e.HasKey(x => x.IdEmpresa);
            e.Property(x => x.IdEmpresa).HasColumnName("id_empresa");
            e.Property(x => x.IdUsuario).HasColumnName("id_usuario").IsRequired();
            e.Property(x => x.NombreEmpresa).HasColumnName("nombre_empresa").HasColumnType("varchar(150)").IsRequired();
            e.Property(x => x.Sector).HasColumnName("sector").HasColumnType("varchar(100)");
            e.Property(x => x.Direccion).HasColumnName("direccion").HasColumnType("varchar(200)");
            e.Property(x => x.Telefono).HasColumnName("telefono").HasColumnType("varchar(20)");
            e.Property(x => x.Descripcion).HasColumnName("descripcion").HasColumnType("text");
            e.HasIndex(x => x.IdUsuario).IsUnique();
            e.HasOne(x => x.Usuario).WithOne(u => u.Empresa).HasForeignKey<Empresa>(x => x.IdUsuario).OnDelete(DeleteBehavior.Cascade);
        });

        // ---------- formacion_academica ----------
        mb.Entity<FormacionAcademica>(e =>
        {
            e.ToTable("formacion_academica");
            e.HasKey(x => x.IdFormacion);
            e.Property(x => x.IdFormacion).HasColumnName("id_formacion");
            e.Property(x => x.IdCandidato).HasColumnName("id_candidato").IsRequired();
            e.Property(x => x.NivelAcademico).HasColumnName("nivel_academico").HasColumnType("varchar(100)").IsRequired();
            e.Property(x => x.Institucion).HasColumnName("institucion").HasColumnType("varchar(150)").IsRequired();
            e.Property(x => x.FechaInicio).HasColumnName("fecha_inicio").HasColumnType("date");
            e.Property(x => x.FechaFin).HasColumnName("fecha_fin").HasColumnType("date");
            e.HasIndex(x => x.IdCandidato);
            e.HasOne(x => x.Candidato).WithMany(c => c.Formaciones).HasForeignKey(x => x.IdCandidato).OnDelete(DeleteBehavior.Cascade);
        });

        // ---------- experiencia_laboral ----------
        mb.Entity<ExperienciaLaboral>(e =>
        {
            e.ToTable("experiencia_laboral");
            e.HasKey(x => x.IdExperiencia);
            e.Property(x => x.IdExperiencia).HasColumnName("id_experiencia");
            e.Property(x => x.IdCandidato).HasColumnName("id_candidato").IsRequired();
            e.Property(x => x.Puesto).HasColumnName("puesto").HasColumnType("varchar(120)").IsRequired();
            e.Property(x => x.EmpresaAnterior).HasColumnName("empresa_anterior").HasColumnType("varchar(150)").IsRequired();
            e.Property(x => x.FechaInicio).HasColumnName("fecha_inicio").HasColumnType("date");
            e.Property(x => x.FechaFin).HasColumnName("fecha_fin").HasColumnType("date");
            e.Property(x => x.Descripcion).HasColumnName("descripcion").HasColumnType("text");
            e.HasIndex(x => x.IdCandidato);
            e.HasOne(x => x.Candidato).WithMany(c => c.Experiencias).HasForeignKey(x => x.IdCandidato).OnDelete(DeleteBehavior.Cascade);
        });

        // ---------- oferta_empleo ----------
        mb.Entity<OfertaEmpleo>(e =>
        {
            e.ToTable("oferta_empleo");
            e.HasKey(x => x.IdOferta);
            e.Property(x => x.IdOferta).HasColumnName("id_oferta");
            e.Property(x => x.IdEmpresa).HasColumnName("id_empresa").IsRequired();
            e.Property(x => x.Titulo).HasColumnName("titulo").HasColumnType("varchar(150)").IsRequired();
            e.Property(x => x.Descripcion).HasColumnName("descripcion").HasColumnType("text").IsRequired();
            e.Property(x => x.Requisitos).HasColumnName("requisitos").HasColumnType("text").IsRequired();
            e.Property(x => x.Ubicacion).HasColumnName("ubicacion").HasColumnType("varchar(150)");
            e.Property(x => x.FechaPublicacion).HasColumnName("fecha_publicacion").HasColumnType("datetime").IsRequired();
            e.Property(x => x.FechaCierre).HasColumnName("fecha_cierre").HasColumnType("date");
            e.Property(x => x.Estado).HasColumnName("estado").HasColumnType("varchar(20)").IsRequired();
            e.HasIndex(x => x.IdEmpresa);
            e.HasIndex(x => x.Titulo);
            e.HasIndex(x => x.FechaPublicacion);
            e.HasIndex(x => x.Estado);
            e.HasOne(x => x.Empresa).WithMany(emp => emp.Ofertas).HasForeignKey(x => x.IdEmpresa).OnDelete(DeleteBehavior.Cascade);
        });

        // ---------- postulacion ----------
        mb.Entity<Postulacion>(e =>
        {
            e.ToTable("postulacion");
            e.HasKey(x => x.IdPostulacion);
            e.Property(x => x.IdPostulacion).HasColumnName("id_postulacion");
            e.Property(x => x.IdCandidato).HasColumnName("id_candidato").IsRequired();
            e.Property(x => x.IdOferta).HasColumnName("id_oferta").IsRequired();
            e.Property(x => x.FechaPostulacion).HasColumnName("fecha_postulacion").HasColumnType("datetime").IsRequired();
            e.Property(x => x.Estado).HasColumnName("estado").HasColumnType("varchar(20)").IsRequired();
            e.Property(x => x.Observacion).HasColumnName("observacion").HasColumnType("varchar(500)");
            e.HasIndex(x => x.IdCandidato);
            e.HasIndex(x => x.IdOferta);
            // UNIQUE compuesta: evita postularse dos veces a la misma oferta
            e.HasIndex(x => new { x.IdCandidato, x.IdOferta }).IsUnique();
            e.HasOne(x => x.Candidato).WithMany(c => c.Postulaciones).HasForeignKey(x => x.IdCandidato).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Oferta).WithMany(o => o.Postulaciones).HasForeignKey(x => x.IdOferta).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
