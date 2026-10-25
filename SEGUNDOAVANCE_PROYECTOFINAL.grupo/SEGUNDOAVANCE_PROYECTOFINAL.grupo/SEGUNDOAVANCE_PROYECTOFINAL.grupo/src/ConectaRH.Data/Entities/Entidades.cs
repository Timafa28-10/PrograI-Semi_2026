namespace ConectaRH.Data.Entities;

/// <summary>Tabla rol: candidato, empresa o administrador.</summary>
public class Rol
{
    public int IdRol { get; set; }
    public string NombreRol { get; set; } = "";
    public string DescripcionPermisos { get; set; } = "";
    public List<Usuario> Usuarios { get; set; } = new();
}

/// <summary>Tabla usuario. El administrador es un usuario con el rol "administrador".</summary>
public class Usuario
{
    public int IdUsuario { get; set; }
    public int IdRol { get; set; }
    public string NombreUsuario { get; set; } = "";
    public string Correo { get; set; } = "";
    public string ContrasenaHash { get; set; } = "";
    public DateTime FechaRegistro { get; set; }
    public string Estado { get; set; } = EstadosUsuario.Activo;

    public Rol Rol { get; set; } = null!;
    public Candidato? Candidato { get; set; }
    public Empresa? Empresa { get; set; }
}

public class Candidato
{
    public int IdCandidato { get; set; }
    public int IdUsuario { get; set; }
    public string NombreCompleto { get; set; } = "";
    public DateTime? FechaNacimiento { get; set; }
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public string? ResumenProfesional { get; set; }

    public Usuario Usuario { get; set; } = null!;
    public List<FormacionAcademica> Formaciones { get; set; } = new();
    public List<ExperienciaLaboral> Experiencias { get; set; } = new();
    public List<Postulacion> Postulaciones { get; set; } = new();
}

public class Empresa
{
    public int IdEmpresa { get; set; }
    public int IdUsuario { get; set; }
    public string NombreEmpresa { get; set; } = "";
    public string? Sector { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Descripcion { get; set; }

    public Usuario Usuario { get; set; } = null!;
    public List<OfertaEmpleo> Ofertas { get; set; } = new();
}

public class FormacionAcademica
{
    public int IdFormacion { get; set; }
    public int IdCandidato { get; set; }
    public string NivelAcademico { get; set; } = "";
    public string Institucion { get; set; } = "";
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }

    public Candidato Candidato { get; set; } = null!;
}

public class ExperienciaLaboral
{
    public int IdExperiencia { get; set; }
    public int IdCandidato { get; set; }
    public string Puesto { get; set; } = "";
    public string EmpresaAnterior { get; set; } = "";
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public string? Descripcion { get; set; }

    public Candidato Candidato { get; set; } = null!;
}

public class OfertaEmpleo
{
    public int IdOferta { get; set; }
    public int IdEmpresa { get; set; }
    public string Titulo { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public string Requisitos { get; set; } = "";
    public string? Ubicacion { get; set; }
    public DateTime FechaPublicacion { get; set; }
    public DateTime? FechaCierre { get; set; }
    public string Estado { get; set; } = EstadosOferta.Publicada;

    public Empresa Empresa { get; set; } = null!;
    public List<Postulacion> Postulaciones { get; set; } = new();
}

public class Postulacion
{
    public int IdPostulacion { get; set; }
    public int IdCandidato { get; set; }
    public int IdOferta { get; set; }
    public DateTime FechaPostulacion { get; set; }
    public string Estado { get; set; } = EstadosPostulacion.Pendiente;
    public string? Observacion { get; set; }

    public Candidato Candidato { get; set; } = null!;
    public OfertaEmpleo Oferta { get; set; } = null!;
}

public static class NombresRol
{
    public const string Candidato = "candidato";
    public const string Empresa = "empresa";
    public const string Administrador = "administrador";
}

public static class EstadosUsuario
{
    public const string Activo = "activo";
    public const string Inactivo = "inactivo";
}

public static class EstadosOferta
{
    public const string Publicada = "publicada";
    public const string Pausada = "pausada";
    public const string Cerrada = "cerrada";
    public static readonly string[] Todos = { Publicada, Pausada, Cerrada };
}

public static class EstadosPostulacion
{
    public const string Pendiente = "pendiente";
    public const string EnRevision = "en revisión";
    public const string Aceptada = "aceptada";
    public const string Rechazada = "rechazada";
    public static readonly string[] Todos = { Pendiente, EnRevision, Aceptada, Rechazada };
}
