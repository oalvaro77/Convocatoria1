using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionVehiculos.Data;
using GestionVehiculos.Models;

namespace GestionVehiculos.Controllers
{
    public class SegurosController : Controller
    {
        private readonly DbContextApplication _context;

        public SegurosController(DbContextApplication context)
        {
            _context = context;
        }

        // GET: Seguros
        public async Task<IActionResult> Index()
        {
            var dbContextApplication = _context.Seguros.Include(s => s.vehiculo);
            return View(await dbContextApplication.ToListAsync());
        }

        // GET: Seguros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguro = await _context.Seguros
                .Include(s => s.vehiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seguro == null)
            {
                return NotFound();
            }

            return View(seguro);
        }

        // GET: Seguros/Create
        public IActionResult Create()
        {
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Placa");
            return View();
        }

        // POST: Seguros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cobertertura,Proveedor,FechaInicio,FechaVencimiento,Costo,VehiculoId")] Seguro seguro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seguro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Placa", seguro.VehiculoId);
            return View(seguro);
        }

        // GET: Seguros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguro = await _context.Seguros.FindAsync(id);
            if (seguro == null)
            {
                return NotFound();
            }
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Placa", seguro.VehiculoId);
            return View(seguro);
        }

        // POST: Seguros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cobertertura,Proveedor,FechaInicio,FechaVencimiento,Costo,VehiculoId")] Seguro seguro)
        {
            if (id != seguro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seguro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeguroExists(seguro.Id))
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
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Placa", seguro.VehiculoId);
            return View(seguro);
        }

        // GET: Seguros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguro = await _context.Seguros
                .Include(s => s.vehiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seguro == null)
            {
                return NotFound();
            }

            return View(seguro);
        }

        // POST: Seguros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seguro = await _context.Seguros.FindAsync(id);
            if (seguro != null)
            {
                _context.Seguros.Remove(seguro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeguroExists(int id)
        {
            return _context.Seguros.Any(e => e.Id == id);
        }
    }
}
