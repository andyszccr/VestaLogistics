using VestaLogistics.Business.Extensions;
using VestaLogistics.Business.Services;
using VestaLogistics.Data;
using VestaLogistics.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Tenant: resuelve EmpresaID por request (claims, subdominio, header, etc.)
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantContext, TenantContext>();

// Data (DbContext y repositorios): registro vía Business → Data
builder.Services.AddVestaLogisticsData(builder.Configuration);

// API externa: tipo de cambio Hacienda (registro vía Business → Data; el valor fluye Data → Business → Web)
builder.Services.AddHaciendaTipoCambio(builder.Configuration);

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
