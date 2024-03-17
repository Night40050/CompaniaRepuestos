using CompaniaRepuestos.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CompaniaRepuestos.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CompaniaRepuestosContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "CompaniaRepuestosCookie"; // Establece el esquema predeterminado
    options.DefaultSignInScheme = "CompaniaRepuestosCookie"; // Establece el esquema para inicio de sesión

})
        .AddCookie("CompaniaRepuestosCookie", options =>
        {
            options.LoginPath = "/Home/Index"; // Página de inicio de sesión
            options.AccessDeniedPath = "/Home/Index"; // Página de acceso denegado
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequiereLogin", policy => policy.RequireAuthenticatedUser());
});

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configuración de la Autenticación y Autorización
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "CompaniaRepuestosCookie";
        options.LoginPath = "/InicioSesion";
        options.AccessDeniedPath = "/AccesoDenegado";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
    options.AddPolicy("Usuario", policy => policy.RequireRole("Usuario"));
});


var app = builder.Build();
//iniciar base de datos con los roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CompaniaRepuestosContext>();
        SeedData.Initialize(services);
    }
    catch (SqlException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
