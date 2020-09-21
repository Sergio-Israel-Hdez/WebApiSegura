using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiSegura.Models;
using WebApiSegura.Models.BD;

namespace WebApiSegura.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PeliculaController : ControllerBase
    {
        private Models.BD.RENTMOVIEContext renta = new Models.BD.RENTMOVIEContext();
        [Authorize(Roles ="1,2")]
        [HttpGet]
        public IEnumerable<Models.BD.Pelicula> Get()
        {
            return renta.Pelicula.ToList().Where(pe=>pe.Estado!=0);
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "1")]
        public IEnumerable<Models.BD.Pelicula>getPeliculasUsuario([FromBody]Models.BD.Usuario usuario)
        {
            var subselect = (from re in renta.Reserva where re.Idusuario==usuario.Idusuario && re.Estado!=0 select re.Idpelicula).ToList();
            var result = from pe in renta.Pelicula where !subselect.Contains(pe.Idpelicula) select pe;
            return result;
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "1,2")]
        public IQueryable<dynamic> detailPelicula([FromBody]Models.BD.Pelicula pelicula)
        {
            var result = (from pe in renta.Pelicula
                          join ca in renta.Categoria on pe.Idcategoria equals ca.Idcategoria
                          where pe.Idpelicula == pelicula.Idpelicula
                          select new
                          {
                              pe.Nombre,
                              pe.Rating,
                              CATEGORIA = ca.Nombre,
                              pe.Director,
                              pe.FechaEstreno
                          });
            return result;
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "2")]
        public IActionResult editPelicula([FromBody]Models.BD.Pelicula pelicula)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            Pelicula pelicula_old = renta.Pelicula.Find(pelicula.Idpelicula);
            pelicula_old.Nombre = pelicula.Nombre;
            pelicula_old.Rating = pelicula.Rating;
            pelicula_old.Director = pelicula.Director;
            pelicula_old.FechaEstreno = pelicula.FechaEstreno;
            pelicula_old.Idcategoria = pelicula.Idcategoria;
            pelicula_old.Estado = pelicula.Estado;
            renta.SaveChanges();
            return Ok(pelicula);
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "2")]
        public IActionResult addPelicula([FromBody] Models.BD.Pelicula pelicula)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            renta.Pelicula.Add(pelicula);
            renta.SaveChanges();
            return Ok(pelicula);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public void Delete(int? id)
        {
            if (id!=null)
            {
                Models.BD.Pelicula PeliculaFind = renta.Pelicula.Find(id);
                PeliculaFind.Estado = 0;
                renta.SaveChanges();
            }
        }
    }
}
