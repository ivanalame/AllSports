using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AllSports.Models
{
    [Table("DetallesDeportes")]
    public class DetalleDeporte
    {
        [Key]
        [Column("IdDetalleDeporte")]
        public int IdDetalleDeporte { get; set; }

        [Column("IdDeporte")]
        public int IdDeporte { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
    }
}
