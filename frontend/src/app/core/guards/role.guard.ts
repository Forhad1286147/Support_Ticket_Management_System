import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return false;
    }

    const expectedRoles: string[] = route.data['roles'] || [];
    if (expectedRoles.length === 0) return true;

    const userRoles = this.authService.getUserRoles().map(r => r.toLowerCase());
    const hasPermission = expectedRoles.some(role => userRoles.includes(role.toLowerCase()));

    if (hasPermission) {
      return true;
    }

    // Redirect to the user's specific role panel if trying to access unauthorized panel
    this.redirectToRolePanel();
    return false;
  }

  public redirectToRolePanel(): void {
    const primaryRole = this.authService.getPrimaryRole();
    if (primaryRole === 'Admin') {
      this.router.navigate(['/admin']);
    } else if (primaryRole === 'Agent') {
      this.router.navigate(['/agent']);
    } else {
      this.router.navigate(['/customer']);
    }
  }
}
