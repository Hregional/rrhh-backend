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

        public async Task<string> GuardarImagen(string contenedor, IFormFile imagen, Guid nombre)
        {
            var cliente = new BlobContainerClient(conectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            var extension = Path.GetExtension(imagen.FileName);
            var nombreUnico = $"{nombre}{extension}";
            var blob = cliente.GetBlobClient(nombreUnico);
            await blob.UploadAsync(imagen.OpenReadStream(), true);
            return nombreUnico;
        }

        public async Task<string> ObtenerUrlConSas(string contenedor, string nombreArchivo)
        {
            var cliente = new BlobContainerClient(conectionString, contenedor);
            var blob = cliente.GetBlobClient(nombreArchivo);

            if (!await blob.ExistsAsync())
            {
                return null;
            }

            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = contenedor,
                BlobName = nombreArchivo,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
                ExpiresOn = DateTimeOffset.UtcNow.AddDays(1),
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read);
            var sasToken = sasBuilder.ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(cliente.AccountName, GetAccountKey()));
            return $"{blob.Uri}?{sasToken}";
        }

        public async Task EliminarImagen(string nombreArchivo, string contenedor)
        {
            if (string.IsNullOrEmpty(nombreArchivo))
            {
                return;
            }
            var cliente = new BlobContainerClient(conectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            var blob = cliente.GetBlobClient(nombreArchivo);
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
