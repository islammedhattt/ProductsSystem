import { Component ,  EventEmitter, Output  } from '@angular/core';
import { Product, ProductService } from '../../services/product.service';
import { CommonModule  } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Import FormsModule

@Component({
  selector: 'app-create-product',
  standalone: true,
  imports: [CommonModule , FormsModule], // <-- Add CommonModule here

  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.scss']
})
export class CreateProductComponent {
  @Output() productCreated = new EventEmitter<void>(); // Event emitter for parent communication

cancel() {
  this.productCreated.emit();
}
  product: Product = {id:'', name: '', price: 0, description: '' };

  constructor(private productService: ProductService) {}

  createProduct() {
    this.productService.addProduct(this.product).subscribe(response => {
      console.log('Product created successfully:', response);
     
      this.productCreated.emit();
      // Add success handling or navigation
    });
  }
}
