using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace reto_citikold_back.Models.dto
{
    public class FacturaDTO
    {

        public int IdFactura { get; set; }

        public int IdClient { get; set; }
        public double subTotal { get; set; }
        public double Igv { get; set; }
        public double Total { get; set; }

        public Client Cliente { get; set; }
        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();
    }
}
