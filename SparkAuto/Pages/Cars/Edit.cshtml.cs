using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
//using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;

namespace SparkAuto.Pages.Cars
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string StatusMessage { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Car Car { get; set; }

        [BindProperty]
        public ApplicationUser AppUser { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }

            Car = await _db.Car.FirstOrDefaultAsync(c => c.Id == id);
            
            if(Car==null)
            {
                return NotFound();
            }
            else
            {
                
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var carFromDb = await _db.Car.FirstOrDefaultAsync(c => c.Id == Car.Id);
            carFromDb.VIN = Car.VIN;
            carFromDb.Make = Car.Make;
            carFromDb.Model = Car.Model;
            carFromDb.Style = Car.Style;
            carFromDb.Year = Car.Year;
            carFromDb.Miles = Car.Miles;
            carFromDb.Color = Car.Color;

            await _db.SaveChangesAsync();

            StatusMessage = "Car Updated Successfully";
            return RedirectToPage("Index", new { userId = Car.UserId } );
        }


    }
}