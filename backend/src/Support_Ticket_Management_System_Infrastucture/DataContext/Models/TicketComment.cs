using System;
using System.Collections.Generic;

namespace Support_Ticket.Infrastucture.DataContext.Models;

public partial class TicketComment
{
    public int Id { get; set; }

    public int? TicketId { get; set; }

    public string? UserId { get; set; }

    public string? Comment { get; set; }

    public string? CreatedAt { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
