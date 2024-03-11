using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reto_citikold_back.Data;
using reto_citikold_back.Models.dto;
using reto_citikold_back.Models;

namespace reto_citikold_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _db;



        public UserController(AppDbContext db)
        {
            _db = db;
        }


        [HttpPost]
        public async Task<IActionResult> ObtenerPorId([FromBody] UserCitikoldDTO userLoggin)
        {
            var user = _db.User.FirstOrDefault(v => v.username == userLoggin.username);
            if (user == null)
            {
                ModelState.AddModelError("Usuario no existe", "Usuario no registrado");
                return BadRequest(ModelState);
            }
            else
            {
                if (user.password == userLoggin.password && user.intentosIncorrecto <= 3)
                {
                    return Ok(user);
                }
                else
                {
                    user.intentosIncorrecto = user.intentosIncorrecto + 1;
                    user.activo = false;
                    if (user.intentosIncorrecto <= 3)
                    {
                        ModelState.AddModelError("Contraseña Incorrecta", "intente de nuevo");
                    }
                    else
                    {
                        ModelState.AddModelError("Usuario Bloqueado", "Comuniquese con el administrador");
                    }

                  //  _db.User.Add(user);
                    _db.SaveChanges();
                    return BadRequest(ModelState);
                }
            }
                


        
        }
    }
}
