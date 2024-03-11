using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reto_citikold_back.Data;
using reto_citikold_back.Models;
using reto_citikold_back.Models.dto;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace reto_citikold_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly AppDbContext _db;



        public FacturaController(AppDbContext db)
        {
            _db = db;
        }



        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var facturasConDetalles = _db.facturas
            .Include(f => f.Detalles)
            .Include(f => f.Cliente)
            .Select(f => new FacturaDTO
            {
               
                IdClient = f.IdClient,
                IdFactura = f.IdFactura,
                Total = f.Total,
                Igv = f.Igv,
                subTotal = f.SubTotal,
                Cliente = new Client
                {
                    idCliente = f.Cliente.idCliente,
                    nombre = f.Cliente.nombre,
                    rucDni =f.Cliente.rucDni,
                    correo = f.Cliente.correo
                    
                },
                Detalles = f.Detalles.Select(d => new DetalleFactura
                {
                    IdDetalle = d.IdDetalle,
                    CodigoProducto  = d.CodigoProducto,
                    NombreProducto =  d.NombreProducto,
                    Cantidad = d.Cantidad,
                }).ToList()
            })
            .ToList();

            return Ok(facturasConDetalles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var facturaConDetalles = _db.facturas
                .Include(f => f.Detalles)
                .Include(f => f.Cliente)
                .Where(f => f.IdFactura == id)
                .Select(f => new FacturaDTO
                {
                    IdClient = f.IdClient,
              
                    IdFactura = f.IdFactura,
                    Total = f.Total,
                    Igv = f.Igv,
                    subTotal = f.SubTotal,
                    Cliente = new Client
                    {
                        idCliente = f.Cliente.idCliente,
                        nombre = f.Cliente.nombre,
                        rucDni = f.Cliente.rucDni,
                        correo = f.Cliente.correo
                    },
                    Detalles = f.Detalles.Select(d => new DetalleFactura
                    {
                        IdDetalle = d.IdDetalle,
                        CodigoProducto = d.CodigoProducto,
                        NombreProducto = d.NombreProducto,
                        Cantidad = d.Cantidad,
                    }).ToList()
                })
                .FirstOrDefault();

            if (facturaConDetalles == null)
            {
                return NotFound(); // Devolver 404 si no se encuentra la factura con ese id
            }

            return Ok(facturaConDetalles);
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Factura factura)
        {
       
            if (_db.facturas.Any(f => f.IdFactura == factura.IdFactura))
            {
                ModelState.AddModelError("IdFactura ya existe", "La factura ya está registrada");
                return BadRequest(ModelState);
            }

            _db.facturas.Add(factura);

           
            foreach (var detalle in factura.Detalles)
            {
                var producto = _db.Products.First(p => p.codigo == detalle.CodigoProducto);
                producto.stock -= detalle.Cantidad;
            }

            _db.SaveChanges();

            return NoContent();
        }
        
        [HttpDelete("{idFactura}")]
        public async Task<IActionResult> Eliminar(int idFactura)
        {
            var factura = _db.facturas
         .Include(f => f.Detalles)
         .FirstOrDefault(f => f.IdFactura == idFactura);

            if (factura == null)
            {
                return NotFound(); 

            }
            _db.detalleFacturas.RemoveRange(factura.Detalles); 
            _db.facturas.Remove(factura);
            _db.SaveChanges();

            return NoContent();
        }



        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] FacturaDTO facturaDTO)
        {

            var facturaExistente = _db.facturas
                .Include(f => f.Detalles)
                .Include(f => f.Cliente)
                .FirstOrDefault(f => f.IdFactura == id);

            if (facturaExistente == null)
            {
                return NotFound(); 
            }

          
            facturaExistente.IdClient = facturaDTO.IdClient;

  
            facturaExistente.Cliente.idCliente = facturaDTO.Cliente.idCliente;
            facturaExistente.Cliente.nombre = facturaDTO.Cliente.nombre;
            facturaExistente.Cliente.rucDni = facturaDTO.Cliente.rucDni;
            facturaExistente.Cliente.correo = facturaDTO.Cliente.correo;

        
            foreach (var detalleExistente in facturaExistente.Detalles.ToList())
            {
             
                if (!facturaDTO.Detalles.Any(d => d.IdDetalle == detalleExistente.IdDetalle))
                {
                    _db.detalleFacturas.Remove(detalleExistente);
                }
            }

            foreach (var detalleDTO in facturaDTO.Detalles)
            {
               
                var detalleExistente = facturaExistente.Detalles.FirstOrDefault(d => d.IdDetalle == detalleDTO.IdDetalle);

                if (detalleExistente != null)
                {
                 
                    detalleExistente.CodigoProducto = detalleDTO.CodigoProducto;
                    detalleExistente.NombreProducto = detalleDTO.NombreProducto;
                    detalleExistente.Cantidad = detalleDTO.Cantidad;
                }
                else
                {
                   
                    facturaExistente.Detalles.Add(new DetalleFactura
                    {
                        CodigoProducto = detalleDTO.CodigoProducto,
                        NombreProducto = detalleDTO.NombreProducto,
                        Cantidad = detalleDTO.Cantidad
                    });
                }
            }

            _db.SaveChanges();

            return NoContent();
        }


        [HttpGet("{idCliente}")]
        public IActionResult ObtenerFacturasConClientes(int idCliente)
        {
            var facturasConClientes = _db.facturas.FirstOrDefault(v => v.IdClient == idCliente);

            if (facturasConClientes == null)
            {
                return NotFound();
            }

            return Ok(facturasConClientes);
        }
    }

    }
