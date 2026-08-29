using System;
using System.Collections.Generic;

namespace Support_Ticket.Domain.Entities;

public partial class Ticket
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public string? CreatedBy { get; set; }

    public string? Status { get; set; }

    public string? Priority { get; set; }

    public string? CreatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    public virtual Category? Category { get; set; }

    public virtual ICollection<TicketComment> TicketComments { get; set; } = new List<TicketComment>();
}
