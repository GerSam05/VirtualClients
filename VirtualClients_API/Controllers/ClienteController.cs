﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VirtualClients_API.Models;
using VirtualClients_API.Models.Dtos;
using VirtualClients_API.Services;

namespace VirtualClients_API.Controllers
{
    [EnableCors("CorsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _service;
        protected readonly APIResponse _response;
        public ClienteController(ClienteService service)
        {
            _service = service;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status206PartialContent)]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            var response = await _service.ListarClientes();
            if (response.IsSuccess == true)
            {
                return Ok(response);
            }
            return StatusCode(StatusCodes.Status206PartialContent, response);
        }

        [HttpGet("tabla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status206PartialContent)]
        public async Task<ActionResult<APIResponse>> GetTable()
        {
            var response = await _service.GetClientesTable();
            if (response.IsSuccess == true)
            {
                return Ok(response);
            }
            return StatusCode(StatusCodes.Status206PartialContent, response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status206PartialContent)]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            var response = await _service.GetById(id);
            if (response.IsSuccess == true)
            {
                return Ok(response);
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return StatusCode(StatusCodes.Status206PartialContent, response);
            }
            return StatusCode(StatusCodes.Status206PartialContent, response);
        }

        [HttpGet("tabla/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status206PartialContent)]
        public async Task<ActionResult<APIResponse>> GetInclude(int id)
        {
            var response = await _service.GetTableById(id);
            if (response.IsSuccess == true)
            {
                return Ok(response);
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return StatusCode(StatusCodes.Status206PartialContent, response);
            }
            return StatusCode(StatusCodes.Status206PartialContent, response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status206PartialContent)]
        public async Task<ActionResult<APIResponse>> Post (ClienteDtoCreate clienteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.Guardar(clienteDto);

            if (response.IsSuccess == true)
            {
                return StatusCode(StatusCodes.Status201Created, response);
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return StatusCode(StatusCodes.Status206PartialContent, response);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status206PartialContent)]
        public async Task<ActionResult<APIResponse>> Put(ClienteDtoUpdate clienteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.Editar(clienteDto);

            if (response.IsSuccess == true)
            {
                return Ok(response);
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return StatusCode(StatusCodes.Status206PartialContent, response);
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status206PartialContent)]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            var response = await _service.Borrar(id);

            if (response.IsSuccess == true)
            {
                return Ok(response);
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(response);
            }
            return StatusCode(StatusCodes.Status206PartialContent, response);
        }
    }
}