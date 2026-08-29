using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.DTOs
{
    public class UpdateTicketComment
    {
        public int Id { get; set; }

        public int? TicketId { get; set; }

        public string? UserId { get; set; }

        public string? Comment { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
