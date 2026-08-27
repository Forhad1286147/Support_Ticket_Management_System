using System;
using System.Collections.Generic;

namespace Support_Ticket.Domain.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();


}
