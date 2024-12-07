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
    public class FacturasController : Controller
    {
        private readonly facturacioncompletaiso810Context _context;

        public FacturasController(facturacioncompletaiso810Context context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            var facturacioncompletaiso810Context = _context.Facturas.Include(f => f.Articulo).Include(f => f.Cliente).Include(f => f.Vendedor);
            return View(await facturacioncompletaiso810Context.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Articulo)
                .Include(f => f.Cliente)
                .Include(f => f.Vendedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Id");
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id");
            ViewData["VendedorId"] = new SelectList(_context.Vendedors, "Id", "Id");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,DescripcionArticulo,Cantidad,Total,Comentario,ClienteId,VendedorId,ArticuloId")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Id", factura.ArticuloId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", factura.ClienteId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedors, "Id", "Id", factura.VendedorId);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Id", factura.ArticuloId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", factura.ClienteId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedors, "Id", "Id", factura.VendedorId);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,DescripcionArticulo,Cantidad,Total,Comentario,ClienteId,VendedorId,ArticuloId")] Factura factura)
        {
            if (id != factura.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.Id))
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
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "Id", "Id", factura.ArticuloId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", factura.ClienteId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedors, "Id", "Id", factura.VendedorId);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.Articulo)
                .Include(f => f.Cliente)
                .Include(f => f.Vendedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Facturas == null)
            {
                return Problem("Entity set 'facturacioncompletaiso810Context.Facturas'  is null.");
            }
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
          return (_context.Facturas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
