using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyLoginWhitJWT.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyLoginWhitJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {

        #region declaraciones

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// contructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="configuration"></param>
        public CuentasController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        #endregion

        #region EndPoints usuario

        /// <summary>
        /// para crear un usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost("Crear")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserLogin model)
        {
            //en el acso de necesitar otro elemento de identidad deberia de ir aqui; 
            //este elemeto deberia de ir en el archivo donde s hacen todas las conecciones
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return new UserToken(model, new List<string>(),_configuration);
            }
            else
            {
                return BadRequest("Correo Ya En Uso");
            }

        }

        /// <summary>
        /// para logearte como usuario
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserLogin userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var usuario = await _userManager.FindByEmailAsync(userInfo.Email);
                var roles = await _userManager.GetRolesAsync(usuario);
                return new UserToken(userInfo, roles,_configuration);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error En Datos De Acceso");
                return BadRequest(ModelState);
            }
        }

        #endregion

    }
}
