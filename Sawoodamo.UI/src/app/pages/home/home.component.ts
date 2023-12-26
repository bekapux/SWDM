import { Component, DestroyRef, Inject, OnInit, inject } from '@angular/core';
import { MainImageComponent } from './main-image/main-image.component';
import { ProductCardsContainerComponent } from '../../commons/product-cards-container/product-cards-container.component';
import { CardComponent } from '../../commons/card/card.component';
import { ProductListItemDTO } from '../../models/product/product-list-item.tso';
import { ProductService } from '../../_services/product.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { CommonModule } from '@angular/common';
import { PlaceholderService } from '../../_services/placeholder.service';

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
  pinnedProductsObservable = inject(ProductService).getPinnedProducts().pipe(takeUntilDestroyed());
  placeHolderObservable = inject(PlaceholderService).getPosts().pipe(takeUntilDestroyed());
}
