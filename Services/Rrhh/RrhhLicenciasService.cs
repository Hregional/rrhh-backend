using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.DTOs;
using rrhh_backend.Data.Models;

namespace rrhh_backend.Services.Rrhh
{
    public class RrhhLicenciasService
    {
        private readonly RrHhContext _context;

        public RrhhLicenciasService(RrHhContext context)
        {
            _context = context;
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
                    Observaciones = l.Observaciones
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

    }
}
