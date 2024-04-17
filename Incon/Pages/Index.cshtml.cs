using Incon.Models;
using Incon.Pages.LoginPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Diagnostics.Metrics;

namespace Incon.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Incon.Models.InconContext _context;

        private readonly ILogger<IndexModel> _logger;

        public static string str { get; set; } = "";

        public IndexModel(ILogger<IndexModel> logger, Incon.Models.InconContext context)
        {

            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet(int count)
        {
            if(count!=null && count==10)
            {
            Pages.GlobalSettings.Globalprofile = null;
            Pages.GlobalSettings.Globalaccount = null;
            }
            string cs = "Host=localhost;Database=incon;Username=postgres;Password=1234;Persist Security Info=True";
            using (NpgsqlConnection con = new NpgsqlConnection(cs))
            {
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand("select * from incon.get_row_counts();", con);
                NpgsqlDataReader reader =  com.ExecuteReader();
                reader.Read();
                str = reader.GetString(0); 
            }
            if (Pages.GlobalSettings.Globalprofile != null && Pages.GlobalSettings.Globalaccount == null) return RedirectToPage("/LoginPages/ChooseAccount");
            return Page();
        }

    }
}