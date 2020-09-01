<center>

### Renta de Peliculas ![foo](https://img.shields.io/jenkins/s/https/builds.apache.org/job/commons-lang.svg)
>### Api segura con Json Web Token

</center>
<hr>

### Importante

```
El jwt, crear un array claim donde se setean los roles
para ser usados en authorization(Roles=).
```
### Ejmplo de Claim 
```csharp
var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Nombre+" "+userInfo.Apellido),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,userInfo.Rol.ToString()),
                new Claim(ClaimTypes.PrimarySid,userInfo.Idusuario.ToString())
            };
```
### Agregando los Claim al token.

```csharp
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires:DateTime.Now.AddMinutes(120),
                signingCredentials:credentials
                );
```
### Using utilizados para la creacion de token
```csharp
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
```
### Roles
<pre>
- 1 (usuario standar)
- 2 (administrador)
</pre>
### Segurad en el controlador
>Ejemplo de controlador con autorización. 
```csharp
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "1,2")]
    public class PeliculaController : ControllerBase
    {
        private Models.BD.RENTMOVIEContext renta = new Models.BD.RENTMOVIEContext();
        [HttpGet]
        public IEnumerable<Models.BD.Pelicula> Get()
        {
            return renta.Pelicula.ToList();
        }
    }
```