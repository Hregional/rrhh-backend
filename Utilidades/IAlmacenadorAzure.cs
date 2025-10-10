
namespace rrhh_backend.Utilidades
{
    public interface IAlmacenadorAzure
    {
        Task<string> EditarImagen(string contenedor, IFormFile imagen, string ruta, Guid nombre);
        Task EliminarImagen(string ruta, string contenedor);
        Task<string> GuardarImagen(string contenedor, IFormFile imagen, Guid nombre);
    }
}