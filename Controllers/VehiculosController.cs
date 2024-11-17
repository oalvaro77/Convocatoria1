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
    public class VehiculosController : Controller
    {
        private readonly DbContextApplication _context;

        public VehiculosController(DbContextApplication context)
        {
            _context = context;
        }

        // GET: Vehiculos
        public async Task<IActionResult> Index()
        {
            var dbContextApplication = _context.Vehiculos.Include(v => v.Cliente);
            return View(await dbContextApplication.ToListAsync());
        }

        // GET: Vehiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // GET: Vehiculos/Create
        public IActionResult Create()
        {
            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "Id", "Name");
            return View();
        }

        // POST: Vehiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Marca,Modelo,Placa,Tipo,Anio,ID_Cliente")] Vehiculo vehiculo, IFormFile Foto)
        {
            // Muestra la lista de clientes en el formulario
            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "Id", "Name", vehiculo.ID_Cliente);

            // Solo se valida y se guarda la foto si se ha proporcionado
            if (Foto != null)
            {
                var extension = Path.GetExtension(Foto.FileName).ToLower();

                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                {
                    ModelState.AddModelError("Foto", "Solo se permiten archivos JPG o PNG.");
                    return View(vehiculo);
                }

                // Define la ruta de almacenamiento de la imagen
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                // Si la carpeta no existe, la crea
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var filePath = Path.Combine(uploadDirectory, Foto.FileName);

                // Si el archivo ya existe, cambiar el nombre (opcional)
                if (System.IO.File.Exists(filePath))
                {
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Foto.FileName);
                    var newFileName = fileNameWithoutExtension + Guid.NewGuid().ToString() + Path.GetExtension(Foto.FileName);
                    filePath = Path.Combine(uploadDirectory, newFileName);
                }

                try
                {
                    // Guarda el archivo en el servidor
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Foto.CopyToAsync(stream);
                    }

                    // Asigna la ruta al modelo
                    vehiculo.Foto = "/uploads/" + Path.GetFileName(filePath);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Foto", "Ocurrió un error al guardar el archivo. Intenta nuevamente.");
                    return View(vehiculo);
                }
            }

            // Si el estado del modelo es válido, guarda el vehículo en la base de datos
            if (ModelState.IsValid)
            {
                _context.Add(vehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vehiculo);
        }


        // GET: Vehiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "Id", "Name", vehiculo.ID_Cliente);
            return View(vehiculo);
        }

        // POST: Vehiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Marca,Modelo,Placa,Tipo,Anio,ID_Cliente")] Vehiculo vehiculo)
        {
            if (id != vehiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculoExists(vehiculo.Id))
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
            ViewData["ID_Cliente"] = new SelectList(_context.Clientes, "Id", "Name", vehiculo.ID_Cliente);
            return View(vehiculo);
        }

        // GET: Vehiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculoExists(int id)
        {
            return _context.Vehiculos.Any(e => e.Id == id);
        }
    }
}
