using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Incon.Models;
using Incon.Pages;
using System.Security.Cryptography;
using System.Text;
using System.Data;

namespace Incon.Pages.RegPages
{
    public class CreateProfile : PageModel
    {
        private readonly Incon.Models.InconContext _context;
        public static string BMessage { get; set; } = "";
        public static string Pass { get; set; } = "";


        public CreateProfile(Incon.Models.InconContext context)
        {

            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Profile Profile { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Profiles == null || Profile == null)
            {
                BMessage = "Ошибка ввода, попробуйте еще раз";
                return Page();
            }
            try
            {
                var hashbytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Profile.Passwordhash));
                Profile.Passwordhash = BitConverter.ToString(hashbytes).Replace("-","").ToLower();
            Profile.Dateofbirthday = DateOnly.FromDateTime(Profile.Dateof);
                _context.Profiles.Add(Profile);
                await _context.SaveChangesAsync();
                Pages.GlobalSettings.Globalprofile = Profile;
            }
            catch (Exception)
            {
                BMessage= "Ошибка ввода, попробуйте еще раз";
                return Page();
            }

            return RedirectToPage("./CreateAccount");
            
        }
    }
}
