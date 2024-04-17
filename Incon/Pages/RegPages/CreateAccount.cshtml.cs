using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Incon.Models;
using Incon.Pages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Incon.Pages.RegPages
{
    public class CreateAccount : PageModel
    {
        private readonly Incon.Models.InconContext _context;

        public static string BMessage { get; set; }

        public CreateAccount(Incon.Models.InconContext context)
        {

            _context = context;
        }

        public IActionResult OnGet()
        {
            if (Pages.GlobalSettings.Globalprofile == null)
                return RedirectToPage("/index");
            Pages.GlobalSettings.Logcheck = true;

            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Accounts == null || Account == null)
            {
                BMessage = "Ошибка ввода, попробуйте еще раз";
                return Page();
            }
            try
            {
                Pages.GlobalSettings.Logcheck = false;
                
                Account.Profileidref = Pages.GlobalSettings.Globalprofile.Profileid;
                Account.Name = Account.Name == "*" ? Pages.GlobalSettings.Globalprofile.Name is not null ? Pages.GlobalSettings.Globalprofile.Name : "" : Account.Name;
                Account.Surname = Account.Surname == "*" ? Pages.GlobalSettings.Globalprofile.Surname is not null ? Pages.GlobalSettings.Globalprofile.Surname : "" : Account.Surname;
                Account.Patronymic = Account.Patronymic == "*" ? Pages.GlobalSettings.Globalprofile.Patronymic is not null ? Pages.GlobalSettings.Globalprofile.Patronymic : "" : Account.Patronymic;
                Account.Dateofbirthday = Account.Dateof is not null ? Pages.GlobalSettings.Globalprofile.Dateofbirthday : null;
                Account.Phone = Account.Phone == "*" ? Pages.GlobalSettings.Globalprofile.Mainphone is not null ? Pages.GlobalSettings.Globalprofile.Mainphone : null : Account.Phone;
                Account.Email = Account.Email == "*" ? Pages.GlobalSettings.Globalprofile.Mainemail is not null ? Pages.GlobalSettings.Globalprofile.Mainemail : null : Account.Email;
                Account.Country = Account.Country == "*" ? Pages.GlobalSettings.Globalprofile.Country is not null ? Pages.GlobalSettings.Globalprofile.Country : null : Account.Country;
                Account.Region = Account.Region == "*" ? Pages.GlobalSettings.Globalprofile.Region is not null ? Pages.GlobalSettings.Globalprofile.Region : null : Account.Region;
                Account.City = Account.City == "*" ? Pages.GlobalSettings.Globalprofile.City is not null ? Pages.GlobalSettings.Globalprofile.City : null : Account.City;
                Account.Street = Account.Street == "*" ? Pages.GlobalSettings.Globalprofile.Street is not null ? Pages.GlobalSettings.Globalprofile.Street : null : Account.Street;
                Account.Housenum = Account.Housenum == 0 ? Pages.GlobalSettings.Globalprofile.Housenum is not null ? Pages.GlobalSettings.Globalprofile.Housenum : null : Account.Housenum;
                Account.Apartmentnum = Account.Apartmentnum == 0 ? Pages.GlobalSettings.Globalprofile.Apartmentnum is not null ? Pages.GlobalSettings.Globalprofile.Apartmentnum : null : Account.Apartmentnum;
                
                    _context.Accounts.Add(Account);
                    _context.SaveChanges();
                    Pages.GlobalSettings.Globalaccount = Account;                
            }
            catch (Exception ex)
            {
                Pages.GlobalSettings.Logcheck = true;
                BMessage = "Ошибка ввода, попробуйте еще раз";
                return Page();
            }


            return RedirectToPage("/index");//должно быть в чаты, хотя пофиг, есть шапка
        }
    }
}
