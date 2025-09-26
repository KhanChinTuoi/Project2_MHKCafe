using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MHKCafe.Models;

namespace MHKCafe.Controllers
{
    public class mhkChiTietHoaDonsController : Controller
    {
        private readonly MhkcafeContext _context;

        public mhkChiTietHoaDonsController(MhkcafeContext context)
        {
            _context = context;
        }

        // GET: mhkChiTietHoaDons
        public async Task<IActionResult> Index()
        {
            var mhkcafeContext = _context.ChiTietHoaDons.Include(c => c.HoaDon).Include(c => c.ThucDon);
            return View(await mhkcafeContext.ToListAsync());
        }

        // GET: mhkChiTietHoaDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHoaDon = await _context.ChiTietHoaDons
                .Include(c => c.HoaDon)
                .Include(c => c.ThucDon)
                .FirstOrDefaultAsync(m => m.ChiTietId == id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }

            return View(chiTietHoaDon);
        }

        // GET: mhkChiTietHoaDons/Create
        public IActionResult Create()
        {
            ViewData["HoaDonId"] = new SelectList(_context.HoaDons, "HoaDonId", "HoaDonId");
            ViewData["ThucDonId"] = new SelectList(_context.ThucDons, "ThucDonId", "ThucDonId");
            return View();
        }

        // POST: mhkChiTietHoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChiTietId,HoaDonId,ThucDonId,SoLuong,DonGia,ThanhTien")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietHoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HoaDonId"] = new SelectList(_context.HoaDons, "HoaDonId", "HoaDonId", chiTietHoaDon.HoaDonId);
            ViewData["ThucDonId"] = new SelectList(_context.ThucDons, "ThucDonId", "ThucDonId", chiTietHoaDon.ThucDonId);
            return View(chiTietHoaDon);
        }

        // GET: mhkChiTietHoaDons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHoaDon = await _context.ChiTietHoaDons.FindAsync(id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }
            ViewData["HoaDonId"] = new SelectList(_context.HoaDons, "HoaDonId", "HoaDonId", chiTietHoaDon.HoaDonId);
            ViewData["ThucDonId"] = new SelectList(_context.ThucDons, "ThucDonId", "ThucDonId", chiTietHoaDon.ThucDonId);
            return View(chiTietHoaDon);
        }

        // POST: mhkChiTietHoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChiTietId,HoaDonId,ThucDonId,SoLuong,DonGia,ThanhTien")] ChiTietHoaDon chiTietHoaDon)
        {
            if (id != chiTietHoaDon.ChiTietId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietHoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietHoaDonExists(chiTietHoaDon.ChiTietId))
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
            ViewData["HoaDonId"] = new SelectList(_context.HoaDons, "HoaDonId", "HoaDonId", chiTietHoaDon.HoaDonId);
            ViewData["ThucDonId"] = new SelectList(_context.ThucDons, "ThucDonId", "ThucDonId", chiTietHoaDon.ThucDonId);
            return View(chiTietHoaDon);
        }

        // GET: mhkChiTietHoaDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHoaDon = await _context.ChiTietHoaDons
                .Include(c => c.HoaDon)
                .Include(c => c.ThucDon)
                .FirstOrDefaultAsync(m => m.ChiTietId == id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }

            return View(chiTietHoaDon);
        }

        // POST: mhkChiTietHoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietHoaDon = await _context.ChiTietHoaDons.FindAsync(id);
            if (chiTietHoaDon != null)
            {
                _context.ChiTietHoaDons.Remove(chiTietHoaDon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietHoaDonExists(int id)
        {
            return _context.ChiTietHoaDons.Any(e => e.ChiTietId == id);
        }
    }
}
