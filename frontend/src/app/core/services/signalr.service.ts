import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject, Observable } from 'rxjs';
import { Ticket } from '../models/ticket.model';
import { Comment } from '../models/comment.model';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;

  private ticketNotificationSubject = new Subject<Ticket>();
  public ticketNotification$: Observable<Ticket> = this.ticketNotificationSubject.asObservable();

  private commentSubject = new Subject<Comment>();
  public comment$: Observable<Comment> = this.commentSubject.asObservable();

  constructor() {
    this.startConnection();
  }

  public startConnection(): void {
    const hubUrl = 'http://localhost:5043/hubs/ticket'; // Matches backend SignalR endpoint

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl, {
        skipNegotiation: false,
        transport: signalR.HttpTransportType.WebSockets | signalR.HttpTransportType.LongPolling
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('✅ SignalR Hub Connected Successfully.'))
      .catch(err => console.warn('⚠️ SignalR Connection Error:', err));

    this.registerSignalREvents();
  }

  private registerSignalREvents(): void {
    this.hubConnection.on('ReceiveTicketNotification', (ticket: Ticket) => {
      console.log('🔔 SignalR Ticket Event:', ticket);
      this.ticketNotificationSubject.next(ticket);
    });

    this.hubConnection.on('ReceiveComment', (comment: Comment) => {
      console.log('💬 SignalR Comment Event:', comment);
      this.commentSubject.next(comment);
    });
  }
}
