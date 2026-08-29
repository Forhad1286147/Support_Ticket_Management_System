import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  constructor(public authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    const role = this.authService.getPrimaryRole();
    if (role === 'Admin') {
      this.router.navigate(['/admin']);
    } else if (role === 'Agent') {
      this.router.navigate(['/agent']);
    } else {
      this.router.navigate(['/customer']);
    }
  }
}
