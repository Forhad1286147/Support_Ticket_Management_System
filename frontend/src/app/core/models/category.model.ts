export interface Category {
  id: number;
  name: string;
  isActive: boolean;
}

export interface CreateCategoryRequest {
  name: string;
}

export interface UpdateCategoryRequest {
  id: number;
  name: string;
  isActive?: boolean;
}
