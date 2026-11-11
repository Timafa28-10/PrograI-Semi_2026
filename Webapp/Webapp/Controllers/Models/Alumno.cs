using System.ComponentModel.DataAnnotations;
namespace Webapp.Controllers.Models
{
    public class Alumno
    {
        [Key]
        public int IdAlumno { get; set;} 
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
    }
}
