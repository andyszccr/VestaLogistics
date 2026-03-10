using Microsoft.AspNetCore.Mvc;
using VestaLogistics.Business.Services;
using VestaLogistics.Entities.Plataforma;

namespace VestaLogistics.Web.Controllers;

public class EmpresaController : Controller
{
    private readonly IEmpresaService _empresaService;

    // TODO: Obtener del usuario logueado. Usaremos 1 temporalmente.
    private const int UsuarioIdAuditoria = 1;

    public EmpresaController(IEmpresaService empresaService)
    {
        _empresaService = empresaService;
    }

    public async Task<IActionResult> Index()
    {
        var empresas = await _empresaService.GetAllAsync(incluirInactivas: true);
        return View(empresas);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Empresa model)
    {
        if (ModelState.IsValid)
        {
            await _empresaService.CreateAsync(model.NombreComercial, model.CedulaJuridica, UsuarioIdAuditoria);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var empresa = await _empresaService.GetByIdAsync(id, incluirInactivas: true);
        if (empresa == null)
        {
            return NotFound();
        }
        return View(empresa);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Empresa model)
    {
        if (id != model.EmpresaID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _empresaService.UpdateAsync(id, model.NombreComercial, model.Estado ?? true, UsuarioIdAuditoria);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var empresa = await _empresaService.GetByIdAsync(id, incluirInactivas: true);
        if (empresa == null)
        {
            return NotFound();
        }
        return View(empresa);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _empresaService.DeleteAsync(id, UsuarioIdAuditoria);
        return RedirectToAction(nameof(Index));
    }
}
