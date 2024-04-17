using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Incon.Models;

namespace Incon.Pages.RegPages
{
    public class EditAccountModel : PageModel
    {
        private readonly Incon.Models.InconContext _context;

        public EditAccountModel(Incon.Models.InconContext context)
        {
            _context = context;
        }
        public static string BMessage { get; set; }

        [BindProperty]
        public Account Account { get; set; } = default!;
        private static int Id { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }
            Pages.GlobalSettings.Accdel = id;

            var account =  await _context.Accounts.FirstOrDefaultAsync(m => m.Accountid == id);
            if (account == null)
            {
                return NotFound();
            }
            Account = account;
            Id = account.Accountid;
           ViewData["Profileidref"] = new SelectList(_context.Profiles, "Profileid", "Profileid");
            try
            {
                Account.Dateof = account.Dateofbirthday.Value.ToDateTime(new TimeOnly(00, 00, 0));
            }
            catch { }
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (Account.Dateof == null)
            //{
            //    Account.Dateof = new DateTime(01, 01, 01, 00, 00, 0);
            //    if (!ModelState.IsValid)
            //    {
            //        return Page();
            //    }
            //    Account.Dateof = null;
            //}
            //else
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return Page();
            //    }

            //}
            Account.Accountid = Id;
            Account.Profileidref = Pages.GlobalSettings.Globalprofile.Profileid;
            if (Account.Dateof != null)
            {
                try
                {
                    Account.Dateofbirthday = DateOnly.FromDateTime(Account.Dateof.Value);
                }
                catch { }
            }

            _context.Attach(Account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                BMessage = "Ошибка ввода, попробуйте еще раз";
                return Page();
            }
            
            Pages.GlobalSettings.Accdel = null;
            return RedirectToPage("/LoginPages/ChooseAccount");
        }

        private bool AccountExists(int id)
        {
          return (_context.Accounts?.Any(e => e.Accountid == id)).GetValueOrDefault();
        }
    }
}
