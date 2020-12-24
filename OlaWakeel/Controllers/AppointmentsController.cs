using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OlaWakeel.Data;
using OlaWakeel.Data.ApplicationUser;
using OlaWakeel.Models;

namespace OlaWakeel.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private UserManager<AppUser> userManager;

        public AppointmentsController(ApplicationDbContext context)
        {
            
            _context = context;
            _userManager = userManager;
        }

        public IActionResult IndexDashboard()
        {
           // var a = _context.Customers.Where(a => !_context.Appointments.Any(x => x.CustomerId == a.CustomerId)).ToList();
            var a = _context.Appointments.ToList();
            return View(a);
            // return View();
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

        public async Task<IActionResult> TrackById(int? AppoinmentId)
        {
            if (AppoinmentId == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointments
                .Include(a => a.CaseCategory)
                .Include(a => a.Customer)
                .Include(a => a.Lawyer)
                .Include(a => a.LawyerAddress)
                .FirstOrDefaultAsync(m => m.AppoinmentId == AppoinmentId);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }
        public async Task<IActionResult> Track()
        {
            //if (AppoinmentId == null)
            //{
            //    return NotFound();
            //}
            //var appointment = await _context.Appointments.FirstOrDefaultAsync(m => m.AppoinmentId == AppoinmentId);
            //if (appointment == null)
            //{
            //    return NotFound();
            //}
         //   return View(appointment);
            return View();
        }

        public async Task<IActionResult> TotalNewAppointment()
        {
            var applicationDbContext = _context.Appointments
                .Include(a => a.CaseCategory)
                .Include(a => a.Customer)
                .Include(a => a.Lawyer)
                .Include(a => a.LawyerAddress);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> TrackAppointment(string searchapp)
        {
            ViewData["getAppointmentDetails"] = searchapp;
            var searchap = from x in _context.Appointments select x;
            if (!string.IsNullOrEmpty(searchapp))
            {
                searchap = _context.Appointments
            .Include(a => a.CaseCategory)
            .Include(a => a.Customer)
            .Include(a => a.Lawyer)
            .Include(a => a.LawyerAddress);
                searchap = searchap.Where(x => x.AppointmentCode.Contains(searchapp));
                return View(await searchap.AsNoTracking().ToListAsync());
            }
            var applicationDbContext = _context.Appointments
                .Include(a => a.CaseCategory)
                .Include(a => a.Customer)
                .Include(a => a.Lawyer)
                .Include(a => a.LawyerAddress);
            return View(await applicationDbContext.ToListAsync());
        }

        //public async Task<IActionResult> TrackAppointment(string searchapp)
        //{
        //    var applicationDbContext = _context.Appointments
        //    .Include(a => a.CaseCategory)
        //    .Include(a => a.Customer)
        //    .Include(a => a.Lawyer)
        //    .Include(a => a.LawyerAddress);
        //    return View(await applicationDbContext.ToListAsync());
        //}


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
        //Pending appointments
        public async Task<IActionResult> Pending()
        {
            try

            {
               // var j = 0;
               var a = _context.Customers.Where(a => !_context.Appointments.Any(x => x.CustomerId == a.CustomerId)).ToList();
              // var b= a.Count();
                return View(a);
                
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }
        public async Task<IActionResult> Cancel()
        {
            try

            {
                var a = _context.Appointments.Where(a => a.AppoinmentStatus == "Cancel")
                .Include(a => a.Customer)
                .Include(a=>a.Lawyer);
                return View(a);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }
        public async Task<IActionResult> TodayPending()
        {
            var aA = _context.Appointments
               .Include(a => a.CaseCategory)
               .Include(a => a.Customer)
               .Include(a => a.Lawyer)
               .Include(a => a.LawyerAddress)
               .Where(w => w.ScheduleDate.Date >= DateTime.Today.Date && w.ScheduleDate.Date < DateTime.Today.AddDays(1).Date);

            return View(await aA.ToListAsync());
        }
        public async Task<IActionResult> WeeklyPending()
        {
            var aA = _context.Appointments
              .Include(a => a.CaseCategory)
              .Include(a => a.Customer)
              .Include(a => a.Lawyer)
              .Include(a => a.LawyerAddress)
              .Where(w => w.ScheduleDate.Date >= DateTime.Today.Date && w.ScheduleDate.Date < DateTime.Today.AddDays(7).Date);
            return View(await aA.ToListAsync());
        }
        public async Task<IActionResult> MonthlyPending()
        {
            var aA = _context.Appointments
             .Include(a => a.CaseCategory)
             .Include(a => a.Customer)
             .Include(a => a.Lawyer)
             .Include(a => a.LawyerAddress)
             .Where(w => w.ScheduleDate.Date >= DateTime.Today.Date && w.ScheduleDate.Date < DateTime.Today.AddMonths(1).Date);

            return View(await aA.ToListAsync());
            //try
            //{
            //    var a = _context.Customers.Where(a => !_context.Appointments.Any(x => x.CustomerId == a.CustomerId)).ToList();
            //    return View(a);
            //}
            //catch (Exception ex)
            //{
            //    return Json("Invalid Data");
            //}
        }
        public async Task<IActionResult> YearlyPending()
        {
            var aA = _context.Appointments
              .Include(a => a.CaseCategory)
              .Include(a => a.Customer)
              .Include(a => a.Lawyer)
              .Include(a => a.LawyerAddress)
              .Where(w => w.ScheduleDate.Date >= DateTime.Today.Date && w.ScheduleDate.Date < DateTime.Today.AddYears(1).Date);

            return View(await aA.ToListAsync());
        }
        public async Task<IActionResult> TodayConfirmAppointments()
        {
            try
            {
                var aA = _context.Appointments
              .Include(a => a.CaseCategory)
              .Include(a => a.Customer)
              .Include(a => a.Lawyer)
              .Include(a => a.LawyerAddress)
              .Where(w => (w.ScheduleDate.Date >= DateTime.Today.Date && w.ScheduleDate.Date < DateTime.Today.AddDays(1).Date) && w.AppoinmentStatus == "Confirmed");
                return View(await aA.ToListAsync());
                //var a = _context.Appointments.Include(a => a.Customer).Where(a => _context.Appointments.Any(x => x.CustomerId == a.CustomerId)).ToList();
                //return View(a);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }
        public async Task<IActionResult> WeeklyConfirmAppointments()
        {
            try
            {
                var aA = _context.Appointments
              .Include(a => a.CaseCategory)
              .Include(a => a.Customer)
              .Include(a => a.Lawyer)
              .Include(a => a.LawyerAddress)
              .Where(w => (w.ScheduleDate.Date >= DateTime.Today.Date && w.ScheduleDate.Date < DateTime.Today.AddDays(7).Date) && w.AppoinmentStatus == "Confirmed");
                return View(await aA.ToListAsync());
                //var a = _context.Appointments.Include(a => a.Customer).Where(a => _context.Appointments.Any(x => x.CustomerId == a.CustomerId)).ToList();
                //return View(a);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }
        public async Task<IActionResult> MonthlyConfirmAppointments()
        {
            try
            {

                var aA = _context.Appointments
              .Include(a => a.CaseCategory)
              .Include(a => a.Customer)
              .Include(a => a.Lawyer)
              .Include(a => a.LawyerAddress)
              .Where(w => (w.ScheduleDate.Date >= DateTime.Today.Date && w.ScheduleDate.Date < DateTime.Today.AddMonths(1).Date) && w.AppoinmentStatus == "Confirmed");
                return View(await aA.ToListAsync());
                //var a = _context.Appointments.Include(a => a.Customer).Where(a => _context.Appointments.Any(x => x.CustomerId == a.CustomerId)).ToList();
                //return View(a);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }
        public async Task<IActionResult> YearlyConfirmAppointments()
        {
            try
            {

                var aA = _context.Appointments
              .Include(a => a.CaseCategory)
              .Include(a => a.Customer)
              .Include(a => a.Lawyer)
              .Include(a => a.LawyerAddress)
              .Where(w => (w.ScheduleDate.Date >= DateTime.Today.Date && w.ScheduleDate.Date < DateTime.Today.AddYears(1).Date) && w.AppoinmentStatus == "Confirmed");
                return View(await aA.ToListAsync());
                //var a = _context.Appointments.Include(a => a.Customer).Where(a => _context.Appointments.Any(x => x.CustomerId == a.CustomerId)).ToList();
                //return View(a);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
            //try
            //{
            //    var a = _context.Customers.Where(a => _context.Appointments.Any(x => x.CustomerId == a.CustomerId)).ToList();
            //    return View(a);
            //}
            //catch (Exception ex)
            //{
            //    return Json("Invalid Data");
            //}
        }
        //REscheduled Appointment
        public ActionResult Rescheduled(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var reappoint = _context.RescheduleAppoints.Find(Id);
            var appoint = _context.Appointments.Find(Id);
            appoint.AppoinmentStatus = "Confirmed";
            appoint.CaseCharges = reappoint.CaseCharges;
            appoint.Date = DateTime.Now;
            appoint.ScheduleDate = reappoint.RescheduleDate;
            appoint.TimeFrom = reappoint.TimeFrom;
            appoint.TimeTo = reappoint.TimeTo;

            _context.Appointments.Update(appoint);
            _context.SaveChanges();
            return View(appoint);

        }
        //Today Reschedule Appointments
        public ActionResult TodayRescheduleAppointments(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var reappoint = _context.RescheduleAppoints.Find(Id);
            var appoint = _context.Appointments.Find(Id);
            appoint.AppoinmentStatus = "Confirmed";
            appoint.CaseCharges = reappoint.CaseCharges;
            appoint.Date = DateTime.Now;
            appoint.ScheduleDate = reappoint.RescheduleDate;
            appoint.TimeFrom = reappoint.TimeFrom;
            appoint.TimeTo = reappoint.TimeTo;

            _context.Appointments.Update(appoint);
            _context.SaveChanges();
            return View(appoint);

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

        //public IActionResult CustomerWithoutAppoint()
        //{
        //    var a = _context.Customers.Where(a => !_context.Appointments.Any(x =>x.CustomerId==a.CustomerId)).ToList();

        //        return View();
        //}

        public async Task<IActionResult> CustomerWithoutAppoint()
        {
            try
            {
                var a =  _context.Customers.Where(a => !_context.Appointments.Any(x => x.CustomerId == a.CustomerId)).ToList();
              
                //return Json(a);
                return View(a);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }
        public async Task<IActionResult> CustomerWithAppoint()
        {
            try
            {
                var a = _context.Customers.Where(a => _context.Appointments.Any(x => x.CustomerId == a.CustomerId)).ToList();

                //return Json(a);
                return View(a);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
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


        //public async Task<IActionResult> Delete(int Id)
        //{
        //    string userId = Id.ToString();

        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null)
        //    {
        //        ViewBag.ErrorMessage = $"User with Id =  { userId } can not be found";
        //        return View("Not Found");
        //    }
        //    else
        //    {
        //        var result = await _userManager.DeleteAsync(user);

        //        if (result.Succeeded)
        //        {
        //            RedirectToAction("Index");
        //        }

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }

        //        return RedirectToAction("Index");
        //    }
        //}


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


        //public async Task<JsonResult> RescheduleAppointt(IFormCollection form)
        //{

        //    try
        //    {
                
        //        var appointmentData = JsonConvert.DeserializeObject<Appointment>(Request.Form["Appointment"]);
        //       // var user = JsonConvert.DeserializeObject<obj4>(Request.Form["user"]);
        //        var appoint = _context.Appointments.Find(appointmentData.AppoinmentId);
        //        appoint.AppoinmentStatus = appoint.AppoinmentStatus;
        //        if (appointmentData.LawyerAddressId == null)
        //            appoint.LawyerAddress = null;
        //        appoint.LawyerAddressId = appointmentData.LawyerAddressId;
        //        appoint.ScheduleDate = appointmentData.ScheduleDate;
        //        appoint.TimeFrom = appointmentData.TimeFrom;
        //        appoint.TimeTo = appointmentData.TimeTo;
        //        appoint.AppoinmentType = appointmentData.AppoinmentType;
        //        appoint.CaseCharges = appointmentData.CaseCharges;
        //        appoint.AppoinmentStatus = "Confirmed";
        //        _context.Appointments.Update(appointmentData);
        //        await _context.SaveChangesAsync();
              
        //      //  return Json(appoint);
        //        return View(appointmentData);
        //    }
        //    catch (Exception ex)
        //    {

        //        return Json("Invalid Data");
        //    }

        //}



    }
}
