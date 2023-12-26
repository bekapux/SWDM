import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductListItemDTO } from '../models/product/product-list-item.tso';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  constructor(private http: HttpClient) { }

  public getPinnedProducts() : Observable<ProductListItemDTO[]>{
    return this.http.get<ProductListItemDTO[]>(`${environment.webapi}/product/pinned`);
  }
}
