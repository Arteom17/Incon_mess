using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Incon.Models;

public partial class Profile
{
    public int Profileid { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Login { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;
    
    [NotMapped]
    public string? Pass { get; set; } = null!;

    public DateOnly Dateofbirthday { get; set; }

    [NotMapped]
    public DateTime Dateof { get; set; }

    public DateTime Dateofdesign { get; set; }

    public string Mainphone { get; set; } = null!;

    public string? Mainemail { get; set; }

    public string? Country { get; set; }

    public string? Region { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public decimal? Housenum { get; set; }

    public decimal? Apartmentnum { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
