using System.ComponentModel.DataAnnotations;
namespace webapp.Models
{
    public class Materia
    {
        [Key]
        public int idMateria { get; set; }

        public int codigo { get; set; }
        public string nombre { get; set; }
        public string uv { get; set; }
        
    }
}
