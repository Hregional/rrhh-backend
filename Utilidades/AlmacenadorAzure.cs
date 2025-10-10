using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace rrhh_backend.Utilidades
{
    public class AlmacenadorAzure : IAlmacenadorAzure
    {
        private string conectionString;

        public AlmacenadorAzure(IConfiguration configuration, ILogger<AlmacenadorAzure> logger)
        {
            conectionString = configuration.GetConnectionString("AzureStorage");
            logger.LogInformation("Connection String: {ConnectionString}", conectionString);
        }

        public async Task<String> GuardarImagen(string contenedor, IFormFile imagen, Guid nombre)
        {
            var cliente = new BlobContainerClient(conectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            // La siguiente línea se elimina porque causa el error PublicAccessNotPermitted
            // cliente.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            var extension = Path.GetExtension(imagen.FileName);
            var nombreUnico = $"{nombre}{extension}";
            var blob = cliente.GetBlobClient(nombreUnico);
            await blob.UploadAsync(imagen.OpenReadStream());

            // Generar una URL SAS para el blob
            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = contenedor,
                BlobName = nombreUnico,
                Resource = "b", // "b" para blob
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddDays(1), // Válido por 1 día
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read); // Permiso de solo lectura

            var sasToken = sasBuilder.ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(cliente.AccountName, GetAccountKey()));
            return $"{blob.Uri}?{sasToken}";
        }

        public async Task EliminarImagen(string ruta, string contenedor)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                return;
            }
            var cliente = new BlobContainerClient(conectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            var archivo = Path.GetFileName(new Uri(ruta).AbsolutePath);
            var blob = cliente.GetBlobClient(archivo);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditarImagen(string contenedor, IFormFile imagen, string ruta, Guid nombre)
        {
            await EliminarImagen(ruta, contenedor);
            return await GuardarImagen(contenedor, imagen, nombre);
        }

        private string GetAccountKey()
        {
            // Extraer la clave de la cuenta de la cadena de conexión
            var parts = conectionString.Split(';').ToDictionary(s => s.Split(new[] { '=' }, 2)[0], s => s.Split(new[] { '=' }, 2)[1]);
            return parts["AccountKey"];
        }
    }
}
