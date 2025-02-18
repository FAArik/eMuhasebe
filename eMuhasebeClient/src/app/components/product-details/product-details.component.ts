import { Component, ElementRef, ViewChild } from '@angular/core';
import { CustomerDetailModel } from '../../models/customer-detail.model';
import { SharedModule } from '../../modules/shared.module';
import { CustomerDetailPipe } from '../../pipes/customer-detail.pipe';
import { HttpService } from '../../services/http.service';
import { ActivatedRoute } from '@angular/router';
import { ProductDetailModel } from '../../models/product-detail.model';
import { ProductModel } from '../../models/product.model';
import { ProductDetailPipe } from '../../pipes/product-detail.pipe';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [SharedModule, ProductDetailPipe],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent {
  product: ProductModel = new ProductModel();
  productId: string = "";
  search: string = "";

  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  constructor(
    private http: HttpService,
    private activated: ActivatedRoute,
  ) {
    this.activated.params.subscribe((params) => {
      this.productId = params["id"];
      this.getAll();
    });
  }

  getAll() {
    this.http.post<ProductModel>("ProductDetails/GetAll", { productId: this.productId }, (res) => {
      this.product = res;
    });
  }

}
