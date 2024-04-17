using Incon.Models;

namespace Incon.Pages
{
    public static class GlobalSettings
    {
        public static bool Logcheck { get; set; } = false;
        public static int? Accdel { get; set; } = null;
        public static Profile? Globalprofile { get; set; } = null;
        public static Account? Globalaccount { get; set; } = null;
        public static Chat? GlobalChat { get; set; } = null;

    }
}
