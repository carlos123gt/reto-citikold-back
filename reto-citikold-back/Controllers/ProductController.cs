using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reto_citikold_back.Data;
using reto_citikold_back.Models;
using reto_citikold_back.Models.dto;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace reto_citikold_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;



        public ProductController(AppDbContext db)
        {
            _db = db;
        }



        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var Productes = _db.Products.ToList();
            return Ok(Productes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var Producte = _db.Products.FirstOrDefault(v => v.idProducto == id);
            if (Producte == null)
                return NotFound();

            return Ok(Producte);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductDTO ProducteDTO)
        {
            if (_db.Products.FirstOrDefault(v => v.nombre.ToLower() == ProducteDTO.nombre) != null)
            {
                ModelState.AddModelError("nombre ya Existe", "nombre ta existe registrado");
                return BadRequest(ModelState);
            }

            Product model = new()
            {

                nombre = ProducteDTO.nombre,
                codigo = ProducteDTO.codigo,
                precio = ProducteDTO.precio,
                stock = ProducteDTO.stock,
                activo = ProducteDTO.activo,
                fechaCreacion = DateTime.Now

            };
            _db.Products.Add(model);
            _db.SaveChanges();

            return NoContent();
        }



        [HttpDelete("{nombre}")]
        public async Task<IActionResult> Eliminar(string nombre)
        {
            var Product = _db.Products.FirstOrDefault(v => v.nombre.ToLower() == nombre);
            if (Product != null)
            {
                _db.Products.Remove(Product);
                _db.SaveChanges();
            }

            return NoContent();
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProductDTO ProducteDTO)
        {
            if (id != ProducteDTO.idProducto)
                return BadRequest();

            var Product = _db.Products.FirstOrDefault(c => c.idProducto == id);

            if (Product != null)
            {
                // Actualiza las propiedades del Producte existente
                Product.nombre = ProducteDTO.nombre;
                Product.codigo = ProducteDTO.codigo;
                Product.precio = ProducteDTO.precio;
                Product.stock = ProducteDTO.stock;
                Product.activo = ProducteDTO.activo;
                Product.fechaCreacion = new DateTime();

                _db.Products.Update(Product);
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
