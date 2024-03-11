using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace reto_citikold_back.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCliente { get; set; }
        [MaxLength(15)]
        [Required]
        public string rucDni { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public Boolean activo { get; set; }
        public DateTime? fechaCreacion { get; set; } = DateTime.Now;
        [JsonIgnore]
        public List<Factura> facturas { get; set; } = new List<Factura>();

    }
}
