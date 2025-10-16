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
    public class mhkThucDonsController : Controller
    {
        private readonly MhkcafeContext _context;

        public mhkThucDonsController(MhkcafeContext context)
        {
            _context = context;
        }

        // GET: mhkThucDons
        public async Task<IActionResult> Index()
        {
            return View(await _context.ThucDons.ToListAsync());
        }

        // GET: mhkThucDons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thucDon = await _context.ThucDons
                .FirstOrDefaultAsync(m => m.ThucDonId == id);
            if (thucDon == null)
            {
                return NotFound();
            }

            return View(thucDon);
        }

        // GET: mhkThucDons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: mhkThucDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThucDonId,TenMon,MoTa,Gia,HinhAnh,TrangThai")] ThucDon thucDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thucDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thucDon);
        }

        // GET: mhkThucDons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thucDon = await _context.ThucDons.FindAsync(id);
            if (thucDon == null)
            {
                return NotFound();
            }
            return View(thucDon);
        }

        // POST: mhkThucDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThucDonId,TenMon,MoTa,Gia,HinhAnh,TrangThai")] ThucDon thucDon)
        {
            if (id != thucDon.ThucDonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thucDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThucDonExists(thucDon.ThucDonId))
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
            return View(thucDon);
        }

        // GET: mhkThucDons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thucDon = await _context.ThucDons
                .FirstOrDefaultAsync(m => m.ThucDonId == id);
            if (thucDon == null)
            {
                return NotFound();
            }

            return View(thucDon);
        }

        // POST: mhkThucDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Xóa các chi tiết hóa đơn liên quan trước
            var chiTietList = _context.ChiTietHoaDons
                                      .Where(ct => ct.ThucDonId == id);

            _context.ChiTietHoaDons.RemoveRange(chiTietList);

            // Xóa món trong ThucDon
            var thucDon = await _context.ThucDons.FindAsync(id);
            if (thucDon != null)
            {
                _context.ThucDons.Remove(thucDon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ForCustomer()
        {
            var list = _context.ThucDons.Where(td => td.TrangThai == true).ToList();
            return View(list);
        }



        private bool ThucDonExists(int id)
        {
            return _context.ThucDons.Any(e => e.ThucDonId == id);
        }
    }
}
