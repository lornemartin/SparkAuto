using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SparkAuto.Data;
using SparkAuto.Models;

namespace SparkAuto.Pages.ServiceTypes
{
    public class CreateModel : PageModel
    {
        private readonly SparkAuto.Data.ApplicationDbContext _db;

        [BindProperty]
        public ServiceType ServiceType { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db; 
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _db.ServiceType.Add(ServiceType);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}