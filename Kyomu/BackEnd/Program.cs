using System.Text;
using BackEnd.Services.Implementaciones;
using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region BD

builder.Services.AddDbContext<KyomuContext>(optionsAction =>
                     optionsAction
                     .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

     );

builder.Services.AddDbContext<AuthDBContext>(optionsAction =>
                    optionsAction
                    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

    );

#endregion

#region Identity

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
         .AddEntityFrameworkStores<AuthDBContext>()
         .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;

});

#endregion

#region  JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

})

    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });



#endregion

#region DI
builder.Services.AddDbContext<KyomuContext>(optionsAction => optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();

builder.Services.AddScoped<IPagoDAL, PagoDAL>();
builder.Services.AddScoped<IPagoService, PagoService>();

builder.Services.AddScoped<IPedidoDAL, PedidoDAL>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddScoped<ICategoriaDAL, CategoriaDAL>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddScoped<IPlatilloDAL, PlatilloDAL>();
builder.Services.AddScoped<IPlatilloService, PlatilloService>();

builder.Services.AddScoped<IReseñaDAL, ReseñaDAL>();
builder.Services.AddScoped<IReseñaService, ReseñaService>();

builder.Services.AddScoped<IRolDAL, RolDAL>();
builder.Services.AddScoped<IRolService, RolService>();

builder.Services.AddScoped<IUsuarioDAL, UsuarioDAL>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IDetalleDAL, DetalleDAL>();
builder.Services.AddScoped<IDetalleService, DetalleService>();

builder.Services.AddScoped<IMetodoPagoDAL, MetodoPagoDAL>();
builder.Services.AddScoped<IMetodoPagoService, MetodoPagoService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
