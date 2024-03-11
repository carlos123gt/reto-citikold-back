using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using reto_citikold_back.Models.dto;

namespace reto_citikold_back.Models
{
    public class Factura
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFactura { get; set; }

        public int IdClient { get; set; }
        public double SubTotal { get; set; }
        public double Igv { get; set; }
        public double Total { get; set; }
        [JsonIgnore]
        public Client Cliente { get; set; }
        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();
    }
}
