import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ProductComponent } from './components/product/product.component';

@Component({
  selector: 'app-root',
  template: `
  <div>
    <app-product style="width:100%;"></app-product>
  </div>
`,
styles: [],
imports: [ProductComponent],
})
export class AppComponent {
  title = 'Produts-FE';
}
