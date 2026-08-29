import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { User, CreateUserRequest, UpdateUserRequest } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = `${environment.apiUrl}/User`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/GetAll`);
  }

  getById(id: string): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/GetById?id=${id}`);
  }

  create(user: CreateUserRequest): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/Add`, user);
  }

  update(user: UpdateUserRequest): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/Update`, user);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Delete?id=${id}`);
  }
}
