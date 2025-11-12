using System;
using System.Threading.Tasks;
using rrhh_backend.Data;
using rrhh_backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace rrhh_backend.Services.Administracion
{
    public class AuditoriaService : IAuditoriaService
    {
        private readonly RrHhContext _context;

        public AuditoriaService(RrHhContext context)
        {
            _context = context;
        }

        public async Task RegistrarCambioEstatus(int trabajadorId, int idEstatusAnterior, int idEstatusNuevo, string motivo, string sistemaEjecutor)
        {
            var auditoria = new AuditoriaEstatus
            {
                TrabajadorId = trabajadorId,
                FechaCambio = DateTime.Now,
                IdEstatusAnterior = idEstatusAnterior,
                IdEstatusNuevo = idEstatusNuevo,
                Motivo = motivo,
                SistemaEjecutor = sistemaEjecutor
            };

            _context.AuditoriaEstatus.Add(auditoria);
            await _context.SaveChangesAsync();
        }
    }
}
