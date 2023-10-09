using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using VirtualClients_API.ContextDb;
using VirtualClients_API.Models;
using VirtualClients_API.Models.ClasesEspeciales;
using VirtualClients_API.Models.Dtos;

namespace VirtualClients_API.Services
{
    public class ClienteService
    {
        private readonly AppDbContext _context;
        protected readonly APIResponse _response;
        public ClienteService(AppDbContext context)
        {
            _context = context;
            _response = new();
        }

        public async Task<APIResponse> ListarClientes()
        {
            try
            {
                var lista = await _context.Clientes.FromSqlInterpolated($"Exec sp_ListarClientes").ToListAsync();
                _response.Resultado = lista;
                _response.StatusCode = HttpStatusCode.OK;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ExceptionMessages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }

        public async Task<APIResponse> GetById(int id)
        {
            try
            {
                var cliente = await _context.Clientes.FromSqlRaw($"Exec sp_ObtenerCliente @id={id}").ToListAsync();
                if (cliente.Count == 0)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorNotFound(id);
                    return _response;
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Resultado = cliente;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ExceptionMessages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }

        public async Task<APIResponse> GetClientesTotal()
        {
            try
            {
                var result = await _context.Set<ClienteTotal>().ToListAsync();
                _response.Resultado = result;
                _response.StatusCode = HttpStatusCode.OK;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ExceptionMessages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }

        public async Task<APIResponse> Guardar(ClienteDtoCreate clienteDto)
        {
            try
            {
                var parametroId = new SqlParameter("@id", SqlDbType.Int);
                parametroId.Direction = ParameterDirection.Output;

                await _context.Database.ExecuteSqlInterpolatedAsync($@"Exec sp_GuardarCliente
                                        @nombre={clienteDto.Nombre}, @apellido={clienteDto.Apellido},
                                        @estatus={clienteDto.Estatus}, @id={parametroId} Output");

                var id = (int)parametroId.Value;
                if ( id > 0 )
                {
                    _response.StatusCode = HttpStatusCode.Created;
                    _response.Guardado(id);
                    return _response;
                }
                else
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorGuardar();
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ExceptionMessages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }

        public async Task<APIResponse> Editar(ClienteDtoUpdate clienteDto)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlInterpolatedAsync($@"Exec sp_ActualizarCliente
                                        @nombre={clienteDto.Nombre}, @apellido={clienteDto.Apellido},
                                        @estatus={clienteDto.Estatus}, @id={clienteDto.Id}");
                if (result > 0)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.Editado();
                    return _response;
                }
                else
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorEditar();
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ExceptionMessages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id}")]
        public async Task<APIResponse> Borrar(int id)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlInterpolatedAsync($@"Exec sp_EliminarCliente @id={id}");
                
                if (result > 0)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.Eliminado();
                    return _response;
                }
                else
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorEliminar();
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ExceptionMessages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }
    }
}
