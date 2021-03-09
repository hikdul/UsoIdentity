using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLoginWhitJWT.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ValuesController : ControllerBase
    {


        [HttpGet]
        public string GET()
        {
            if (User.IsInRole("Admin"))
                return User.Identity.Name + " Esta Si Es La Clave Secreta: ESTUDIA MUCHO CARAJO";
            else
                return "Esta es la clave secreta";
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Admin")]

        [HttpGet("log/{id}")]
        public string GETById(int id)
        {
                
            return "El Valor secreto es: " + id;

        }

    }
}
