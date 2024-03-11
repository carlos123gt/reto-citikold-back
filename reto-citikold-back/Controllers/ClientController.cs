using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reto_citikold_back.Data;
using reto_citikold_back.Models;
using reto_citikold_back.Models.dto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace reto_citikold_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly AppDbContext _db;



        public ClientController(AppDbContext db)
        {
            _db = db;
        }



        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var clientes = _db.Clients.ToList();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var cliente = _db.Clients.FirstOrDefault(v => v.idCliente == id);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ClientDTO clienteDTO)
        {
            if (_db.Clients.FirstOrDefault(v => v.rucDni.ToLower() == clienteDTO.rucDni.ToLower()) != null)
            {
                ModelState.AddModelError("RUc ya Existe", "ruc ta existe registrado");
                return BadRequest(ModelState);
            }
            else { 

                    Client model = new()
                    {

                        nombre = clienteDTO.nombre,
                        rucDni = clienteDTO.rucDni,
                        correo = clienteDTO.correo,
                        activo = clienteDTO.activo,
                        direccion = clienteDTO.direccion,
                        fechaCreacion = DateTime.Now

                    };
                    _db.Clients.Add(model);
                    _db.SaveChanges();

                    return NoContent();
            }
        }



        [HttpDelete("{rucDni}")]
        public async Task<IActionResult> Eliminar(string rucDni)
        {
            var client = _db.Clients.FirstOrDefault(v => v.rucDni.ToLower() == rucDni);
            if (client != null)
            {
                _db.Clients.Remove(client);
                _db.SaveChanges();
            }

            return NoContent();
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ClientDTO clienteDTO)
        {
            if (id != clienteDTO.idCliente)
                return BadRequest();

            var client = _db.Clients.FirstOrDefault(c => c.idCliente == id);

            if (client != null)
            {
                // Actualiza las propiedades del cliente existente
                client.nombre = clienteDTO.nombre;
                client.rucDni = clienteDTO.rucDni;
                client.correo = clienteDTO.correo;
                client.activo = clienteDTO.activo;
                client.direccion = clienteDTO.direccion;

                _db.Clients.Update(client);
                _db.SaveChanges();

                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }

    }
