using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Incon.Models;
using NuGet.Versioning;
using Npgsql;

namespace Incon.Pages.ChatsPages
{
    public class DeleteFromChat : PageModel
    {
        private readonly InconContext _context;

        public DeleteFromChat(InconContext context)
        {
            _context = context;
        }

        // Строка подключения к базе данных PostgreSQL

        [BindProperty]
        public Chatsaccount Chatsaccount { get; set; } = default!;
        public Chat Chat { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Chatsaccounts == null)
            {
                return NotFound();
            }

            var chatsaccount = await _context.Chatsaccounts.FirstOrDefaultAsync(m => m.Chatidref == id && m.Accountidref==Pages.GlobalSettings.Globalaccount.Accountid);
            var chat = await _context.Chats.FirstOrDefaultAsync(m => m.Chatid == id);
            if (chatsaccount == null || chat ==null)
            {
                return NotFound();
            }
            else
            {
                Chatsaccount = chatsaccount;
                Chat = chat;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
        string cs = "Host=localhost;Database=incon;Username=postgres;Password=1234;Persist Security Info=True";
            if (id == null || _context.Chatsaccounts == null)
            {
                return NotFound();
            }



                var chatsaccount = await _context.Chatsaccounts.FindAsync(id, Pages.GlobalSettings.Globalaccount.Accountid);

            if (chatsaccount != null)
            {
                Chatsaccount = chatsaccount;
                _context.Chatsaccounts.Remove(Chatsaccount);
                await _context.SaveChangesAsync();

                //using (NpgsqlConnection con = new NpgsqlConnection(cs))
                //{
                //    con.Open();
                //    NpgsqlCommand com = new NpgsqlCommand("select * from delete_chat_if_emptys()", con);
                //    com.ExecuteReader();
                //}

            }

            return RedirectToPage("/ChatsPages/IndexChat");
        }
    }
}
