using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.DTOs
{
    public class UpdateNotification
    {
        public int Id { get; set; }

        public string? Message { get; set; }

        public bool? IsRead { get; set; }

    }
}
