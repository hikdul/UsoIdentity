

# Pasos para trabajar con esto

 Aqui van la lista de pasos y el orden en que los hice para que esto funciones, basicamente tome el uso de la libreria y lo que deseaba era generar una forma de trabajar con MVC y tener mis web api en el mismo segmento de codigo ya que es como me lo piden en la empresa en la que trabajo.

## Pasos para Generar Mis JWT Tokens

estos son los pasos para poder conseguir usar jwt token en un projecto de .net con mvc

0.intalar el paquete nuget _Microsoft.AspNetCore.Authentication.JwtBearer_
1. intalar los paquetes:
    * _Microsoft.AspNetCore.Identity.EntityFrameworkCore_
    * _Microsoft.EntityFrameworkCore.SqlServer_
    * _Microsoft.EntityFrameworkCore.tools_
Luego agregar el string conection de la base de datos:
```
 "ConnectionStrings": {
    "DefaultConnection": "Server=LAPTOP-LBB5660H;Database=[NameDataDase]];Trusted_Connection=True;MultipleActiveResultSets=true"
  },
```
Tambien crear la instancia de bases de datos o en la carpeta data
```
  public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
```

Y configurar la base de datos
```
 services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

```

2.Hay que generar las clases **(Para mi DTO)** de este proyecto que son:
    *UserToken : En esta clae va la logica de construccion de un Token
    *UserInfo
3.Luego se genera el controlador llamado CuentasController   
4.Ahora Configurar el la clase Startup lo siguiente **(Nota Personal, Verificar lo escrito ya que con lo generado al inicio caus un error del coño)** 

```
  services.AddIdentity<IdentityUser,IdentityRole>(options =>
   options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

```

5. Configurar la lectura de nuestros tokens en la clase Staptup:

```

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( O =>
                {
                    O.SaveToken = true;
                    O.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                        ClockSkew = TimeSpan.Zero
                    };              
                });

```
6. En Este punto hay que generar nuestra clave con ubicacion en **jwt:key**; yo coloque esta en este ejemplo _3st43sl4CL4V3D3US0P4R4T0K3N23NN3TC0R353L3G1D4P0RM13sp3r0func10n3_ pero debe de ser larga  y viable

```
 "jwt": {
    "key": "3st43sl4CL4V3D3US0P4R4T0K3N23NN3TC0R353L3G1D4P0RM13sp3r0func10n33st43sl4CL4V3D3US0P4R4T0K3N23NN3TC0R353L3G1D4P0RM13sp3r0func10n3"
  },
```
7.Aqui explora asi que vamos a ver generar la migracion:

```
    Add-Migration [Nombre Que Deseas]
```

Y luego actualizar la base de datos
```
    Update-Database
```


YA bien aqui _leemos y generamos tokens_ pero ahora estamos completamente deslogeados, por eso tenemos que generar un controlador de cuentas para el lado del usuario o **front end** ; esto se ara al final por ahora necesitamos asignar un rol de usuario


## Asignar roles a nuestros usuario

0. Como primero debemos de crear la semilla de datos con esto.. are un punto aparte donde solo agege la semilla de datos para cada uno

1. se agrega El controlador _UsuarioControler.cs_ este contendra todo lo necesario para asignar y remover a un usuario de un rol.. en este ejemplo esto se encuentra expuesto pero hay que colocar esto bajo autorizacion de un rol super para que funciones 

2. solo se usa 1 solo **Data Transfer Object** que aqui llamamo _EditarRolDTO.cs_ y contiene lo siguiente:

```
  public class EditarRolDTO
        {
            public string UserId { get; set; }
            public string RoleName { get; set; }
        }
```



## Para Login en vistas

1. Cree el controlador de AccountsController.cs alli basicamente jugue; esta parte esta a mitad de camino pero me llevo **1 dia** completo hacer esto