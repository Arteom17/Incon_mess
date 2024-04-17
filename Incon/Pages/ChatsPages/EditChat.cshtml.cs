using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Incon.Models;

namespace Incon.Pages.ChatsPages
{
    public class EditModel : PageModel
    {
        private readonly Incon.Models.InconContext _context;
        public EditModel(Incon.Models.InconContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Chat Chat { get; set; } = default!;
        private static DateTime DT { get; set; }
        private static Chat CH { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Chats == null)
            {
                return NotFound();
            }

            var chat =  await _context.Chats.FirstOrDefaultAsync(m => m.Chatid == id);
            DT = chat.Dateofdesign;
            if (chat == null)
            {
                return NotFound();
            }
            Chat = chat;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Chat.Dateofdesign = DT;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Chat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatExists(Chat.Chatid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/ChatsPages/IndexChat");
        }

        private bool ChatExists(int id)
        {
          return (_context.Chats?.Any(e => e.Chatid == id)).GetValueOrDefault();
        }
    }
}
