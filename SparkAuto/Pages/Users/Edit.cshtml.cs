using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Models;
using SparkAuto.Utility;

namespace SparkAuto.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class EditModel : PageModel
    {
        private readonly SparkAuto.Data.ApplicationDbContext _db;

        public EditModel(SparkAuto.Data.ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }
        public async Task<IActionResult> OnGetAsync(string guid)
        {
            if(guid == null)
            {
                return NotFound();
            }

            ApplicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == guid);
            if(ApplicationUser==null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userFromDb = await _db.ApplicationUser.FirstOrDefaultAsync(s => s.Id == ApplicationUser.Id);
            userFromDb.Name = ApplicationUser.Name;
            userFromDb.PhoneNumber = ApplicationUser.PhoneNumber;
            userFromDb.Address = ApplicationUser.Address;
            userFromDb.City = ApplicationUser.City;
            userFromDb.PostalCode = ApplicationUser.PostalCode;

            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}