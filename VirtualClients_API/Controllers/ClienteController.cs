using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using VirtualClients_API.ContextDb;
using VirtualClients_API.Models;
using VirtualClients_API.Models.ClasesEspeciales;
using VirtualClients_API.Models.Dtos;

namespace VirtualClients_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ClienteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetAll()
        {
            var lista = await _context.Clientes.FromSqlInterpolated($"Exec sp_ListarClientes").ToListAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var cliente = await _context.Clientes.FromSqlRaw($"Exec sp_ObtenerCliente @id={id}").ToListAsync();
            if (cliente.Count == 0)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpGet("general")]
        public async Task<ActionResult<IEnumerable<ClienteTotal>>> GetTotal()
        {
            var result = await _context.Set<ClienteTotal>().ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post (ClienteDtoCreate clienteDto)
        {
            var parametroId = new SqlParameter("@id", SqlDbType.Int);
            parametroId.Direction = ParameterDirection.Output;

            await _context.Database.ExecuteSqlInterpolatedAsync($@"Exec sp_GuardarCliente
                                        @nombre={clienteDto.Nombre}, @apellido={clienteDto.Apellido},
                                        @estatus={clienteDto.Estatus}, @id={parametroId} Output");

            var id = (int)parametroId.Value;
            return Ok(id);
        }

        [HttpPut("update")]
        public async Task<ActionResult<int>> Put(ClienteDtoUpdate clienteDto)
        {
            var result = await _context.Database.ExecuteSqlInterpolatedAsync($@"Exec sp_ActualizarCliente
                                        @nombre={clienteDto.Nombre}, @apellido={clienteDto.Apellido},
                                        @estatus={clienteDto.Estatus}, @id={clienteDto.Id}");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var result = await _context.Database.ExecuteSqlInterpolatedAsync($@"Exec sp_EliminarCliente @id={id}");
            return Ok(result);
        }
    }
}