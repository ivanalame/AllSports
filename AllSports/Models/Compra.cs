using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AllSports.Models
{
    [Table("COMPRAS")]
    public class Compra
    {
        [Key]
        [Column("IdCompra")]
        public int IdCompra { get; set; }
        [Column("Albaran")]
        public int Albaran { get; set; }
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        [Column("IdProducto")]
        public int IdProducto { get; set; }
        [Column("Cantidad")]
        public int Cantidad { get; set; }
        [Column("FechaCompra")]
        public DateTime FechaCompra { get; set; }
        [Column("Descuento")]
        public int Descuento { get; set; }
    }
}
