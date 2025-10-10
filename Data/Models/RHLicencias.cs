namespace rrhh_backend.Data.Models
{
    public class RHLicencias
    {
        public int IdLicencias { get; set; }
        public Guid Uuid { get; set; }
        public int IdColaborador { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? Observaciones { get; set; }
        public int IdTipoLicencia { get; set; }
        public int IdEstadoLicencia { get; set; }
        public string? UrlConstancia { get; set; }
        public virtual RHColaborador RHColaborador { get; set; } = null!;
        public virtual RHTipoLicencias RHTipoLicencias { get; set; } = null!;
        public virtual RHEstadoLicencias RHEstadoLicencias { get; set; } = null!;
    }
}
