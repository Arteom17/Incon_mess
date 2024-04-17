using System;
using System.Collections.Generic;

namespace Incon.Models;

public partial class Chatsaccount
{
    public int Chatidref { get; set; }

    public int Accountidref { get; set; }

    public DateTime Dateofadd { get; set; }

    public virtual Account AccountidrefNavigation { get; set; } = null!;

    public virtual Chat ChatidrefNavigation { get; set; } = null!;
}
