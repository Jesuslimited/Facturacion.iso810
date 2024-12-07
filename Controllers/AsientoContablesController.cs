using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Facturacion.iso810.Models;

namespace Facturacion.iso810.Controllers
{
    public class AsientoContablesController : Controller
    {
        private readonly facturacioncompletaiso810Context _context;

        public AsientoContablesController(facturacioncompletaiso810Context context)
        {
            _context = context;
        }

        // GET: AsientoContables
        public async Task<IActionResult> Index()
        {
            var facturacioncompletaiso810Context = _context.AsientoContables.Include(a => a.Cliente).Include(a => a.Cuenta);
            return View(await facturacioncompletaiso810Context.ToListAsync());
        }

        // GET: AsientoContables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AsientoContables == null)
            {
                return NotFound();
            }

            var asientoContable = await _context.AsientoContables
                .Include(a => a.Cliente)
                .Include(a => a.Cuenta)
                .FirstOrDefaultAsync(m => m.IdAsiento == id);
            if (asientoContable == null)
            {
                return NotFound();
            }

            return View(asientoContable);
        }

        // GET: AsientoContables/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id");
            ViewData["CuentaId"] = new SelectList(_context.Cuenta, "Id", "Id");
            return View();
        }

        // POST: AsientoContables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAsiento,Descripcion,ClienteId,CuentaId,Fecha,Monto,TipoMovimiento,Activo")] AsientoContable asientoContable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asientoContable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", asientoContable.ClienteId);
            ViewData["CuentaId"] = new SelectList(_context.Cuenta, "Id", "Id", asientoContable.CuentaId);
            return View(asientoContable);
        }

        // GET: AsientoContables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AsientoContables == null)
            {
                return NotFound();
            }

            var asientoContable = await _context.AsientoContables.FindAsync(id);
            if (asientoContable == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", asientoContable.ClienteId);
            ViewData["CuentaId"] = new SelectList(_context.Cuenta, "Id", "Id", asientoContable.CuentaId);
            return View(asientoContable);
        }

        // POST: AsientoContables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAsiento,Descripcion,ClienteId,CuentaId,Fecha,Monto,TipoMovimiento,Activo")] AsientoContable asientoContable)
        {
            if (id != asientoContable.IdAsiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asientoContable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsientoContableExists(asientoContable.IdAsiento))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", asientoContable.ClienteId);
            ViewData["CuentaId"] = new SelectList(_context.Cuenta, "Id", "Id", asientoContable.CuentaId);
            return View(asientoContable);
        }

        // GET: AsientoContables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AsientoContables == null)
            {
                return NotFound();
            }

            var asientoContable = await _context.AsientoContables
                .Include(a => a.Cliente)
                .Include(a => a.Cuenta)
                .FirstOrDefaultAsync(m => m.IdAsiento == id);
            if (asientoContable == null)
            {
                return NotFound();
            }

            return View(asientoContable);
        }

        // POST: AsientoContables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AsientoContables == null)
            {
                return Problem("Entity set 'facturacioncompletaiso810Context.AsientoContables'  is null.");
            }
            var asientoContable = await _context.AsientoContables.FindAsync(id);
            if (asientoContable != null)
            {
                _context.AsientoContables.Remove(asientoContable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsientoContableExists(int id)
        {
          return (_context.AsientoContables?.Any(e => e.IdAsiento == id)).GetValueOrDefault();
        }
    }
}
