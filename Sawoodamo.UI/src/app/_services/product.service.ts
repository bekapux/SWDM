import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, shareReplay, tap } from 'rxjs';
import { ProductListItemDTO } from '../models/product/product-list-item.dto';
import { environment } from '../../environments/environment.development';
import { ProductDto } from '../models/product/product.dto';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) {}

  public getPinnedProducts(): Observable<ProductListItemDTO[]> {
    return this.http.get<ProductListItemDTO[]>(
      `${environment.webapi}/product/pinned`
    );
  }

  public getProductBySlug(slug: string): Observable<ProductDto> {
    return this.http.get<ProductDto>(
      `${environment.webapi}/product/by-slug/${slug}`
    );
  }
}
