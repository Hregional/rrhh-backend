using System;
using System.ComponentModel.DataAnnotations;

namespace rrhh_backend.Data.Models
{
    public class RHAsueto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; }
    }
}
