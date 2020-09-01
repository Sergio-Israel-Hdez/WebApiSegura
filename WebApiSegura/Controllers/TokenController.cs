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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TokenController : ControllerBase
    {
        
        public ActionResult<IEnumerable<string>> Get()
        {
            var currentUser = HttpContext.User;
            int speandingTimewithCompany = 0;

            if (currentUser.HasClaim(x=>x.Type == "DateOfJoin"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                speandingTimewithCompany = DateTime.Today.Year - date.Year;
            }
            if (speandingTimewithCompany>5)
            {
                return new string[] { "High Time1", "High Time2", "High Time3", "High Time4", "High Time5" };
            }
            else
            {
                return new string[] { "value1", "value2", "value3", "value4", "value5" };
            }
        }
    }
}
