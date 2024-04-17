using System;
using System.Collections.Generic;

namespace Incon.Models;

public partial class Chat
{
    public int Chatid { get; set; }

    public DateTime Dateofdesign { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Chatsaccount> Chatsaccounts { get; set; } = new List<Chatsaccount>();

    public virtual ICollection<Communicationobj> Communicationobjs { get; set; } = new List<Communicationobj>();
}
