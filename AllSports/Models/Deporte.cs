using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllSports.Models
{
    [Table("Deportes")]
    public class Deporte
    {
        [Key]
        [Column("IdDeporte")]
        public int IdDeporte { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
    }
}
