using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using VirtualClients_API.ContextDb;
using VirtualClients_API.Models.Dtos;
using VirtualClients_API.Models;
using Microsoft.EntityFrameworkCore;

namespace VirtualClients_API.Services
{
    public class CondicionService
    {
        private readonly AppDbContext _context;
        protected readonly APIResponse _response;
        public CondicionService(AppDbContext context)
        {
            _context = context;
            _response = new();
        }

        public async Task<APIResponse> ListarCondicion()
        {
            try
            {
                var lista = await _context.Condicions.FromSqlInterpolated($"Exec sp_ListarCondicion").ToListAsync();
                _response.Result = lista;
                _response.StatusCode = HttpStatusCode.OK;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Messages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }

        public async Task<APIResponse> GetById(int id)
        {
            try
            {
                var condicion = await _context.Condicions.FromSqlInterpolated($"Exec sp_ObtenerCondicion @id={id}").ToListAsync();

                if (condicion.Count == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return _response;
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = condicion;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Messages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }

        public async Task<APIResponse> Guardar(CondicionDtoCreate condicionDto)
        {
            try
            {
                var parametroId = new SqlParameter("@id", SqlDbType.Int);
                parametroId.Direction = ParameterDirection.Output;

                await _context.Database.ExecuteSqlInterpolatedAsync($@"Exec sp_GuardarCondicion
                                        @estatus={condicionDto.Estatus}, @id={parametroId} Output");

                var id = (int)parametroId.Value;
                if (id > 0)
                {
                    _response.Result = condicionDto;
                    _response.StatusCode = HttpStatusCode.Created;
                    return _response;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Messages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }

        public async Task<APIResponse> Editar(CondicionDtoUpdate condicionDto)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlInterpolatedAsync($@"Exec sp_ActualizarCondicion
                                        @estatus={condicionDto.Estatus}, @id={condicionDto.Id}");
                if (result > 0)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return _response;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Messages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id}")]
        public async Task<APIResponse> Borrar(int id)
        {
            try
            {
                var result = await _context.Database.ExecuteSqlInterpolatedAsync($@"Exec sp_EliminarCondicion @id={id}");

                if (result > 0)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return _response;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Messages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }
    }
}
