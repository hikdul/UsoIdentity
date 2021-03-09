using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLoginWhitJWT.Controllers
{
    /// <summary>
    /// en este controlador iran todos los elementos que se solicitan para poder tener acceso a las cuentas de usuario como loggearse y demas
    /// </summary>
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
