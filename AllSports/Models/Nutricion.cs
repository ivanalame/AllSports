﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllSports.Models
{
    [Table("Nutricion")]
    public class Nutricion
    {
        [Key]
        [Column("IdTipoNutricion")]
        public int IdTipoNutricion { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
    }
}
