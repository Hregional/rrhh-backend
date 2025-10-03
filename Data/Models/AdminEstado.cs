using System.ComponentModel.DataAnnotations.Schema;

namespace rrhh_backend.Data.Models
{
    public class AdminEstado
    {
        public int IdEstado { get; set; }

        public string NombreEstado { get; set; } = null!;

        [Column(TypeName = "longtext")]
        public string? Descripcion { get; set; }

        public virtual ICollection<AdminUser> UserUsers { get; set; } = new List<AdminUser>();
    }
}
