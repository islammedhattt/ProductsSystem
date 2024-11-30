import { Component ,Input , EventEmitter, Output  } from '@angular/core';
import { Product ,  ProductService } from '../../services/product.service';
import { CommonModule  } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Import FormsModule
@Component({
  selector: 'app-update-product',
  standalone: true,
  imports: [CommonModule , FormsModule], // <-- Add CommonModule here

  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.scss']
})
export class UpdateProductComponent {

  @Output() productUpdated = new EventEmitter<void>(); // Event emitter for parent communication

cancel() {
  this.productUpdated.emit();

}
  @Input() product!: any;
  constructor(private productService: ProductService) {



  }

  updateProduct() {
    this.productService.updateProduct(this.product).subscribe(response => {
      console.log('Product updated successfully:', response);
      this.productUpdated.emit();

      // Add success handling or navigation
    });
  }
}
