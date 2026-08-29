export interface NotificationItem {
  id: number;
  userId?: string;
  message: string;
  isRead?: boolean;
  createdAt?: string;
}

export interface CreateNotificationRequest {
  message: string;
}

export interface UpdateNotificationRequest {
  id: number;
  userId?: string;
  message?: string;
  isRead?: boolean;
  createdAt?: string;
}
