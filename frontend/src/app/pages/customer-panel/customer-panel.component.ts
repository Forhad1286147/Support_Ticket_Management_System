import { Component, OnInit } from '@angular/core';
import { TicketService } from '../../core/services/ticket.service';
import { CategoryService } from '../../core/services/category.service';
import { CommentService } from '../../core/services/comment.service';
import { AuthService } from '../../core/services/auth.service';
import { Ticket, CreateTicketRequest } from '../../core/models/ticket.model';
import { Category } from '../../core/models/category.model';
import { TicketComment } from '../../core/models/comment.model';

@Component({
  selector: 'app-customer-panel',
  templateUrl: './customer-panel.component.html',
  styleUrls: ['./customer-panel.component.css']
})
export class CustomerPanelComponent implements OnInit {
  tickets: Ticket[] = [];
  categories: Category[] = [];

  showCreateModal = false;
  selectedTicket: Ticket | null = null;
  ticketComments: TicketComment[] = [];

  newTicket: CreateTicketRequest = {
    title: '',
    description: '',
    priority: 'Medium'
  };

  newCommentText = '';

  msg = '';
  errorMsg = '';

  constructor(
    private ticketService: TicketService,
    private categoryService: CategoryService,
    private commentService: CommentService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.ticketService.getAll().subscribe(res => this.tickets = res || []);
    this.categoryService.getAll().subscribe(res => this.categories = (res || []).filter(c => c.isActive));
  }

  createTicket(): void {
    if (!this.newTicket.title || !this.newTicket.description) {
      this.errorMsg = 'Please provide both title and description.';
      return;
    }

    this.ticketService.create(this.newTicket).subscribe({
      next: (t) => {
        this.msg = 'Your support ticket has been submitted successfully!';
        this.showCreateModal = false;
        this.newTicket = { title: '', description: '', priority: 'Medium' };
        this.loadData();
      },
      error: () => this.errorMsg = 'Failed to submit ticket.'
    });
  }

  openTicketDetails(ticket: Ticket): void {
    this.selectedTicket = ticket;
    this.commentService.getAll().subscribe({
      next: res => {
        this.ticketComments = (res || []).filter(c => c.ticketId === ticket.id);
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
      }
    });
  }

  closeModal(): void {
    this.selectedTicket = null;
    this.ticketComments = [];
  }
}
