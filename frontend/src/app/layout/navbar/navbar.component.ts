import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { NotificationService } from '../../core/services/notification.service';
import { SignalRService } from '../../core/services/signalr.service';
import { NotificationItem } from '../../core/models/notification.model';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  notifications: NotificationItem[] = [];
  showNotifDropdown = false;
  toastMessage: string | null = null;

  constructor(
    public authService: AuthService,
    private notificationService: NotificationService,
    private signalRService: SignalRService,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.loadNotifications();
    }

    // Subscribe to real-time SignalR notifications
    this.signalRService.ticketNotification$.subscribe(ticket => {
      const msg = `🎫 New Ticket #${ticket.id}: "${ticket.title}" submitted!`;
      this.showToast(msg);
      this.notifications.unshift({ id: Date.now(), message: msg, isRead: false, createdAt: new Date().toISOString() });
    });

    this.signalRService.comment$.subscribe(comment => {
      const msg = `💬 New reply on Ticket #${comment.ticketId}: "${comment.comment}"`;
      this.showToast(msg);
      this.notifications.unshift({ id: Date.now(), message: msg, isRead: false, createdAt: new Date().toISOString() });
    });
  }

  showToast(message: string): void {
    this.toastMessage = message;
    setTimeout(() => {
      if (this.toastMessage === message) {
        this.toastMessage = null;
      }
    }, 5000);
  }

  loadNotifications(): void {
    this.notificationService.getAll().subscribe({
      next: (res) => {
        this.notifications = res || [];
      },
      error: () => {
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
