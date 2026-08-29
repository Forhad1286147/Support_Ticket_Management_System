export interface User {
  id: string;
  userName: string;
  email: string;
  phone?: string;
}

export interface CreateUserRequest {
  userName: string;
  email: string;
  password?: string;
}

export interface UpdateUserRequest {
  id: string;
  userName: string;
  phone: string;
  email: string;
  password?: string;
}
