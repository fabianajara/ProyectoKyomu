using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

// Configuración para el manejo de sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Registro de helpers y repositorios
builder.Services.AddHttpClient<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ICategoriaHelper, CategoriaHelper>();
builder.Services.AddScoped<IDetallePedidoHelper, DetallePedidoHelper>();
builder.Services.AddScoped<IMetodoPagoHelper, MetodoPagoHelper>();
builder.Services.AddScoped<IPagoHelper, PagoHelper>();
builder.Services.AddScoped<IPedidoHelper, PedidoHelper>();
builder.Services.AddScoped<IPlatilloHelper, PlatilloHelper>();
builder.Services.AddScoped<IReseñaHelper, ReseñaHelper>();
builder.Services.AddScoped<IRolHelper, RolHelper>();
builder.Services.AddScoped<IUsuarioHelper, UsuarioHelper>();
builder.Services.AddScoped<ICarritoHelper, CarritoHelper>();
builder.Services.AddScoped<IItemCarritoHelper, ItemCarritoHelper>();

builder.Services.AddHttpContextAccessor();

// Configuración de la autenticación basada en cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Usuario/Login";
    });

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

// Orden correcto: sesión, autenticación y autorización
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();
