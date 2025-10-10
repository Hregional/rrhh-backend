using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;
using rrhh_backend.Utilidades;

namespace rrhh_backend.Services.Rrhh
{
    public class RrhhLicenciasService
    {
        private readonly RrHhContext _context;
        private readonly IAlmacenadorAzure _almacenadorAzure;

        public RrhhLicenciasService(RrHhContext context, IAlmacenadorAzure almacenadorAzure)
        {
            _context = context;
            _almacenadorAzure = almacenadorAzure;
        }

        public async Task<List<RHListarLicenciaDto>> GetLicencias()
        {
            var result = await _context.RHLicencias
                .Include(l => l.RHColaborador)          
                .Include(l => l.RHTipoLicencias)         
                .Include(l => l.RHEstadoLicencias)       
                .Select(l => new RHListarLicenciaDto
                {
                    Id = l.IdLicencias,
                    Uuid = l.Uuid,
                    IdColaborador = l.IdColaborador,
                    IdTipoLicencia = l.IdTipoLicencia,
                    IdEstadoLicencia = l.IdEstadoLicencia,
                    NombreColaborador = l.RHColaborador.Nombres + " " + l.RHColaborador.PrimerApellido +" "+l.RHColaborador.SegundoApellido,   
                    Dpi = l.RHColaborador.Dpi,
                    TipoLicencia = l.RHTipoLicencias.TipoLicencia, 
                    FechaInicio = l.FechaInicio, 
                    FechaFin = l.FechaFin,       
                    EstadoLicencia = l.RHEstadoLicencias.EstadoLicencia,
                    Observaciones = l.Observaciones,
                    UrlConstancia = l.UrlConstancia
                })
                .ToListAsync();

            return result;
        }
        public async Task<List<RHTipoLicencias>> GetTipoLicencias()
        {
            return await _context.RHTipoLicencias
                .ToListAsync();
        }
        public async Task<List<RHEstadoLicencias>> GetEstadoLicencias()
        {
            return await _context.RHEstadoLicencias
                .ToListAsync();
        }

        public async Task CrearLicencia(int idColaborador, int idTipoLicencia, int idEstadoLicencia, DateTime fechaInicio, DateTime fechaFin, string observaciones)
        {
            try
            {
                var licencia = new RHLicencias
                {
                    Uuid = Guid.NewGuid(),
                    IdColaborador = idColaborador,
                    IdTipoLicencia = idTipoLicencia,
                    IdEstadoLicencia = idEstadoLicencia,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    Observaciones = observaciones
                };
                _context.RHLicencias.Add(licencia);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear la licencia: {ex.Message}");
            }
        }

        public async Task ActualizarLicencia(int idLicencia, int idColaborador, int idTipoLicencia, int idEstadoLicencia, DateTime fechaInicio, DateTime fechaFin, string observaciones)
        {
            try
            {
                var licenciaExiste = await _context.RHLicencias.FindAsync(idLicencia);
                if (licenciaExiste != null)
                {
                    licenciaExiste.IdColaborador = idColaborador;
                    licenciaExiste.IdTipoLicencia = idTipoLicencia;
                    licenciaExiste.IdEstadoLicencia = idEstadoLicencia;
                    licenciaExiste.FechaInicio = fechaInicio;
                    licenciaExiste.FechaFin = fechaFin;
                    licenciaExiste.Observaciones = observaciones;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"No se encontró la licencia con ID: {idLicencia}");

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la licencia: {ex.Message}");
            }
        }   

        public async Task SubirConstancia(int idLicencia, IFormFile archivo)
        {
            var licencia = await _context.RHLicencias.FindAsync(idLicencia);
            if (licencia == null)
            {
                throw new Exception($"No se encontró la licencia con ID: {idLicencia}");
            }

            // Si ya existe una constancia, eliminarla antes de subir la nueva.
            if (!string.IsNullOrEmpty(licencia.UrlConstancia))
            {
                await _almacenadorAzure.EliminarImagen(licencia.UrlConstancia, "rrhh");
            }

            var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();
            if (extension != ".pdf" && extension != ".jpg" && extension != ".png")
            {
                throw new Exception("El archivo debe ser de tipo PDF, JPG o PNG.");
            }

            if (archivo.Length > 10 * 1024 * 1024) // 10 MB
            {
                throw new Exception("El tamaño del archivo no puede exceder los 10 MB.");
            }

            var nombreArchivo = await _almacenadorAzure.GuardarImagen("rrhh", archivo, licencia.Uuid);

            licencia.UrlConstancia = nombreArchivo;
            licencia.IdEstadoLicencia = 2; // 2 = Aprobado

            await _context.SaveChangesAsync();
        }

        public async Task<string> ObtenerUrlConstancia(int idLicencia)
        {
            var licencia = await _context.RHLicencias.FindAsync(idLicencia);
            if (licencia == null || string.IsNullOrEmpty(licencia.UrlConstancia))
            {
                return null;
            }

            return await _almacenadorAzure.ObtenerUrlConSas("rrhh", licencia.UrlConstancia);
        }

    }
}
