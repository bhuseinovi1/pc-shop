import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './shared/models/pagination';
import { IProduct } from './shared/models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'PC SHOP';
  products: IProduct[] | undefined;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    /* this.http.get('https://localhost:5209/api/products?pageSize=50').subscribe((response: any) => {
      this.products = response.data;
    }, error => {
      console.log(error);
    }) */
    this.http.get('https://localhost:5209/api/products?pageSize=50').subscribe({
      next: (p : IPagination) => {
        this.products = p.data;
      },
      error: (e) => console.log(e),
      complete: () => console.info('complete')
    }
    )
  }
}
