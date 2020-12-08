using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OlaWakeel.Data;
using OlaWakeel.Models;

namespace OlaWakeel.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult IndexDashboard()
        {
            return View();
        }
        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Appointments
                .Include(a => a.CaseCategory)
                .Include(a => a.Customer)
                .Include(a => a.Lawyer)
                .Include(a => a.LawyerAddress);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Active()
        {
            //var pT = _context.Appointments.Where(w => w.Date == DateTime.Today).FirstOrDefault();
            //return View(pT.ToList());
            var aA = _context.Appointments
                .Include(a => a.CaseCategory)
                .Include(a => a.Customer)
                .Include(a => a.Lawyer)
                .Include(a => a.LawyerAddress)
                .Where(w => w.Date == DateTime.Today);
            //if (aA == null)
            //{
            //    return NotFound();
            //}

            return View(await aA.ToListAsync());
        }
        public async Task<IActionResult> Pending()
        {
            var pA = _context.Appointments
                .Include(a => a.CaseCategory)
                .Include(a => a.Customer)
                .Include(a => a.Lawyer)
                .Include(a => a.LawyerAddress)
                .Where(d => d.Date > DateTime.Today);
            return View(await pA.ToListAsync());
        }
        public async Task<IActionResult> Rescheduled()
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            var rA = _context.Appointments
                .Include(a => a.CaseCategory)
                .Include(a => a.Customer)
                .Include(a => a.Lawyer)
                .Include(a => a.LawyerAddress)
                .Where(w => w.Date == DateTime.Today);
            //if (aA == null)
            //{
            //    return NotFound();
            //}

            return View(await rA.ToListAsync());
        }
        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.CaseCategory)
                .Include(a => a.Customer)
                .Include(a => a.Lawyer)
                .Include(a => a.LawyerAddress)
                .FirstOrDefaultAsync(m => m.AppoinmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            //ViewData["CaseCategoryId"] = new SelectList(_context.CaseCategories, "CaseCategoryId", "CaseCategoryId");
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            //ViewData["LawyerId"] = new SelectList(_context.Lawyers, "LawyerId", "LawyerId");
            //ViewData["LawyerAddressId"] = new SelectList(_context.LawyerAddresses, "LawyerAddressId", "LawyerAddressId");
         //   return View();

            ViewData["CaseCategoryId"] = new SelectList(_context.CaseCategories, "CaseCategoryId", "CaseCategoryId");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["LawyerId"] = new SelectList(_context.Lawyers, "LawyerId", "LawyerId");
            ViewData["LawyerAddressId"] = new SelectList(_context.LawyerAddresses, "LawyerAddressId", "LawyerAddressId");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppoinmentId,AppointmentCode,LawyerAddressId,CaseCategoryId,TimeFrom,TimeTo,CaseCharges,ScheduleDate,Rating,AppoinmentType,AppoinmentStatus,Date,CustomerId,LawyerId")] Appointment appointment)
            {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaseCategoryId"] = new SelectList(_context.CaseCategories, "CaseCategoryId", "CaseCategoryId", appointment.CaseCategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", appointment.CustomerId);
            ViewData["LawyerId"] = new SelectList(_context.Lawyers, "LawyerId", "LawyerId", appointment.LawyerId);
            ViewData["LawyerAddressId"] = new SelectList(_context.LawyerAddresses, "LawyerAddressId", "LawyerAddressId", appointment.LawyerAddressId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["CaseCategoryId"] = new SelectList(_context.CaseCategories, "CaseCategoryId", "CaseCategoryId", appointment.CaseCategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", appointment.CustomerId);
            ViewData["LawyerId"] = new SelectList(_context.Lawyers, "LawyerId", "LawyerId", appointment.LawyerId);
            ViewData["LawyerAddressId"] = new SelectList(_context.LawyerAddresses, "LawyerAddressId", "LawyerAddressId", appointment.LawyerAddressId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppoinmentId,AppointmentCode,LawyerAddressId,CaseCategoryId,TimeFrom,TimeTo,CaseCharges,ScheduleDate,Rating,AppoinmentType,AppoinmentStatus,Date,CustomerId,LawyerId")] Appointment appointment)
        {
            if (id != appointment.AppoinmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppoinmentId))
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
            ViewData["CaseCategoryId"] = new SelectList(_context.CaseCategories, "CaseCategoryId", "CaseCategoryId", appointment.CaseCategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", appointment.CustomerId);
            ViewData["LawyerId"] = new SelectList(_context.Lawyers, "LawyerId", "LawyerId", appointment.LawyerId);
            ViewData["LawyerAddressId"] = new SelectList(_context.LawyerAddresses, "LawyerAddressId", "LawyerAddressId", appointment.LawyerAddressId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.CaseCategory)
                .Include(a => a.Customer)
                .Include(a => a.Lawyer)
                .Include(a => a.LawyerAddress)
                .FirstOrDefaultAsync(m => m.AppoinmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _context.Delete(id);
        //    return RedirectToAction("Index");
        //}

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppoinmentId == id);
        }
        
    }
}
