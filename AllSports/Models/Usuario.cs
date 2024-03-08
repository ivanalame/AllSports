using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AllSports.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Apellidos")]
        public string Apellidos { get; set; }
        [Column("Nif")]
        public int Nif { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("SALT")]

        public string Salt { get; set; }
        //UNA VENTAJA CON EF ES QUE LOS TIPOS DE  

        //DATOS VARBINARY, BLOB, CLOB SON CONVERTIDOS 

        //AUTOMATICAMENTE A byte[] 

        [Column("Contraseña")]

        public byte[] Password { get; set; }
    }
}
