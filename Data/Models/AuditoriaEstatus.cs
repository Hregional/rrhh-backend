using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rrhh_backend.Data.Models
{
    public class AuditoriaEstatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TrabajadorId { get; set; }
        public RHColaborador Trabajador { get; set; }

        public DateTime FechaCambio { get; set; } = DateTime.Now;

        public int IdEstatusAnterior { get; set; }
        public RHEstadoColaborador EstatusAnteriorNavigation { get; set; }

        public int IdEstatusNuevo { get; set; }
        public RHEstadoColaborador EstatusNuevoNavigation { get; set; }

        [StringLength(500)]
        public string Motivo { get; set; }

        [Required]
        [StringLength(100)]
        public string SistemaEjecutor { get; set; }
    }
}
