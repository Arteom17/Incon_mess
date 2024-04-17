using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Incon.Models;
using System.Text;
using System.Security.Cryptography;

namespace Incon.Pages.RegPages
{
    public class EditProfileModel : PageModel
    {
        private readonly Incon.Models.InconContext _context;

        public EditProfileModel(Incon.Models.InconContext context)
        {
            _context = context;
        }
        public static string BMessage { get; set; }
        [BindProperty]
        public Profile Profile { get; set; } = default!;
        private static string hash { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Profiles == null)
            {
                return NotFound();
            }

            var profile =  await _context.Profiles.FirstOrDefaultAsync(m => m.Profileid == id);
            if (profile == null)
            {
                return NotFound();
            }
            Profile = profile;
            Profile.Dateof = profile.Dateofbirthday.ToDateTime(new TimeOnly(00, 00, 0));
            hash = profile.Passwordhash;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            Profile.Profileid = Pages.GlobalSettings.Globalprofile.Profileid;
            if (Profile.Pass != null && Profile.Pass!="")
            {
            var hashbytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Profile.Passwordhash));
            Profile.Passwordhash = BitConverter.ToString(hashbytes).Replace("-", "").ToLower();
            }else
            Profile.Passwordhash = hash;
            Profile.Dateofbirthday = DateOnly.FromDateTime(Profile.Dateof);
            _context.Attach(Profile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                BMessage = "Ошибка ввода, попробуйте еще раз";
                return Page();
            }

            return RedirectToPage("/Index");
        }

        private bool ProfileExists(int id)
        {
          return (_context.Profiles?.Any(e => e.Profileid == id)).GetValueOrDefault();
        }
    }
}
