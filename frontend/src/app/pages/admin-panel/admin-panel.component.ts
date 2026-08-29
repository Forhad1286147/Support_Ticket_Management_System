import { Component, OnInit } from '@angular/core';
import { UserService } from '../../core/services/user.service';
import { RoleService } from '../../core/services/role.service';
import { CategoryService } from '../../core/services/category.service';
import { TicketService } from '../../core/services/ticket.service';
import { User, CreateUserRequest, UpdateUserRequest } from '../../core/models/user.model';
import { Role, CreateRoleRequest, UpdateRoleRequest } from '../../core/models/role.model';
import { Category, CreateCategoryRequest, UpdateCategoryRequest } from '../../core/models/category.model';
import { Ticket, CreateTicketRequest, UpdateTicketRequest } from '../../core/models/ticket.model';

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
  showTicketModal = false;

  editingUser: UpdateUserRequest | null = null;
  editingRole: UpdateRoleRequest | null = null;
  editingCategory: UpdateCategoryRequest | null = null;
  editingTicket: UpdateTicketRequest | null = null;

  newUser: CreateUserRequest = { userName: '', email: '', password: '' };
  newRole: CreateRoleRequest = { name: '' };
  newCategory: CreateCategoryRequest = { name: '' };
  newTicket: CreateTicketRequest = { title: '', description: '', priority: 'Medium' };

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

  clearAlerts(): void {
    this.msg = '';
    this.errorMsg = '';
  }

  loadAllData(): void {
    this.userService.getAll().subscribe({
      next: u => this.users = u || [],
      error: err => this.errorMsg = err.error || 'Failed to load users.'
    });

    this.roleService.getAll().subscribe({
      next: r => this.roles = r || [],
      error: err => this.errorMsg = err.error || 'Failed to load roles.'
    });

    this.categoryService.getAll().subscribe({
      next: c => this.categories = c || [],
      error: err => this.errorMsg = err.error || 'Failed to load categories.'
    });

    this.ticketService.getAll().subscribe({
      next: t => this.tickets = t || [],
      error: err => this.errorMsg = err.error || 'Failed to load tickets.'
    });
  }

  // --- USER MANAGEMENT ---
  openCreateUserModal(): void {
    this.clearAlerts();
    this.editingUser = null;
    this.newUser = { userName: '', email: '', password: '' };
    this.showUserModal = true;
  }

  openEditUserModal(user: User): void {
    this.clearAlerts();
    this.editingUser = { id: user.id, userName: user.userName, email: user.email, phone: user.phone || '', password: '' };
    this.showUserModal = true;
  }

  saveUser(): void {
    this.clearAlerts();
    if (this.editingUser) {
      this.userService.update(this.editingUser).subscribe({
        next: () => {
          this.msg = 'User updated successfully.';
          this.showUserModal = false;
          this.editingUser = null;
          this.loadAllData();
        },
        error: err => this.errorMsg = typeof err.error === 'string' ? err.error : 'Failed to update user.'
      });
    } else {
      if (!this.newUser.userName || !this.newUser.email || !this.newUser.password) {
        this.errorMsg = 'Username, email and password are required.';
        return;
      }
      this.userService.create(this.newUser).subscribe({
        next: () => {
          this.msg = 'User created successfully.';
          this.showUserModal = false;
          this.newUser = { userName: '', email: '', password: '' };
          this.loadAllData();
        },
        error: err => this.errorMsg = typeof err.error === 'string' ? err.error : 'Failed to create user. Ensure password has uppercase, number & symbol.'
      });
    }
  }

  deleteUser(id: string): void {
    this.clearAlerts();
    if (confirm('Are you sure you want to delete this user?')) {
      this.userService.delete(id).subscribe({
        next: () => {
          this.msg = 'User deleted successfully.';
          this.loadAllData();
        },
        error: err => this.errorMsg = 'Failed to delete user.'
      });
    }
  }

  // --- ROLE MANAGEMENT ---
  openCreateRoleModal(): void {
    this.clearAlerts();
    this.editingRole = null;
    this.newRole = { name: '' };
    this.showRoleModal = true;
  }

  openEditRoleModal(role: Role): void {
    this.clearAlerts();
    this.editingRole = { id: role.id, name: role.name };
    this.showRoleModal = true;
  }

  saveRole(): void {
    this.clearAlerts();
    if (this.editingRole) {
      this.roleService.update(this.editingRole.id, this.editingRole).subscribe({
        next: () => {
          this.msg = 'Role updated successfully.';
          this.showRoleModal = false;
          this.editingRole = null;
          this.loadAllData();
        },
        error: err => this.errorMsg = 'Failed to update role.'
      });
    } else {
      if (!this.newRole.name) return;
      this.roleService.create(this.newRole).subscribe({
        next: () => {
          this.msg = 'Role created successfully.';
          this.showRoleModal = false;
          this.newRole = { name: '' };
          this.loadAllData();
        },
        error: err => this.errorMsg = 'Failed to create role.'
      });
    }
  }

  deleteRole(id: string): void {
    this.clearAlerts();
    if (confirm('Delete role?')) {
      this.roleService.delete(id).subscribe({
        next: () => {
          this.msg = 'Role deleted successfully.';
          this.loadAllData();
        },
        error: err => this.errorMsg = 'Failed to delete role.'
      });
    }
  }

  // --- CATEGORY MANAGEMENT ---
  openCreateCategoryModal(): void {
    this.clearAlerts();
    this.editingCategory = null;
    this.newCategory = { name: '' };
    this.showCategoryModal = true;
  }

  openEditCategoryModal(cat: Category): void {
    this.clearAlerts();
    this.editingCategory = { id: cat.id, name: cat.name, isActive: cat.isActive };
    this.showCategoryModal = true;
  }

  saveCategory(): void {
    this.clearAlerts();
    if (this.editingCategory) {
      this.categoryService.update(this.editingCategory).subscribe({
        next: () => {
          this.msg = 'Category updated successfully.';
          this.showCategoryModal = false;
          this.editingCategory = null;
          this.loadAllData();
        },
        error: err => this.errorMsg = 'Failed to update category.'
      });
    } else {
      if (!this.newCategory.name) return;
      this.categoryService.create(this.newCategory).subscribe({
        next: () => {
          this.msg = 'Category created successfully.';
          this.showCategoryModal = false;
          this.newCategory = { name: '' };
          this.loadAllData();
        },
        error: err => this.errorMsg = 'Failed to create category.'
      });
    }
  }

  deleteCategory(id: number): void {
    this.clearAlerts();
    if (confirm('Delete category?')) {
      this.categoryService.delete(id).subscribe({
        next: () => {
          this.msg = 'Category deleted successfully.';
          this.loadAllData();
        },
        error: err => this.errorMsg = 'Failed to delete category.'
      });
    }
  }

  // --- TICKET MANAGEMENT ---
  openCreateTicketModal(): void {
    this.clearAlerts();
    this.editingTicket = null;
    this.newTicket = { title: '', description: '', priority: 'Medium' };
    this.showTicketModal = true;
  }

  openEditTicketModal(ticket: Ticket): void {
    this.clearAlerts();
    this.editingTicket = {
      id: ticket.id,
      title: ticket.title,
      description: ticket.description,
      status: ticket.status,
      priority: ticket.priority
    };
    this.showTicketModal = true;
  }

  saveTicket(): void {
    this.clearAlerts();
    if (this.editingTicket) {
      this.ticketService.update(this.editingTicket.id, this.editingTicket).subscribe({
        next: () => {
          this.msg = `Ticket #${this.editingTicket?.id} updated successfully.`;
          this.showTicketModal = false;
          this.editingTicket = null;
          this.loadAllData();
        },
        error: err => this.errorMsg = 'Failed to update ticket.'
      });
    } else {
      if (!this.newTicket.title || !this.newTicket.description) return;
      this.ticketService.create(this.newTicket).subscribe({
        next: () => {
          this.msg = 'Ticket created successfully.';
          this.showTicketModal = false;
          this.newTicket = { title: '', description: '', priority: 'Medium' };
          this.loadAllData();
        },
        error: err => this.errorMsg = 'Failed to create ticket.'
      });
    }
  }

  updateTicketStatus(ticket: Ticket, newStatus: string): void {
    this.clearAlerts();
    this.ticketService.update(ticket.id, { id: ticket.id, status: newStatus }).subscribe({
      next: () => {
        ticket.status = newStatus;
        this.msg = `Ticket #${ticket.id} status updated to ${newStatus}`;
      },
      error: err => this.errorMsg = 'Failed to update ticket status.'
    });
  }

  deleteTicket(id: number): void {
    this.clearAlerts();
    if (confirm(`Are you sure you want to delete ticket #${id}?`)) {
      this.ticketService.delete(id).subscribe({
        next: () => {
          this.msg = `Ticket #${id} deleted successfully.`;
          this.loadAllData();
        },
        error: err => this.errorMsg = 'Failed to delete ticket.'
      });
    }
  }
}
