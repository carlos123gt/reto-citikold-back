using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace reto_citikold_back.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int idProducto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public int stock { get; set; }
        public Boolean activo { get; set; }
        public DateTime? fechaCreacion { get; set; } = DateTime.Now;
    }
}
