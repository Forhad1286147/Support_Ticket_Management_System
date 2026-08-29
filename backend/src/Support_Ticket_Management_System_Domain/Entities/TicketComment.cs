using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Support_Ticket.Domain.Entities;

public partial class TicketComment
{
    public int Id { get; set; }

    public int? TicketId { get; set; }

    public string? UserId { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    [JsonIgnore]
    public virtual Ticket? Ticket { get; set; }
}
