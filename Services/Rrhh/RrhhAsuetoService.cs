using Microsoft.EntityFrameworkCore;
using rrhh_backend.Data;
using rrhh_backend.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace rrhh_backend.Services.Rrhh
{
    public class RrhhAsuetoService
    {
        private readonly RrHhContext _context;

        public RrhhAsuetoService(RrHhContext context)
        {
            _context = context;
        }

        public async Task<List<RHAsueto>> GetAllAsuetos()
        {
            return await _context.RHAsuetos.ToListAsync();
        }

        public async Task<RHAsueto> GetAsuetoById(int id)
        {
            return await _context.RHAsuetos.FindAsync(id);
        }

        public async Task<RHAsueto> CreateAsueto(RHAsueto asueto)
        {
            _context.RHAsuetos.Add(asueto);
            await _context.SaveChangesAsync();
            return asueto;
        }

        public async Task<bool> UpdateAsueto(int id, RHAsueto asueto)
        {
            if (id != asueto.Id)
            {
                return false;
            }

            _context.Entry(asueto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AsuetoExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> DeleteAsueto(int id)
        {
            var asueto = await _context.RHAsuetos.FindAsync(id);
            if (asueto == null)
            {
                return false;
            }

            _context.RHAsuetos.Remove(asueto);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<bool> AsuetoExists(int id)
        {
            return await _context.RHAsuetos.AnyAsync(e => e.Id == id);
        }
    }
}
