using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyLoginWhitJWT.DataTransferObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyLoginWhitJWT.Controllers
{
    /// <summary>
    /// en este controlador iran todos los elementos que se solicitan para poder tener acceso a las cuentas de usuario como loggearse y demas
    /// </summary>
    public class AccountsController : Controller
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;



        public AccountsController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        /// <summary>
        /// para logearse si existe el token se logea solo
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> login()
        {

            var Token =HttpContext.Request.Cookies["Token"];


            if(String.IsNullOrEmpty(Token))
                return View();
            else
                return RedirectToAction("Index", "Home");



        }

        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> Logear(UserLogin inf)
        {

            HttpClient client = new();
            try
            {

                HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44318/API/Cuentas/login", inf);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                Console.WriteLine(responseBody);
                var token =  JsonConvert.DeserializeObject<UserToken>(responseBody);
                //guardalo en la cookie
                HttpContext.Response.Cookies.Append("Token", token.Token, new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(6)
                });

                var user =  await _userManager.FindByEmailAsync(inf.Email);
                await _signInManager.PasswordSignInAsync(user, inf.Password, false, false);

                return RedirectToAction("Index", "Home");

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\n HTTP  Exception Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return View("login");
            }
            finally
            {
                client.Dispose();

            }

        }


    }
}
