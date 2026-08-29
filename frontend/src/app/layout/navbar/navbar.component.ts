import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { NotificationService } from '../../core/services/notification.service';
import { NotificationItem } from '../../core/models/notification.model';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  notifications: NotificationItem[] = [];
  showNotifDropdown = false;

  constructor(
    public authService: AuthService,
    private notificationService: NotificationService,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.loadNotifications();
    }
  }

  loadNotifications(): void {
    this.notificationService.getAll().subscribe({
      next: (res) => {
        this.notifications = res || [];
      },
      error: () => {
        // Fallback demo notifications if DB empty
        this.notifications = [
          { id: 1, message: 'Welcome to SupportDesk Pro', isRead: false, createdAt: new Date().toISOString() }
        ];
      }
    });
  }

  get unreadCount(): number {
    return this.notifications.filter(n => !n.isRead).length;
  }

  toggleNotifDropdown(): void {
    this.showNotifDropdown = !this.showNotifDropdown;
  }

  markAsRead(item: NotificationItem): void {
    item.isRead = true;
    this.notificationService.update({ id: item.id, isRead: true }).subscribe();
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
