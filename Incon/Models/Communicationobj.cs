using System;
using System.Collections.Generic;

namespace Incon.Models;

public partial class Communicationobj
{
    public int Communicationobjid { get; set; }

    public int Creatoraccountidref { get; set; }

    public string Text { get; set; } = null!;

    public DateTime? Dateofdesign { get; set; }

    public int? Chatidref { get; set; }

    public virtual Chat? ChatidrefNavigation { get; set; }
}
