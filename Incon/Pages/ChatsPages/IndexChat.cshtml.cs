using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Incon.Models;
using Microsoft.IdentityModel.Tokens;

namespace Incon.Pages.ChatsPages
{
    public class IndexChat : PageModel
    {
        private readonly Incon.Models.InconContext _context;

        public IndexChat(Incon.Models.InconContext context)
        {
            _context = context;
        }

        public IList<Chat> Chat { get; set; } = default!;
        public IList<Chatsaccount> chatacc { get; set; } = default!;
        public IList<Chat> chats { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Pages.GlobalSettings.GlobalChat = null;
            if (_context.Chats != null)
            {
                Chat = new List<Chat>();
            IQueryable<Chat> Chats = from s in _context.Chats select s;
            IQueryable<Chatsaccount> chatsAccounts = from s in _context.Chatsaccounts select s;

            if (Pages.GlobalSettings.Globalaccount != null)
            {
                    chatsAccounts = chatsAccounts.Where(m => m.Accountidref == Pages.GlobalSettings.Globalaccount.Accountid);
                    try { chatacc = chatsAccounts.ToList(); }
                    catch (Exception ex) { return RedirectToPage("/ChatsPages/CreateChat"); }
                    try { chats = Chats.ToList(); }
                    catch (Exception ex) { return RedirectToPage("/ChatsPages/CreateChat"); }
                    foreach (Chat ch in Chats)
                    {
                        if(!ch.Chatsaccounts.IsNullOrEmpty())
                        {
                        foreach (Chatsaccount ca in ch.Chatsaccounts)
                        {
                            if (ca.Accountidref == Pages.GlobalSettings.Globalaccount.Accountid)
                            {
                                Chat.Add(ch);
                                break;
                            }
                        }
                        }
                    }
                if (Chat.IsNullOrEmpty())
                    return RedirectToPage("/ChatsPages/CreateChat");
                return Page();
            }
            }


            return RedirectToPage("/LoginPages/ChooseAccount");
            
        }
    }
}
