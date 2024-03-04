import {
  ChangeDetectionStrategy,
  Component,
  DestroyRef,
  WritableSignal,
  inject,
  signal,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductDto } from '../../models/product/product.dto';
import { ProductService } from '../../_services/product.service';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { ButtonComponent } from '../../commons/button/button.component';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage, ButtonComponent],
  templateUrl: './product.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrl: './product.component.scss',
})
export class ProductComponent {
  public product: WritableSignal<ProductDto | null> = signal(null);
  public selectedImageIndex: WritableSignal<number> = signal(0);
  public quantity: WritableSignal<number> = signal(0);
  private destroyRef = inject(DestroyRef);

  constructor(private productService: ProductService) {
    productService
      .getProductBySlug(inject(ActivatedRoute).snapshot.params['slug'])
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((res) => {
        this.product.set(res);
      });
  }

  public selectImage(index: number) {
    this.selectedImageIndex.set(index);
  }

  public increaseQuantity() {
    this.quantity.set(this.quantity() + 1);
  }

  public decreaseQuantity() {
    if (this.quantity() >= 1) {
      this.quantity.set(this.quantity() - 1);
    }
  }
}
