using Microsoft.EntityFrameworkCore;
using VestaLogistics.Data;
using VestaLogistics.Web.Services;
using VestaLogistics.Business.Services;

var builder = WebApplication.CreateBuilder(args);

// Base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Tenant: resuelve EmpresaID por request (claims, subdominio, header, etc.)
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantContext, TenantContext>();

// DbContext con soporte Multitenant. El contenedor inyecta ITenantContext en el constructor del DbContext.
builder.Services.AddDbContext<VestaLogisticsDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Repositorios
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();

// Servicios de negocio
builder.Services.AddScoped<ITipoCambioService, TipoCambioService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
