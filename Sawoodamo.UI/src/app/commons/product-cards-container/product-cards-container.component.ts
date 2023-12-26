import { Component, Input } from '@angular/core';
import { CardComponent } from '../card/card.component';
import { ProductListItemDTO } from '../../models/product/product-list-item.tso';

@Component({
  selector: 'product-cards-container',
  standalone: true,
  imports: [CardComponent],
  templateUrl: './product-cards-container.component.html'
})
export class ProductCardsContainerComponent {
  @Input() title: string | undefined;
  @Input() productList: ProductListItemDTO[] | null = []
}
