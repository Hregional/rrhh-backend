using Microsoft.AspNetCore.Mvc;
using rrhh_backend.Data.Models;
using rrhh_backend.Services.Rrhh;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace rrhh_backend.Controllers.Rrhh
{
    [Route("api/rrhh/asuetos")]
    [ApiController]
    public class RrhhAsuetoController : ControllerBase
    {
        private readonly RrhhAsuetoService _asuetoService;

        public RrhhAsuetoController(RrhhAsuetoService asuetoService)
        {
            _asuetoService = asuetoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RHAsueto>>> GetAllAsuetos()
        {
            return await _asuetoService.GetAllAsuetos();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RHAsueto>> GetAsuetoById(int id)
        {
            var asueto = await _asuetoService.GetAsuetoById(id);

            if (asueto == null)
            {
                return NotFound();
            }

            return asueto;
        }

        [HttpPost]
        public async Task<ActionResult<RHAsueto>> CreateAsueto(RHAsueto asueto)
        {
            var createdAsueto = await _asuetoService.CreateAsueto(asueto);
            return CreatedAtAction(nameof(GetAsuetoById), new { id = createdAsueto.Id }, createdAsueto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsueto(int id, RHAsueto asueto)
        {
            var result = await _asuetoService.UpdateAsueto(id, asueto);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsueto(int id)
        {
            var result = await _asuetoService.DeleteAsueto(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
