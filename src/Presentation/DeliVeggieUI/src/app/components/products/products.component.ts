import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { FilterModel } from 'src/app/models/filter.model';
import { ProductModel } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  public page = 1;
  public pageSize = 10;
  public totalProducts = 0;
  public products: ProductModel[] = [];

  constructor(private productService: ProductService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.getProducts();
  }

  public getProducts() {
    this.spinner.show();
    const skip = ((this.page - 1) || 0) * this.pageSize;
    var filter = {
      skip: skip,
      limit: this.pageSize,
    } as FilterModel;

    this.productService.getProducts(filter)
      .pipe(finalize(() => {
        this.spinner.hide();
      }))
      .subscribe(resp => {
        if (resp.ok && resp.body != null) {

          if (this.page === 1) {
            this.totalProducts = resp.body.recordsTotal;
          }

          this.products = [...resp.body.data.slice()];
        }
      });
  }

}
