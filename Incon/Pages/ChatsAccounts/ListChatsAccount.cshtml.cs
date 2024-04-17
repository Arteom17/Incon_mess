using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Incon.Models;

namespace Incon.Pages.ChatsAccounts
{
    public class ListChatsAccountModel : PageModel
    {
        private readonly Incon.Models.InconContext _context;



        public ListChatsAccountModel(Incon.Models.InconContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get;set; } = default!;

        public async Task OnGetAsync(int id)
        {
           
            if (_context != null)
            {
                try
                {
                    var ca = (from s in _context.Chatsaccounts where s.Chatidref == id select s).ToList();

                    Account = new List<Account>();
                    foreach (var c in ca)
                    {
                        try
                        {
                            var acc = _context.Accounts.FirstOrDefault(m => m.Accountid == c.Accountidref);
                            Account.Add(acc);
                        }
                        catch { }


                    }
                }
                catch { }
            }
        }
    }
}
