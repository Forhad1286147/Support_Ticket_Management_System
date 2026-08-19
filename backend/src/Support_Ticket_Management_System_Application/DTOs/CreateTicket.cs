using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.DTOs
{
    public class CreateTicket
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Priority { get; set; }

       
    }
}
