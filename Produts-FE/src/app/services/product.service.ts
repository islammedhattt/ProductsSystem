import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// Define request and response interfaces
export interface GetProductByIdRequest {
  id: number;
}

export interface AddProductRequest {
  name: string;
  price: number;
  description: string;
}

export interface UpdateProductRequest {
  id: string;
  name: string;
  price: number;
  description: string;
}

export interface DeleteProductRequest {
  id: string;
}

export interface Product {
  id: string;
  name: string;
  price: number;
  description: string;
}

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl = 'https://localhost:7176/api/Product';

  constructor(private http: HttpClient) {}

  // Get a product by ID
  getProductById(request: GetProductByIdRequest): Observable<Product> {
    return this.http.post<Product>(`${this.apiUrl}/GetProductById`, request);
  }

  // Add a new product
  addProduct(request: AddProductRequest): Observable<Product> {
    return this.http.post<Product>(`${this.apiUrl}/AddProduct`, request);
  }

  // Get all products
  getAllProducts(): Observable<Product[]> {
    const request = {}; // Assuming GetAllProductsRequest has no properties
    return this.http.post<Product[]>(`${this.apiUrl}/GetAll`, request);
  }

  // Update a product
  updateProduct(request: UpdateProductRequest): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/UpdateProduct`, request);
  }

  // Delete a product
  deleteProduct(request: DeleteProductRequest): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/DeleteProduct`, request);
  }
}
