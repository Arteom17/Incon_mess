using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Incon.Models;

namespace Incon.Pages.Messages
{
    public class DeleteMessage : PageModel
    {
        private readonly Incon.Models.InconContext _context;

        public DeleteMessage(Incon.Models.InconContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Communicationobj Communicationobj { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Communicationobjs == null)
            {
                return NotFound();
            }

            var communicationobj = await _context.Communicationobjs.FirstOrDefaultAsync(m => m.Communicationobjid == id);
            if (communicationobj.Creatoraccountidref != Pages.GlobalSettings.Globalaccount.Accountid) return RedirectToPage("/ChatsPages/DetailsChat", new { id = Pages.GlobalSettings.GlobalChat.Chatid });
            if (communicationobj == null)
            {
                return NotFound();
            }
            else 
            {
                Communicationobj = communicationobj;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Communicationobjs == null)
            {
                return NotFound();
            }
            var communicationobj = await _context.Communicationobjs.FindAsync(id);

            if (communicationobj != null)
            {
                Communicationobj = communicationobj;
                _context.Communicationobjs.Remove(Communicationobj);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/ChatsPages/DetailsChat", new { id = Pages.GlobalSettings.GlobalChat.Chatid });
        }
    }
}
