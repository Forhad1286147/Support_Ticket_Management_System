import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { NotificationItem, CreateNotificationRequest, UpdateNotificationRequest } from '../models/notification.model';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private apiUrl = `${environment.apiUrl}/Notification`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<NotificationItem[]> {
    return this.http.get<NotificationItem[]>(`${this.apiUrl}/GetAll`);
  }

  getById(id: number): Observable<NotificationItem> {
    return this.http.get<NotificationItem>(`${this.apiUrl}/GetById/${id}`);
  }

  create(notification: CreateNotificationRequest): Observable<NotificationItem> {
    return this.http.post<NotificationItem>(`${this.apiUrl}/Add`, notification);
  }

  update(notification: UpdateNotificationRequest): Observable<NotificationItem> {
    return this.http.put<NotificationItem>(`${this.apiUrl}/Update`, notification);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Delete/${id}`);
  }
}
