using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final.Models;
using Final.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Final.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Profile/Details/5
        public async Task<IActionResult> Details()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            var profile = await _context.Profiles
                .SingleOrDefaultAsync(m => m.Id == currentUser.ProfileId);


            return View(profile);
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(ProfileSearchViewModel vm)
        {
            List<ProfileSearchResultViewModel> result
                                    = new List<ProfileSearchResultViewModel>();
            if (ModelState.IsValid)
            {
                result = (from p in _context.Profiles
                          where p.Email == vm.Email
                          select new ProfileSearchResultViewModel
                          {
                              Email = p.Email,
                              TotalLimit = p.TotalLimit,
                              LimitLeft = p.LimitLeft,
                              SystemStatus = p.SystemStatus,
                              PeriodFrom = p.PeriodFrom,
                              PeriodTill = p.PeriodTill

                          }).ToList();
            }
            return View("Result", result);
        }
        //Create Profile
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Email,TotalLimit,LimitLeft,SystemStatus,PeriodFrom,PeriodTill")] Profile profile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(profile);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(profile);
        //}

        // GET: Profile/Edit/5
        public async Task<IActionResult> Edit()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.Id == currentUser.ProfileId);
            return View(profile);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Email,TotalLimit,LimitLeft,SystemStatus,PeriodFrom,PeriodTill")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!ProfileExists(profile.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        //GET: Profile/Delete/5
        //public async Task<IActionResult> Delete()
        //{
        //    ApplicationUser currentUser = await _userManager.GetUserAsync(User);
        //    var profile = await _context.Profiles
        //        .SingleOrDefaultAsync(m => m.Id == currentUser.ProfileId);
        //    return View(profile);
        //}

        //// POST: Profile/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed()
        //{
        //    ApplicationUser currentUser = await _userManager.GetUserAsync(User);
        //    var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.Id == currentUser.ProfileId);
        //    _context.Profiles.Remove(profile);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}
