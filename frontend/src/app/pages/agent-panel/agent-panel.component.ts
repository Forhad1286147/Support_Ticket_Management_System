import { Component, OnInit } from '@angular/core';
import { TicketService } from '../../core/services/ticket.service';
import { CommentService } from '../../core/services/comment.service';
import { AuthService } from '../../core/services/auth.service';
import { Ticket } from '../../core/models/ticket.model';
import { TicketComment } from '../../core/models/comment.model';

@Component({
  selector: 'app-agent-panel',
  templateUrl: './agent-panel.component.html',
  styleUrls: ['./agent-panel.component.css']
})
export class AgentPanelComponent implements OnInit {
  tickets: Ticket[] = [];
  filteredTickets: Ticket[] = [];
  statusFilter: string = 'All';

  // Detail & Comments Modal
  selectedTicket: Ticket | null = null;
  ticketComments: TicketComment[] = [];
  newCommentText = '';

  msg = '';
  errorMsg = '';

  constructor(
    private ticketService: TicketService,
    private commentService: CommentService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets(): void {
    this.ticketService.getAll().subscribe({
      next: res => {
        this.tickets = res || [];
        this.applyFilter();
      }
    });
  }

  applyFilter(): void {
    if (this.statusFilter === 'All') {
      this.filteredTickets = this.tickets;
    } else {
      this.filteredTickets = this.tickets.filter(t => t.status === this.statusFilter);
    }
  }

  setFilter(status: string): void {
    this.statusFilter = status;
    this.applyFilter();
  }

  updateStatus(ticket: Ticket, newStatus: string, event?: Event): void {
    if (event) event.stopPropagation();
    this.ticketService.update(ticket.id, { id: ticket.id, status: newStatus }).subscribe({
      next: () => {
        ticket.status = newStatus;
        this.msg = `Ticket #${ticket.id} status changed to ${newStatus}`;
        this.applyFilter();
      }
    });
  }

  openTicketDetails(ticket: Ticket): void {
    this.selectedTicket = ticket;
    this.loadComments(ticket.id);
  }

  loadComments(ticketId: number): void {
    this.commentService.getAll().subscribe({
      next: res => {
        this.ticketComments = (res || []).filter(c => c.ticketId === ticketId);
      }
    });
  }

  addComment(): void {
    if (!this.newCommentText || !this.selectedTicket) return;
    const userId = this.authService.currentUserValue?.userId || '';
    this.commentService.create({
      ticketId: this.selectedTicket.id,
      userId: userId,
      comment: this.newCommentText,
      createdAt: new Date().toISOString()
    }).subscribe({
      next: (c) => {
        this.ticketComments.push(c);
        this.newCommentText = '';
        this.msg = 'Comment posted successfully.';
      }
    });
  }

  closeModal(): void {
    this.selectedTicket = null;
    this.ticketComments = [];
  }

  get countOpen(): number {
    return this.tickets.filter(t => t.status === 'Open').length;
  }

  get countInProgress(): number {
    return this.tickets.filter(t => t.status === 'In Progress').length;
  }

  get countResolved(): number {
    return this.tickets.filter(t => t.status === 'Resolved').length;
  }
}
