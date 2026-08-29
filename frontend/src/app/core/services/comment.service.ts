import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { TicketComment, CreateCommentRequest, UpdateCommentRequest } from '../models/comment.model';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private apiUrl = `${environment.apiUrl}/TicketComment`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<TicketComment[]> {
    return this.http.get<TicketComment[]>(`${this.apiUrl}/GetAll`);
  }

  getById(id: number): Observable<TicketComment> {
    return this.http.get<TicketComment>(`${this.apiUrl}/GetById/${id}`);
  }

  create(comment: CreateCommentRequest): Observable<TicketComment> {
    return this.http.post<TicketComment>(`${this.apiUrl}/Add`, comment);
  }

  update(comment: UpdateCommentRequest): Observable<TicketComment> {
    return this.http.put<TicketComment>(`${this.apiUrl}/Update`, comment);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Delete/${id}`);
  }
}
