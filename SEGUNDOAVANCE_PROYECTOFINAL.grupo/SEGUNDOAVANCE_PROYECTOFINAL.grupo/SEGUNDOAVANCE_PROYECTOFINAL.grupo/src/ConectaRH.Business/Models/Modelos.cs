using System.ComponentModel.DataAnnotations;

namespace ConectaRH.Business.Models;

public class RegistroModel
{
    [Required(ErrorMessage = "Selecciona el tipo de usuario.")]
    public string TipoUsuario { get; set; } = "candidato";

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    public string NombreCompleto { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Correo no válido.")]
    [StringLength(150)]
    public string Correo { get; set; } = "";

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(8, ErrorMessage = "Mínimo 8 caracteres.")]
    public string Contrasena { get; set; } = "";
}

public class LoginModel
{
    [Required(ErrorMessage = "Ingresa tu correo.")]
    [EmailAddress(ErrorMessage = "Correo no válido.")]
    public string Correo { get; set; } = "";

    [Required(ErrorMessage = "Ingresa tu contraseña.")]
    public string Contrasena { get; set; } = "";

    public bool Recordarme { get; set; } = true;
}

public class DatosCandidatoModel
{
    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [StringLength(150)]
    public string NombreCompleto { get; set; } = "";

    public DateTime? FechaNacimiento { get; set; }

    [StringLength(20)]
    public string? Telefono { get; set; }

    [StringLength(200)]
    public string? Direccion { get; set; }

    public string? ResumenProfesional { get; set; }
}

public class FormacionModel
{
    [Required(ErrorMessage = "Indica el nivel académico o título.")]
    [StringLength(100)]
    public string NivelAcademico { get; set; } = "";

    [Required(ErrorMessage = "Indica la institución.")]
    [StringLength(150)]
    public string Institucion { get; set; } = "";

    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
}

public class ExperienciaModel
{
    [Required(ErrorMessage = "Indica el puesto.")]
    [StringLength(120)]
    public string Puesto { get; set; } = "";

    [Required(ErrorMessage = "Indica la empresa.")]
    [StringLength(150)]
    public string EmpresaAnterior { get; set; } = "";

    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public string? Descripcion { get; set; }
}

public class DatosEmpresaModel
{
    [Required(ErrorMessage = "El nombre de la empresa es obligatorio.")]
    [StringLength(150)]
    public string NombreEmpresa { get; set; } = "";

    [StringLength(100)]
    public string? Sector { get; set; }

    [StringLength(200)]
    public string? Direccion { get; set; }

    [StringLength(20)]
    public string? Telefono { get; set; }

    public string? Descripcion { get; set; }
}

public class OfertaModel
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(150)]
    public string Titulo { get; set; } = "";

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    public string Descripcion { get; set; } = "";

    [Required(ErrorMessage = "Los requisitos son obligatorios.")]
    public string Requisitos { get; set; } = "";

    [StringLength(150)]
    public string? Ubicacion { get; set; }

    public DateTime? FechaCierre { get; set; }
}

public class ResultadoOperacion
{
    public bool Exito { get; set; }
    public string? Error { get; set; }
    public int? Id { get; set; }

    public static ResultadoOperacion Ok(int? id = null) => new() { Exito = true, Id = id };
    public static ResultadoOperacion Falla(string error) => new() { Exito = false, Error = error };
}

public class ResumenReportes
{
    public int TotalUsuarios { get; set; }
    public int Candidatos { get; set; }
    public int Empresas { get; set; }
    public int Administradores { get; set; }
    public int OfertasPublicadas { get; set; }
    public int OfertasPausadas { get; set; }
    public int OfertasCerradas { get; set; }
    public int Postulaciones { get; set; }
    public int PostulacionesPendientes { get; set; }
    public int PostulacionesAceptadas { get; set; }
}
