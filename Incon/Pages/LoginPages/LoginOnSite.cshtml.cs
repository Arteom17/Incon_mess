using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Incon.Models;
using System.Text;
using System.Security.Cryptography;
using NuGet.Packaging.Signing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Incon.Pages.LoginPage
{
    public class LoginOnSiteModel : PageModel
    {
        private readonly Incon.Models.InconContext _context;

        public string BMessage { get; set; } = "";

        public LoginOnSiteModel(Incon.Models.InconContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Profile Profile { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (_context.Profiles == null || Profile == null)
            {
                return Page();
            }
            try
            {
                var hashbytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Profile.Passwordhash));
                Profile.Passwordhash = BitConverter.ToString(hashbytes).Replace("-", "").ToLower();
                IQueryable<Profile> profiq = from s in _context.Profiles where s.Login == Profile.Login && s.Passwordhash == Profile.Passwordhash select s;
                Pages.GlobalSettings.Globalprofile = profiq.ToList()[0];
            }
            catch(Exception ex) 
            {
                BMessage = "Не корректый логин или пароль, попробуйте еще раз";
                return Page();
            }
            return RedirectToPage("/LoginPages/ChooseAccount");
        }
    }
}
