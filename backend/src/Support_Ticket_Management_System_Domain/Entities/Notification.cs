using System;
using System.Collections.Generic;

namespace Support_Ticket.Domain.Entities;

public partial class Notification
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public string? Message { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;
}
