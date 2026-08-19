using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.DTOs
{
    public class UpdateCategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
