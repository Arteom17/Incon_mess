using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Incon.Models;

namespace Incon.Pages.Messages
{
    public class EditMessage : PageModel
    {
        private readonly Incon.Models.InconContext _context;
        private static int ID { get; set; }
        private static DateTime DT { get; set; }

        public EditMessage(Incon.Models.InconContext context)
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

            var communicationobj =  await _context.Communicationobjs.FirstOrDefaultAsync(m => m.Communicationobjid == id);
            if (communicationobj == null)
            {
                return NotFound();
            }
            Communicationobj = communicationobj;
            ID = Communicationobj.Communicationobjid;
            try
            {
                DT = Communicationobj.Dateofdesign.Value;
            }
            catch { }
            if (communicationobj.Creatoraccountidref != Pages.GlobalSettings.Globalaccount.Accountid) return RedirectToPage("/ChatsPages/DetailsChat", new { id = Pages.GlobalSettings.GlobalChat.Chatid });
           
            
            ViewData["Chatidref"] = new SelectList(_context.Chats, "Chatid", "Chatid");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Communicationobj.Communicationobjid = ID;
            Communicationobj.Chatidref = Pages.GlobalSettings.GlobalChat.Chatid;
            Communicationobj.Creatoraccountidref = Pages.GlobalSettings.Globalaccount.Accountid;
            Communicationobj.Dateofdesign = DT;
            _context.Attach(Communicationobj).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunicationobjExists(Communicationobj.Communicationobjid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/ChatsPages/DetailsChat", new { id=Pages.GlobalSettings.GlobalChat.Chatid});
        }

        private bool CommunicationobjExists(int id)
        {
          return (_context.Communicationobjs?.Any(e => e.Communicationobjid == id)).GetValueOrDefault();
        }
    }
}
