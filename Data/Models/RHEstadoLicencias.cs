using System.ComponentModel.DataAnnotations.Schema;

namespace rrhh_backend.Data.Models
{
    public class RHEstadoLicencias
    {
        public int IdEstadoLicencia { get; set; }

        [Column(TypeName = "longtext")]
        public string EstadoLicencia { get; set; }

        [Column(TypeName = "longtext")]
        public string? Descripcion { get; set; }

        public virtual ICollection<RHLicencias> RHLicencias { get; set; } = new List<RHLicencias>();
    }
}
