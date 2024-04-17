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
    public class DetailsmessageModel : PageModel
    {
        private readonly Incon.Models.InconContext _context;

        public DetailsmessageModel(Incon.Models.InconContext context)
        {
            _context = context;
        }

      public Communicationobj Communicationobj { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Communicationobjs == null)
            {
                return NotFound();
            }

            var communicationobj = await _context.Communicationobjs.FirstOrDefaultAsync(m => m.Communicationobjid == id);
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
    }
}
