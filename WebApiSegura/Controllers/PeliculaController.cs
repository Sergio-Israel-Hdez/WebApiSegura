using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiSegura.Models;
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
            return renta.Pelicula.ToList();
        }
        [HttpPost]
        [Route("[action]")]
        public IEnumerable<Models.BD.Pelicula>getPeliculasUsuario([FromBody]Models.BD.Usuario usuario)
        {
            var subselect = (from re in renta.Reserva where re.Idusuario==usuario.Idusuario && re.Estado!=0 select re.Idpelicula).ToList();
            var result = from pe in renta.Pelicula where !subselect.Contains(pe.Idpelicula) select pe;
            return result;
        }
    }
}
