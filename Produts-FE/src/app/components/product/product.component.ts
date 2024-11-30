import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // <-- Import CommonModule here
import { Product, ProductService, DeleteProductRequest } from '../../services/product.service';
import { CreateProductComponent } from './../create-product/create-product.component';  // Import CreateProductComponent
import { UpdateProductComponent } from './../update-product/update-product.component';  // Import UpdateProductComponent

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule, CreateProductComponent, UpdateProductComponent], // <-- Add CommonModule here
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
onProductupdate() {
  console.log('Product created, switching back to list view.');
  this.showUpdateForm = false;
  this.showList = true;
  this.loadProducts(); // Optionally reload the products
}
  products: Product[] = [];
showCreateForm: boolean = false;
showList:boolean = true;
showUpdateForm: any;
selectedProduct: any = null; // Declare selectedProduct
  constructor(private productService: ProductService) {}

  // Load all products
  loadProducts(): void {
    this.productService.getAllProducts().subscribe((data) => {
      this.products = data;
    });
  }

  // Delete a product by ID
  deleteProduct(id: string): void {
    const request: DeleteProductRequest = { id }; // Create request payload
    this.productService.deleteProduct(request).subscribe(() => {
      this.products = this.products.filter((p) => p.id !== id);
    });
  }

  // Open create product form
  openCreateForm(): void {
    this.showCreateForm = true;
    this.showUpdateForm = false;
    this.showList = false;

    // Code to open the create product form
  }

  // Open update product form
  openUpdateForm(product: any) {
    this.showUpdateForm = true;
    this.showCreateForm = false;
    this.showList = false;


    this.selectedProduct = product; // Set the selected product
    console.log('Open Update Form with product:', this.selectedProduct);
  }

  // Load products on component initialization
  ngOnInit(): void {
    this.loadProducts();
  }

  onProductCreated() {
    console.log('Product created, switching back to list view.');
    this.showCreateForm = false;
    this.showList = true;
    this.loadProducts(); // Optionally reload the products
  }
}
