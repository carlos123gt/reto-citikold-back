using System.ComponentModel.DataAnnotations;

namespace reto_citikold_back.Models.dto
{
    public class ClientDTO
    {
        public int idCliente { get; set; }

        [MaxLength(15)]
        [Required]
        public string rucDni { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public Boolean activo { get; set; }
        public DateTime? fechaCreacion { get; set; } = DateTime.Now;
    }
}
