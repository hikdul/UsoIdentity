using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyLoginWhitJWT.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using static MyLoginWhitJWT.DataTransferObjects.EditarRolDto;

namespace MyLoginWhitJWT.Controllers
{
    /// <summary>
    /// para asignar usuarios a un rol
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public UsuariosController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Route("AsignarUsuarioRol")]
        public async Task<ActionResult> AsignarRolUsuario(EditarRolDTO edit)
        {
            var usuario = await userManager.FindByIdAsync(edit.UserId);
            // este es para el uso clasico de identity por scanfolding -- aunque a mi no me agarra esa huevonada.. 
            await userManager.AddClaimAsync(usuario, new Claim(ClaimTypes.Role, edit.RoleName));
            // y con este es para usarlo con nuesto jwt
            await userManager.AddToRoleAsync(usuario, edit.RoleName);
            return Ok();
        }

        [Route("RemoverUsuarioRol")]
        public async Task<ActionResult> RemoverUsuarioRol(EditarRolDTO editarRolDTO)
        {
            var usuario = await userManager.FindByIdAsync(editarRolDTO.UserId);
            await userManager.RemoveClaimAsync(usuario, new Claim(ClaimTypes.Role, editarRolDTO.RoleName));
            await userManager.RemoveFromRoleAsync(usuario, editarRolDTO.RoleName);
            return Ok();
        }
    }

    
}
