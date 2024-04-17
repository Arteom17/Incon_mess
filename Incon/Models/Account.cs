using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Incon.Models;

public partial class Account
{
    public int Accountid { get; set; }

    public int Profileidref { get; set; }

    public string Name { get; set; } = null!;

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public string Visid { get; set; } = null!;

    public DateOnly? Dateofbirthday { get; set; }

    [NotMapped]
    public DateTime? Dateof { get; set; }

    public DateTime Dateofdesign { get; set; }

    public string? Country { get; set; }

    public string? Region { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public decimal? Housenum { get; set; }

    public decimal? Apartmentnum { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Chatsaccount> Chatsaccounts { get; set; } = new List<Chatsaccount>();

    public virtual Profile ProfileidrefNavigation { get; set; } = null!;

    public virtual ICollection<Account> Accountfriendidrefs { get; set; } = new List<Account>();

    public virtual ICollection<Account> Accountidrefs { get; set; } = new List<Account>();
}
