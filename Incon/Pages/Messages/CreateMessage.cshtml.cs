using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Incon.Models;

namespace Incon.Pages.Messages
{
    public class CreateMessage : PageModel
    {
        private readonly Incon.Models.InconContext _context;

        public CreateMessage(Incon.Models.InconContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Chatidref"] = new SelectList(_context.Chats, "Chatid", "Chatid");
            return Page();
        }

        [BindProperty]
        public Communicationobj Communicationobj { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Communicationobjs == null || Communicationobj == null)
            {
                return Page();
            }

            Communicationobj.Chatidref = Pages.GlobalSettings.GlobalChat.Chatid;
            Communicationobj.Creatoraccountidref = Pages.GlobalSettings.Globalaccount.Accountid;



            _context.Communicationobjs.Add(Communicationobj);
            await _context.SaveChangesAsync();

            return RedirectToPage("/ChatsPages/DetailsChat", new {id = Pages.GlobalSettings.GlobalChat.Chatid });
        }
    }
}
