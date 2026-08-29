export interface TicketComment {
  id: number;
  ticketId?: number;
  userId?: string;
  comment: string;
  createdAt?: string;
}

export interface CreateCommentRequest {
  ticketId?: number;
  userId?: string;
  comment: string;
  createdAt?: string;
}

export interface UpdateCommentRequest {
  id: number;
  ticketId?: number;
  userId?: string;
  comment: string;
  createdAt?: string;
}
