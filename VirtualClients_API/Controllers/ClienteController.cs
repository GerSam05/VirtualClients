using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using VirtualClients_API.ContextDb;
using VirtualClients_API.Models;
using VirtualClients_API.Models.ClasesEspeciales;
using VirtualClients_API.Models.Dtos;
using VirtualClients_API.Services;

namespace VirtualClients_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _service;
        protected readonly APIResponse _response;
        public ClienteController(ClienteService service, AppDbContext context)
        {
            _service = service;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetAll()
        {
            var result = await _service.ListarClientes();
            if (result.IsExitoso == true)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var result = await _service.GetById(id);
            if (result.IsExitoso == true)
            {
                return Ok(result);
            }
            if (result.ErrorMessage != null)
            {
                return NotFound(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
        
        [HttpGet("general")]
        public async Task<ActionResult<IEnumerable<ClienteTotal>>> GetTotal()
        {
            var result = await _service.GetClientesTotal();
            if (result.IsExitoso == true)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
        
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Post (ClienteDtoCreate clienteDto)
        {
            var result = await _service.Guardar(clienteDto);

            if (result.IsExitoso == true)
            {
                return StatusCode(StatusCodes.Status201Created, result);
            }
            if (result.ErrorMessage != null)
            {
                return BadRequest(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
        
        [HttpPut("update")]
        public async Task<ActionResult<APIResponse>> Put(ClienteDtoUpdate clienteDto)
        {
            var result = await _service.Editar(clienteDto);

            if (result.IsExitoso == true)
            {
                return Ok(result);
            }
            if (result.ErrorMessage != null)
            {
                return BadRequest(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            var result = await _service.Borrar(id);

            if (result.IsExitoso == true)
            {
                return Ok(result);
            }
            if (result.ErrorMessage != null)
            {
                return BadRequest(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}