using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiSegura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="1")]
    public class ReservaController : ControllerBase
    {
        private Models.BD.RENTMOVIEContext db = new Models.BD.RENTMOVIEContext();
        
        [HttpPost]
        [Route("[action]")]
        public IEnumerable<dynamic> GetReservas([FromBody]Models.BD.Usuario usuario)
        {
            var result = (from re in db.Reserva
                          join pe in db.Pelicula on re.Idpelicula equals pe.Idpelicula
                          join ca in db.Categoria on pe.Idcategoria equals ca.Idcategoria
                          where re.Idusuario == usuario.Idusuario & re.Estado == 1
                          select new
                          {
                              re.Idreserva,
                              pe.Nombre,
                              pe.Rating,
                              Categoria = ca.Nombre,
                              pe.Director,
                              re.FechaRegis
                          });
            return result.AsEnumerable();
        }
        [HttpPost]
        public IActionResult Post([FromBody]Models.BD.Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            db.Reserva.Add(reserva);
            var result = db.SaveChanges();
            return Ok(reserva);

        }
        [HttpDelete("{id}")]
        public void Delete(int? id)
        {
            if (id!=null)
            {
                Models.BD.Reserva ReservaFind = db.Reserva.Find(id);
                ReservaFind.Estado = 0;
                db.SaveChanges();
            }
        }
    }
}
