import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AdminPanelComponent } from './pages/admin-panel/admin-panel.component';
import { AgentPanelComponent } from './pages/agent-panel/agent-panel.component';
import { CustomerPanelComponent } from './pages/customer-panel/customer-panel.component';
import { AuthGuard } from './core/guards/auth.guard';
import { RoleGuard } from './core/guards/role.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { 
    path: 'admin', 
    component: AdminPanelComponent, 
    canActivate: [AuthGuard, RoleGuard], 
    data: { roles: ['Admin'] } 
  },
  { 
    path: 'agent', 
    component: AgentPanelComponent, 
    canActivate: [AuthGuard, RoleGuard], 
    data: { roles: ['Agent'] } 
  },
  { 
    path: 'customer', 
    component: CustomerPanelComponent, 
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: ['Customer'] } 
  },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: '**', redirectTo: 'dashboard' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
