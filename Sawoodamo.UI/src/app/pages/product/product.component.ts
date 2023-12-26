import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, share } from 'rxjs';
import { ProductDto } from '../../models/product/product.dto';
import { ProductService } from '../../_services/product.service';
import { CommonModule, NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss',
})
export class ProductComponent {
  public productObs$: Observable<ProductDto> = inject(ProductService)
    .getProductBySlug(inject(ActivatedRoute).snapshot.params['slug'])
    .pipe(share());
}
