using Microsoft.EntityFrameworkCore;
using VestaLogistics.Business.Clients;
using VestaLogistics.Business.Services;
using VestaLogistics.Data;
using VestaLogistics.Web.Clients;
using VestaLogistics.Web.Options;
using VestaLogistics.Web.Services;

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

// API externa: tipo de cambio Hacienda (https://api.hacienda.go.cr/indicadores/tc/dolar)
builder.Services.Configure<HaciendaTipoCambioOptions>(
    builder.Configuration.GetSection(HaciendaTipoCambioOptions.SectionName));
builder.Services.AddHttpClient<ITipoCambioExternoClient, HaciendaTipoCambioClient>();

// Servicios de negocio
builder.Services.AddScoped<ITipoCambioService, TipoCambioService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();

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

// API: controladores con [Route("api/...")]
app.MapControllers();

// MVC: páginas y ruta por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
