using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Support_Ticket.Api.Hubs
{
    public class TicketHub : Hub
    {
        public async Task JoinTicketGroup(string ticketId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Ticket_{ticketId}");
        }

        public async Task LeaveTicketGroup(string ticketId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Ticket_{ticketId}");
        }

        public async Task NotifyNewTicket(object ticketData)
        {
            await Clients.All.SendAsync("ReceiveTicketNotification", ticketData);
        }

        public async Task NotifyNewComment(object commentData)
        {
            await Clients.All.SendAsync("ReceiveComment", commentData);
        }
    }
}
