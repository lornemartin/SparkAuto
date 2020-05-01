using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Utility;

namespace SparkAuto.Areas.Identity.Pages.Account
{
    public class VerifyEmailModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public VerifyEmailModel(ApplicationDbContext db, UserManager<IdentityUser> um, IEmailSender sdr)
        {
            _db = db;
            _userManager = um;
            _emailSender = sdr;
        }
        public IActionResult OnGet(string id)
        {
            Email = id;
            return Page();
        }

        public async Task<IActionResult> OnPost(string id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email==id);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            return RedirectToPage("/Users/Index");

        }
    }
}
