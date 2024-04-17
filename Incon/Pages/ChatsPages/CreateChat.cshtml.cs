using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Incon.Models;
using Microsoft.EntityFrameworkCore.Sqlite.Query.Internal;
using Microsoft.IdentityModel.Tokens;

namespace Incon.Pages.ChatsPages
{
    public class CreateModel : PageModel
    {
        private readonly Incon.Models.InconContext _context;

        public CreateModel(Incon.Models.InconContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Chat Chat { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Chats == null || Chat == null)
            {
                return Page();
            }
            string name = Chat.Name;
            _context.Chats.Add(Chat);
            await _context.SaveChangesAsync();

            IList<Chat> Chats = default!;
            
            Chats = (from s in _context.Chats orderby s.Chatid descending select s).ToList();
            bool bl=false;
            foreach (Chat ch in Chats)
            {
                if(ch.Chatsaccounts.IsNullOrEmpty())    
                if (!bl && ch.Name.Contains(Chat.Name)) 
                {
                    bl = true;
                    Chat = ch;
                    break;
                }
                    
            }
            if(!bl) return RedirectToPage("/ChatsPages/IndexChat");
            Chatsaccount c = new Chatsaccount();
            c.Accountidref = Pages.GlobalSettings.Globalaccount.Accountid;
            c.Chatidref = Chat.Chatid;
            _context.Chatsaccounts.Add(c);
            _context.SaveChanges();


            return RedirectToPage("/ChatsPages/IndexChat");
        }
    }
}
