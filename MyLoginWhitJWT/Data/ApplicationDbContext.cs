using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLoginWhitJWT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        #region Tablas Del Negocio


        #endregion


        #region creacion de semillas 

        /// <summary>
        /// para crear nuestos modelos y SuperAdmin User por Defecto
        /// </summary>
        /// <param name="builder"></param>
        //protected override void OnModelCreating(ModelBuilder builder)
        //{

        //    var SuperAdmin = new IdentityUser()
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Email = "desarrollo@automatismoslau.cl",
        //        NormalizedEmail = "desarrollo@automatismoslau.cl".ToUpper(),
        //        UserName = "desarrollo@automatismoslau.cl",
        //        NormalizedUserName = "desarrollo@automatismoslau.cl".ToUpper(),
        //        EmailConfirmed = true,
        //        PhoneNumber = "+56 9 3315 8879",
        //        PhoneNumberConfirmed = true,

        //    };

        //    var RoleAdmin = new IdentityRole()
        //    {

        //        Id = Guid.NewGuid().ToString(),
        //        Name = "Admin",
        //        NormalizedName = "ADMIN"

        //    };



        //    base.OnModelCreating(builder);
        //}

        #endregion

    }
}
