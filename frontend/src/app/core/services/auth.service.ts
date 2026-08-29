import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { LoginRequest, LoginResponse } from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/Auth`;
  private currentUserSubject = new BehaviorSubject<LoginResponse | null>(this.getStoredUser());
  public currentUser$ = this.currentUserSubject.asObservable();

  public readonly demoAccounts = [
    { role: 'Admin', email: 'admin@gmail.com', password: 'Admin@123', icon: '👑', badge: 'bg-purple' },
    { role: 'Agent', email: 'agent@gmail.com', password: 'Agent@123', icon: '🛠️', badge: 'bg-blue' },
    { role: 'Customer', email: 'customer@gmail.com', password: 'Customer@123', icon: '👤', badge: 'bg-green' }
  ];

  constructor(private http: HttpClient) {}

  public get currentUserValue(): LoginResponse | null {
    return this.currentUserSubject.value;
  }

  public get token(): string | null {
    return localStorage.getItem('auth_token');
  }

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/Login`, credentials).pipe(
      tap(res => {
        if (res && res.token) {
          localStorage.setItem('auth_token', res.token);
          localStorage.setItem('user_session', JSON.stringify(res));
          this.currentUserSubject.next(res);
        }
      })
    );
  }

  logout(): void {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('user_session');
    this.currentUserSubject.next(null);
  }

  isLoggedIn(): boolean {
    return !!this.token;
  }

  getUserRoles(): string[] {
    const user = this.currentUserValue;
    return user?.roles || [];
  }

  hasRole(role: string): boolean {
    const roles = this.getUserRoles();
    return roles.map(r => r.toLowerCase()).includes(role.toLowerCase());
  }

  isAdmin(): boolean {
    return this.hasRole('Admin');
  }

  isAgent(): boolean {
    return this.hasRole('Agent');
  }

  isCustomer(): boolean {
    return this.hasRole('Customer');
  }

  getPrimaryRole(): 'Admin' | 'Agent' | 'Customer' | 'User' {
    if (this.isAdmin()) return 'Admin';
    if (this.isAgent()) return 'Agent';
    if (this.isCustomer()) return 'Customer';
    return 'User';
  }

  private getStoredUser(): LoginResponse | null {
    const data = localStorage.getItem('user_session');
    if (!data) return null;
    try {
      return JSON.parse(data);
    } catch {
      return null;
    }
  }
}
