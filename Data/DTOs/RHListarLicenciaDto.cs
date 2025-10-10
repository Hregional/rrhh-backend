namespace rrhh_backend.Data.DTOs
{
    public class RHListarLicenciaDto
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public int IdColaborador { get; set; }
        public int IdTipoLicencia { get; set; }
        public int IdEstadoLicencia { get; set; }
        public string NombreColaborador { get; set; }
        public string Dpi { get; set; }
        public string TipoLicencia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string EstadoLicencia { get; set; }
        public string Observaciones { get; set; }
        public string UrlConstancia { get; set; }
    }
}
