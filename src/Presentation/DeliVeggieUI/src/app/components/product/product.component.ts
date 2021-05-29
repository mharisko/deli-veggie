import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs/operators';
import { ProductModel } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  public product!: ProductModel;

  constructor(private productService: ProductService,
    private spinner: NgxSpinnerService,
    private activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    const productId = this.activeRoute.snapshot.paramMap.get("id") || '';
    this.getProduct(productId);
  }

  private getProduct(productId: string) {
    this.spinner.show();
    this.productService.getProduct(productId)
      .pipe(finalize(() => {
        this.spinner.hide();
      }))
      .subscribe(resp => {
        if (resp.ok && resp.body != null) {

          this.product = resp.body;
        }
      });
  }

}
