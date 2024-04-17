using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Incon.Models;

namespace Incon.Pages.RegPages
{
    public class DeleteProfileModel : PageModel
    {
        private readonly Incon.Models.InconContext _context;

        public DeleteProfileModel(Incon.Models.InconContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Profile Profile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            id = Pages.GlobalSettings.Globalprofile.Profileid;
            if (id == null || _context.Profiles == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles.FirstOrDefaultAsync(m => m.Profileid == id);

            if (profile == null)
            {
                return NotFound();
            }
            else 
            {
                Profile = profile;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            id = Pages.GlobalSettings.Globalprofile.Profileid;
            if (id == null || _context.Profiles == null)
            {
                return NotFound();
            }
            var profile = await _context.Profiles.FindAsync(id);

            if (profile != null)
            {
                Profile = profile;
                _context.Profiles.Remove(Profile);
                await _context.SaveChangesAsync();
                Pages.GlobalSettings.Globalprofile = null;
                Pages.GlobalSettings.Globalaccount = null;
            }

            return RedirectToPage("/Index");
        }
    }
}
