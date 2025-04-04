using L02P02_2022_EA_650_2022_RC_652.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

// Configurar el DbContext usando la cadena de conexión del appsettings.json
builder.Services.AddDbContext<libroContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("libroDbConnection")));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configurar el pipeline HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

// Ruta por defecto, que se puede ajustar si lo necesitas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=libro}/{action=Index}/{id?}");

app.Run();
