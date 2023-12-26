import { Component, inject } from '@angular/core';
import { MainImageComponent } from './main-image/main-image.component';
import { ProductCardsContainerComponent } from '../../commons/product-cards-container/product-cards-container.component';
import { CardComponent } from '../../commons/card/card.component';
import { ProductService } from '../../_services/product.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    MainImageComponent,
    ProductCardsContainerComponent,
    CardComponent,
    CommonModule,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent{
  pinnedProductsObservable = inject(ProductService).getPinnedProducts();
}
