using System.Threading.Tasks;

namespace rrhh_backend.Services.Administracion
{
    public interface IAuditoriaService
    {
        Task RegistrarCambioEstatus(int trabajadorId, int idEstatusAnterior, int idEstatusNuevo, string motivo, string sistemaEjecutor);
    }
}
