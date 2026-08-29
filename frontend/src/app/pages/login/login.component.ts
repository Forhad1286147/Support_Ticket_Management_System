import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email = '';
  password = '';
  errorMessage = '';
  isLoading = false;

  demoAccounts = this.authService.demoAccounts;

  constructor(private authService: AuthService, private router: Router) {
    if (this.authService.isLoggedIn()) {
      this.navigateUser();
    }
  }

  useDemoAccount(acc: { email: string; password: string }): void {
    this.email = acc.email;
    this.password = acc.password;
    this.onLogin();
  }

  onLogin(): void {
    if (!this.email || !this.password) {
      this.errorMessage = 'Please enter both email and password.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.authService.login({ email: this.email, password: this.password }).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.navigateUser();
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = err.error || 'Invalid credentials. Please check your email and password.';
      }
    });
  }

  private navigateUser(): void {
    const role = this.authService.getPrimaryRole();
    if (role === 'Admin') {
      this.router.navigate(['/admin']);
    } else if (role === 'Agent') {
      this.router.navigate(['/agent']);
    } else if (role === 'Customer') {
      this.router.navigate(['/customer']);
    } else {
      this.router.navigate(['/dashboard']);
    }
  }
}
