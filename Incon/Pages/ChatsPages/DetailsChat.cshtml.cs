using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Incon.Models;
using MessagePack;

namespace Incon.Pages.ChatsPages
{
    public class DetailsChat  : PageModel
    {
        private readonly Incon.Models.InconContext _context;


        public DetailsChat(Incon.Models.InconContext context)
        {
            _context = context;
        }

        public class messages
        {
            public int accid { get; set; } = default!;
            public string? name { get; set; } = default!;
            public string? vid { get; set; } = default!;
            public int commid { get; set; } = default!;
            public string text { get; set; } = default!;
            public DateTime? dt { get; set; } = default!;

            public messages( int _accid, string _name, string _vid, int _commid, string _text, DateTime? _dt )
            {
                accid = _accid;
                name= _name;
                vid= _vid;
                commid= _commid;
                text= _text;
                dt= _dt;
            }

        }
        public Chat Chat { get; set; } = default!;
        public List<Communicationobj> Mess { get; set; } = default!;
        public List<Account> Acc { get; set; } = default!;

        public static IList<messages> Messs { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Chats == null)
            {
                return NotFound();
            }
            var chat = await _context.Chats.FirstOrDefaultAsync(m => m.Chatid == id);
            Pages.GlobalSettings.GlobalChat = chat;
            var mess = (from s in _context.Communicationobjs where s.Chatidref == id orderby s.Dateofdesign descending select s).ToList();
            var acc = (from s in _context.Communicationobjs where s.Chatidref == id select s).ToList();
            if (chat == null || mess ==null || acc ==null)
            {
                return NotFound();
            }
            else 
            {
                Chat = chat;
                Mess = mess;
                Acc = Acc;

                Messs=new List<messages>();

                foreach (Communicationobj co in Mess)
                {
                    try
                    {

                        
                        var ac = _context.Accounts.FirstOrDefault(m => m.Accountid == co.Creatoraccountidref);
                        if(ac!=null)
                            Messs.Add(new messages(co.Creatoraccountidref, ac.Name, ac.Visid, co.Communicationobjid, co.Text, co.Dateofdesign));
                        else
                            Messs.Add(new messages(0, "", "", co.Communicationobjid, co.Text, co.Dateofdesign));
                    }
                    catch(Exception ex)
                    {
                    }
                }
            }
            return Page();
        }
        //public async Task<IActionResult> OnGetEdit(int id)
        //{
        
        //}
    }
}
