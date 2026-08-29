import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Role, CreateRoleRequest, UpdateRoleRequest } from '../models/role.model';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private apiUrl = `${environment.apiUrl}/Role`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Role[]> {
    return this.http.get<Role[]>(`${this.apiUrl}/GetAll`);
  }

  getById(id: string): Observable<Role> {
    return this.http.get<Role>(`${this.apiUrl}/GetById?id=${id}`);
  }

  create(role: CreateRoleRequest): Observable<Role> {
    return this.http.post<Role>(`${this.apiUrl}/Add`, role);
  }

  update(id: string, role: UpdateRoleRequest): Observable<Role> {
    return this.http.put<Role>(`${this.apiUrl}/Update?id=${id}`, role);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Delete?id=${id}`);
  }
}
