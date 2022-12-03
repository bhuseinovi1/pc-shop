import { Component } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
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
  shopParams = new ShopParams();
  totalCount: number;
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'}
  ]

  constructor(private shopService: ShopService) {}

  ngOnInit() {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (p) => {
        this.products = p.data;
        this.shopParams.pageNumber = p.pageIndex;
        this.shopParams.pageSize = p.pageSize;
        this.totalCount = p.count;
      },
      error: (e) => console.log(e),
      complete: () => console.info('complete')
    }
    )
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: (p) => {
        this.brands = [{id:0, name: 'All'},...p];
      },
      error: (e) => console.log(e),
      complete: () => console.info('complete')
    }
    )
  }

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: (p) => {
        this.types = [{id:0, name: 'All'},...p];
      },
      error: (e) => console.log(e),
      complete: () => console.info('complete')
    }
    )
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.getProducts();
  }

  onSortSelected(target: EventTarget) {
    //
    const sort = target as HTMLInputElement;

    this.shopParams.sort = sort.value;
    this.getProducts();
  }

  onPageChanged(event : any) {
    this.shopParams.pageNumber = event.page;
    this.getProducts();
  }
}
