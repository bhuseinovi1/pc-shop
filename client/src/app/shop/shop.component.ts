import { Component } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent {
  products: IProduct[];
  brands: IBrand[];
  types: IType[];

  constructor(private shopService: ShopService) {}

  ngOnInit() {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts().subscribe({
      next: (p) => {
        this.products = p.data;
      },
      error: (e) => console.log(e),
      complete: () => console.info('complete')
    }
    )
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: (p) => {
        this.brands = p;
      },
      error: (e) => console.log(e),
      complete: () => console.info('complete')
    }
    )
  }

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: (p) => {
        this.types = p;
      },
      error: (e) => console.log(e),
      complete: () => console.info('complete')
    }
    )
  }

}
