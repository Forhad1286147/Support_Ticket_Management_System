import { Category } from './category.model';
import { TicketComment } from './comment.model';

export interface Ticket {
  id: number;
  title: string;
  description: string;
  categoryId?: number;
  createdBy?: string;
  status: string;
  priority: string;
  createdAt?: string;
  category?: Category;
  ticketComments?: TicketComment[];
}

export interface CreateTicketRequest {
  title: string;
  description: string;
  priority: string;
}

export interface UpdateTicketRequest {
  id: number;
  title?: string;
  description?: string;
  status?: string;
  priority?: string;
}
