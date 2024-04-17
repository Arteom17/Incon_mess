using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Incon.Models;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Incon.Pages.LoginPage
{
    public class ChooseAccountModel : PageModel
    {
        private readonly Incon.Models.InconContext _context;

        public ChooseAccountModel(Incon.Models.InconContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Pages.GlobalSettings.Accdel != null) Pages.GlobalSettings.Accdel = null;
            if (Pages.GlobalSettings.Globalprofile != null)
            {
                Pages.GlobalSettings.Globalaccount = null;

                IQueryable<Account> accountiq = from s in _context.Accounts where s.Profileidref == Pages.GlobalSettings.Globalprofile.Profileid select s;

                accountiq = accountiq.OrderBy(a => a.Accountid);
                try { Account = accountiq.ToList(); }

                catch (Exception ex)
                {
                    return RedirectToPage("/Index");
                }
                if (Account.Count() == 0)
                    return RedirectToPage("/RegPages/CreateAccount");
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
        public async Task<IActionResult> OnGetChoose(int id)
        {
            Account account = (from s in _context.Accounts where s.Accountid == id select s).ToList()[0];
            Pages.GlobalSettings.Logcheck = false;
            if (account.Accountid==id)
                {
                    Pages.GlobalSettings.Globalaccount=account;
                    return RedirectToPage("/index");
                }
             
            return Page();
        }
    }
}
