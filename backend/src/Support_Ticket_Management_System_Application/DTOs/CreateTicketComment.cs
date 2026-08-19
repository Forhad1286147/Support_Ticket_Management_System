using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.DTOs
{
    public class CreateTicketComment
    {
        public string? Comment { get; set; }

        public string? CreatedAt { get; set; }
    }
}
