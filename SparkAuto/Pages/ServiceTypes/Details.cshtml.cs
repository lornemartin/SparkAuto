﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;

namespace SparkAuto.Pages.ServiceTypes
{
    public class DetailsModel : PageModel
    {
        private readonly SparkAuto.Data.ApplicationDbContext _context;

        public DetailsModel(SparkAuto.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ServiceType ServiceType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceType = await _context.ServiceType.FirstOrDefaultAsync(m => m.Id == id);

            if (ServiceType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
