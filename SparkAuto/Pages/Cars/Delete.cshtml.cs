using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;

namespace SparkAuto.Pages.Cars
{
    public class DeleteModel : PageModel
    {
        public readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public Car Car { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Car = await _db.Car.FirstOrDefaultAsync(c => c.Id == id);

            if (Car == null)
            {
                return NotFound();
            }
            else
            {

                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Car = await _db.Car.FindAsync(id);

            if (Car != null)
            {
                _db.Car.Remove(Car);
                await _db.SaveChangesAsync();
            }

            StatusMessage = "Car Deleted Successfully.";
            return RedirectToPage("Index", new { userId = Car.UserId });
        }
    }
}