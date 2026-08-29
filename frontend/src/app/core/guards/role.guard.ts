import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const expectedRoles: string[] = route.data['roles'] || [];
    if (!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return false;
    }

    if (expectedRoles.length === 0) return true;

    const userRoles = this.authService.getUserRoles().map(r => r.toLowerCase());
    const hasPermission = expectedRoles.some(role => userRoles.includes(role.toLowerCase()));

    if (hasPermission) {
      return true;
    }

    this.router.navigate(['/dashboard']);
    return false;
  }
}
