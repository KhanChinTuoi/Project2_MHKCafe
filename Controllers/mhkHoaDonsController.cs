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
    public class mhkHoaDonsController : Controller
    {
        private readonly MhkcafeContext _context;

        public mhkHoaDonsController(MhkcafeContext context)
        {
            _context = context;
        }

        // GET: mhkHoaDons
        public async Task<IActionResult> Index()
        {
            var mhkcafeContext = _context.HoaDons.Include(h => h.NguoiDung);
            return View(await mhkcafeContext.ToListAsync());
        }

        // GET: mhkHoaDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.NguoiDung)
                .FirstOrDefaultAsync(m => m.HoaDonId == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // GET: mhkHoaDons/Create
        public IActionResult Create()
        {
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId");
            return View();
        }

        // POST: mhkHoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HoaDonId,NguoiDungId,TenKhachHang,SoDienThoai,DiaChiGiaoHang,NgayLap,TongTien,PhuongThucThanhToan,TrangThai")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", hoaDon.NguoiDungId);
            return View(hoaDon);
        }

        // GET: mhkHoaDons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", hoaDon.NguoiDungId);
            return View(hoaDon);
        }

        // POST: mhkHoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HoaDonId,NguoiDungId,TenKhachHang,SoDienThoai,DiaChiGiaoHang,NgayLap,TongTien,PhuongThucThanhToan,TrangThai")] HoaDon hoaDon)
        {
            if (id != hoaDon.HoaDonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.HoaDonId))
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
            ViewData["NguoiDungId"] = new SelectList(_context.NguoiDungs, "NguoiDungId", "NguoiDungId", hoaDon.NguoiDungId);
            return View(hoaDon);
        }

        // GET: mhkHoaDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDons
                .Include(h => h.NguoiDung)
                .FirstOrDefaultAsync(m => m.HoaDonId == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: mhkHoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoaDon = await _context.HoaDons.FindAsync(id);
            if (hoaDon != null)
            {
                _context.HoaDons.Remove(hoaDon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(int id)
        {
            return _context.HoaDons.Any(e => e.HoaDonId == id);
        }
    }
}
