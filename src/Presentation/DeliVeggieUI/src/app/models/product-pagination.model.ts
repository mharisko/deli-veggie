import { ProductModel } from "./product.model";

export class ProductPaginationModel {
  public data!: Array<ProductModel>;
  public recordsTotal!: number;
}
