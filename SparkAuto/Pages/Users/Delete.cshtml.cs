﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Models;

namespace SparkAuto.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly SparkAuto.Data.ApplicationDbContext _db;

        public DeleteModel(SparkAuto.Data.ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == id);

            if (ApplicationUser == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser = await _db.ApplicationUser.FindAsync(id);

            if (ApplicationUser != null)
            {
                _db.ApplicationUser.Remove(ApplicationUser);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}