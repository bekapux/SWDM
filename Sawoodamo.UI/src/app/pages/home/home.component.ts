import { Component } from '@angular/core';
import { MainImageComponent } from './main-image/main-image.component';
import { ProductCardsContainerComponent } from '../../commons/product-cards-container/product-cards-container.component';
import { CardComponent } from '../../commons/card/card.component';
import { ProductListItemDTO } from '../../models/product/product-list-item.tso';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MainImageComponent, ProductCardsContainerComponent, CardComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  pinnedProducts: ProductListItemDTO[] = [
    {
      name: 'wooden spoon',
      shortDescription: 'wooden spoon one',
      fullDescription: 'wooden spoon one extra woody',
      discount: 10,
      price: 100,
      slug: 'product-1',
      mainImageUrl:
        'https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c',
    }
    // {
    //   name: 'wooden spoon',
    //   shortDescription: 'wooden spoon one',
    //   fullDescription: 'wooden spoon one extra woody',
    //   discount: 10,
    //   price: 100,
    //   slug: 'product-1',
    //   mainImageUrl: 'https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c',
    // },
    // {
    //   name: 'wooden spoon',
    //   shortDescription: 'wooden spoon one',
    //   fullDescription: 'wooden spoon one extra woody',
    //   discount: 10,
    //   price: 100,
    //   slug: 'product-1',
    //   mainImageUrl: 'https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c',
    // },
    // {
    //   name: 'wooden spoon',
    //   shortDescription: 'wooden spoon one',
    //   fullDescription: 'wooden spoon one extra woody',
    //   discount: 10,
    //   price: 100,
    //   slug: 'product-1',
    //   mainImageUrl: 'https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c',
    // },
    // {
    //   name: 'wooden spoon',
    //   shortDescription: 'wooden spoon one',
    //   fullDescription: 'wooden spoon one extra woody',
    //   discount: 10,
    //   price: 100,
    //   slug: 'product-1',
    //   mainImageUrl: 'https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c',
    // },
    // {
    //   name: 'wooden spoon',
    //   shortDescription: 'wooden spoon one',
    //   fullDescription: 'wooden spoon one extra woody',
    //   discount: 10,
    //   price: 100,
    //   slug: 'product-1',
    //   mainImageUrl: 'https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c',
    // },
    // {
    //   name: 'wooden spoon',
    //   shortDescription: 'wooden spoon one',
    //   fullDescription: 'wooden spoon one extra woody',
    //   discount: 10,
    //   price: 100,
    //   slug: 'product-1',
    //   mainImageUrl: 'https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c',
    // },
    // {
    //   name: 'wooden spoon',
    //   shortDescription: 'wooden spoon one',
    //   fullDescription: 'wooden spoon one extra woody',
    //   discount: 10,
    //   price: 100,
    //   slug: 'product-1',
    //   mainImageUrl: 'https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c',
    // },
    // {
    //   name: 'wooden spoon',
    //   shortDescription: 'wooden spoon one',
    //   fullDescription: 'wooden spoon one extra woody',
    //   discount: 10,
    //   price: 100,
    //   slug: 'product-1',
    //   mainImageUrl: 'https://sawoodamo.s3.eu-central-1.amazonaws.com/1a60e232-930f-4b21-83c3-f9f59a005a9c',
    // },
  ];
}
