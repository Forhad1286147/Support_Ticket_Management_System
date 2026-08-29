import { Component, OnInit } from '@angular/core';
import { UserService } from '../../core/services/user.service';
import { RoleService } from '../../core/services/role.service';
import { CategoryService } from '../../core/services/category.service';
import { TicketService } from '../../core/services/ticket.service';
import { User, CreateUserRequest } from '../../core/models/user.model';
import { Role, CreateRoleRequest } from '../../core/models/role.model';
import { Category, CreateCategoryRequest } from '../../core/models/category.model';
import { Ticket } from '../../core/models/ticket.model';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {
  activeTab: 'overview' | 'users' | 'roles' | 'categories' | 'tickets' = 'overview';

  users: User[] = [];
  roles: Role[] = [];
  categories: Category[] = [];
  tickets: Ticket[] = [];

  // Modals state
  showUserModal = false;
  showRoleModal = false;
  showCategoryModal = false;

  newUser: CreateUserRequest = { userName: '', email: '', password: '' };
  newRole: CreateRoleRequest = { name: '' };
  newCategory: CreateCategoryRequest = { name: '' };

  msg = '';
  errorMsg = '';

  constructor(
    private userService: UserService,
    private roleService: RoleService,
    private categoryService: CategoryService,
    private ticketService: TicketService
  ) {}

  ngOnInit(): void {
    this.loadAllData();
  }

  loadAllData(): void {
    this.userService.getAll().subscribe(u => this.users = u || []);
    this.roleService.getAll().subscribe(r => this.roles = r || []);
    this.categoryService.getAll().subscribe(c => this.categories = c || []);
    this.ticketService.getAll().subscribe(t => this.tickets = t || []);
  }

  // User Management
  addUser(): void {
    if (!this.newUser.email || !this.newUser.userName || !this.newUser.password) return;
    this.userService.create(this.newUser).subscribe({
      next: () => {
        this.msg = 'User created successfully.';
        this.showUserModal = false;
        this.newUser = { userName: '', email: '', password: '' };
        this.loadAllData();
      },
      error: err => this.errorMsg = 'Error creating user.'
    });
  }

  deleteUser(id: string): void {
    if (confirm('Are you sure you want to delete this user?')) {
      this.userService.delete(id).subscribe({
        next: () => {
          this.msg = 'User deleted.';
          this.loadAllData();
        }
      });
    }
  }

  // Role Management
  addRole(): void {
    if (!this.newRole.name) return;
    this.roleService.create(this.newRole).subscribe({
      next: () => {
        this.msg = 'Role added.';
        this.showRoleModal = false;
        this.newRole = { name: '' };
        this.loadAllData();
      }
    });
  }

  deleteRole(id: string): void {
    if (confirm('Delete role?')) {
      this.roleService.delete(id).subscribe({
        next: () => {
          this.msg = 'Role deleted.';
          this.loadAllData();
        }
      });
    }
  }

  // Category Management
  addCategory(): void {
    if (!this.newCategory.name) return;
    this.categoryService.create(this.newCategory).subscribe({
      next: () => {
        this.msg = 'Category added.';
        this.showCategoryModal = false;
        this.newCategory = { name: '' };
        this.loadAllData();
      }
    });
  }

  deleteCategory(id: number): void {
    if (confirm('Delete category?')) {
      this.categoryService.delete(id).subscribe({
        next: () => {
          this.msg = 'Category deleted.';
          this.loadAllData();
        }
      });
    }
  }

  // Ticket status update
  updateTicketStatus(ticket: Ticket, newStatus: string): void {
    this.ticketService.update(ticket.id, { id: ticket.id, status: newStatus }).subscribe({
      next: () => {
        this.msg = `Ticket #${ticket.id} status updated to ${newStatus}`;
        this.loadAllData();
      }
    });
  }

  deleteTicket(id: number): void {
    if (confirm(`Are you sure you want to delete ticket #${id}?`)) {
      this.ticketService.delete(id).subscribe({
        next: () => {
          this.msg = `Ticket #${id} deleted.`;
          this.loadAllData();
        }
      });
    }
  }
}
