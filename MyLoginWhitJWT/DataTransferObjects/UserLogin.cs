using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLoginWhitJWT.DataTransferObjects
{
    /// <summary>
    /// clase para transferir datos de login
    /// </summary>
    public class UserLogin
    {
        public string  Email { get; set; }
        public string Password { get; set; }

    }
}
