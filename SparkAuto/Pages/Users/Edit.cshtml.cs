using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Models;

namespace SparkAuto.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly SparkAuto.Data.ApplicationDbContext _db;

        public EditModel(SparkAuto.Data.ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ApplicationUser AppUser { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            AppUser = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == id.ToString());
            if(AppUser==null)
            {
                return NotFound();
            }
            return Page();
        }

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    ServiceType = await _db.ServiceType.FirstOrDefaultAsync(m => m.Id == id);

        //    if (ServiceType == null)
        //    {
        //        return NotFound();
        //    }
        //    return Page();
        //}
    }
}