﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<APIResponse> GetClientesTable()
        {
            try
            {
                var result = await _context.Set<ClienteTotal>().ToListAsync();
                _response.Result = result;
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
                var clientes= await _context.Clientes.FromSqlInterpolated($"Exec sp_ObtenerCliente @id={id}").ToListAsync();
                var cliente = clientes.FirstOrDefault();

                if (cliente == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return _response;
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = cliente;
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

        public async Task<APIResponse> GetTableById(int id)
        {
            try
            {
                var clientes = await _context.Set<ClienteTotal>().ToListAsync();
                var cliente = clientes.FirstOrDefault(i => i.Id == id);

                if (cliente == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return _response;
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = cliente;
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
                    _response.Result = clienteDto;
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
                var result = await _context.Database.ExecuteSqlInterpolatedAsync($@"Exec sp_EliminarCliente @id={id}");
                
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
