using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace reto_citikold_back.Models
{
    public class UserCitikold
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int idUser { get; set; }

        public string username { get; set; }
        public string password { get; set; }
        public int intentosIncorrecto { get; set; }
        public Boolean activo { get; set; }

    }
}