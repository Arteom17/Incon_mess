using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Incon.Models;
using System.Collections;

namespace Incon.Pages.ChatsAccounts
{
    public class AddUserModel : PageModel
    {
        private readonly Incon.Models.InconContext _context;

        private static IList<Account> Acc { get; set; }


        public AddUserModel(Incon.Models.InconContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; } = "";
        [BindProperty(SupportsGet = true)]
        public string? SortString { get; set; } = "";
        [BindProperty(SupportsGet = true)]
        public string SortFilter { get; set; } = "asc";

        public IActionResult OnGetAsync(string searchString, string sortString, string sortFilter)
        {
            SearchString = searchString;
            SortString = sortString;
            SortFilter = sortFilter;






            if (_context.Accounts != null)
            {
                if ((SearchString != null && SearchString!="") || (SortString != null && SortString!=""))
                {
                    if (SearchString != null && SearchString!="")
                    {
                        Account = Acc;
                        IList<Account> a = new List<Account>();
                        try
                        {
                            var account = (from s in _context.Accounts where s.Name.Contains(SearchString) || s.Visid.Contains(SearchString) || s.Surname != null && s.Surname.Contains(SearchString) || s.Patronymic != null && s.Patronymic.Contains(SearchString) || s.Country != null && s.Country.Contains(SearchString) || s.City != null && s.City.Contains(SearchString) || s.Street != null && s.Street.Contains(SearchString) || s.Phone != null && s.Phone.Contains(SearchString) || s.Email != null && s.Email.Contains(SearchString) orderby s.Accountid select s).ToList();
                            var chatsaccount = (from s in _context.Chatsaccounts where s.Chatidref == Pages.GlobalSettings.GlobalChat.Chatid select s).ToList();
                            for (int i = 0; i < account.Count; i++)
                            {
                                bool bl = true;
                                foreach (var acc in chatsaccount)
                                {
                                    if (account[i].Accountid == acc.Accountidref){ bl = false; break;}
                                }
                                if (bl) a.Add(account[i]);
                            }
                            Account = a;
                            var z = Account;
                            Acc = z;
                        }
                        catch { return Page(); }
                        if (SortString != null && SortString!="")
                        {
                            try {
                                var account = (from s in _context.Accounts select s);
                                switch (SortString)
                                {
                                    case "name":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Name);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Name);
                                            }
                                        }
                                        break;
                                    case "surname":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Surname);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Surname);
                                            }
                                        }
                                        break;
                                    case "patronymic":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Patronymic);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Patronymic);
                                            }
                                        }
                                        break;
                                    case "visid":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Visid);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Visid);
                                            }
                                        }
                                        break;
                                    case "country":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Country);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Country);
                                            }
                                        }
                                        break;
                                    case "region":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Region);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Region);
                                            }
                                        }
                                        break;
                                    case "city":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.City);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.City);
                                            }
                                        }
                                        break;
                                    case "phone":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Phone);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Phone);
                                            }
                                        }
                                        break;
                                    case "email":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Email);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Email);
                                            }
                                        }
                                        break;
                                    default: { } break;
                                }
                                IList<Account> aa = new List<Account>();
                                var accc = account.ToList();
                                for (int i = 0; i < accc.Count; i++)
                                {
                                    foreach (var acc in Acc)
                                    {
                                        if (acc.Accountid == accc[i].Accountid){ aa.Add(accc[i]); break;}
                                    }
                                }
                                Account = aa;
                                var zz = Account;
                                Acc = zz;
                            }
                            catch { return Page(); }
                        }
                                return Page();
                    }
                    else
                    {

                        if (SortString != null && SortString != "")
                        {
                            try
                            {
                                var account = (from s in _context.Accounts select s);
                                var chatsaccount = (from s in _context.Chatsaccounts where s.Chatidref == Pages.GlobalSettings.GlobalChat.Chatid select s).ToList();
                                switch (SortString)
                                {
                                    case "name":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Name);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Name);
                                            }
                                        }
                                        break;
                                    case "surname":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Surname);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Surname);
                                            }
                                        }
                                        break;
                                    case "patronymic":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Patronymic);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Patronymic);
                                            }
                                        }
                                        break;
                                    case "visid":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Visid);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Visid);
                                            }
                                        }
                                        break;
                                    case "country":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Country);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Country);
                                            }
                                        }
                                        break;
                                    case "region":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Region);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Region);
                                            }
                                        }
                                        break;
                                    case "city":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.City);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.City);
                                            }
                                        }
                                        break;
                                    case "phone":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Phone);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Phone);
                                            }
                                        }
                                        break;
                                    case "email":
                                        {
                                            if (SortFilter == "asc")
                                            {
                                                account = account.OrderBy(s => s.Email);
                                            }
                                            else
                                            {
                                                account = account.OrderByDescending(s => s.Email);
                                            }
                                        }
                                        break;
                                    default: { } break;
                                }
                                IList<Account> aa = new List<Account>();
                                var accc = account.ToList();
                                for (int i = 0; i < accc.Count; i++)
                                {
                                    bool bl = true;
                                    foreach (var ac in chatsaccount)
                                    {
                                        if (accc[i].Accountid == ac.Accountidref){ bl = false; break;}
                                    }
                                    if (bl) { aa.Add(accc[i]); }
                                }
                                Account = aa;
                                var zz = Account;
                                Acc = zz;

                            }
                            catch { return Page(); }
                            return Page();
                        }
                    }



                }
                else
                {
                    try
                    {

                        Account = new List<Account>();
                        var account = (from s in _context.Accounts orderby s.Accountid select s).ToList();
                        var chatsaccount = (from s in _context.Chatsaccounts where s.Chatidref == Pages.GlobalSettings.GlobalChat.Chatid orderby s.Accountidref select s).ToList();
                        for (int i = 0; i < account.Count; i++)
                        {
                            bool bl = true;
                            foreach (var acc in chatsaccount)
                            {
                                if (account[i].Accountid == acc.Accountidref) { bl = false; break; }
                            }
                            if(bl) Account.Add(account[i]);
                        }
                        var a = Account;
                        Acc = a;
                    }
                    catch { return NotFound(); }
                    return Page();

                }
            }
            return NotFound();
        }
        public IActionResult OngetChoose(int id)
        {
            if (_context.Accounts != null)
            {
                try
                {
                    Chatsaccount c = new Chatsaccount();
                    c.Accountidref = id;
                    c.Chatidref = Pages.GlobalSettings.GlobalChat.Chatid;
                    _context.Chatsaccounts.Add(c);
                    _context.SaveChanges();
                    return RedirectToPage("/ChatsPages/DetailsChat", new {id =Pages.GlobalSettings.GlobalChat.Chatid});
                }
                catch { return Page(); }


            }
            return Page();
        }

    }
}
