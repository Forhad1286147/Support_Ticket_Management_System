export interface Role {
  id: string;
  name: string;
}

export interface CreateRoleRequest {
  name: string;
}

export interface UpdateRoleRequest {
  id: string;
  name: string;
}
