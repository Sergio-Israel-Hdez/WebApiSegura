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
    }
}
