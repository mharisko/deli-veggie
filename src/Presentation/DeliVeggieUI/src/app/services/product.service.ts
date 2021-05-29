import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { FilterModel } from '../models/filter.model';
import { ProductPaginationModel } from '../models/product-pagination.model';
import { ProductModel } from '../models/product.model';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpService: HttpService) { }

  public getProducts(filter: FilterModel) {
    return this.httpService.get<ProductPaginationModel>(this.httpService.getPaginationApiUrl(filter));
  }

  public getProduct(productId: string) {
    return this.httpService.get<ProductModel>(`${environment.apiUrl}/api/v1.0/products/${productId}`);
  }
}
