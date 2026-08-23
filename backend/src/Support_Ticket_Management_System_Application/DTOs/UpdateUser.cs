using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.DTOs
{
    public class UpdateUser

    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string    Phone   { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
