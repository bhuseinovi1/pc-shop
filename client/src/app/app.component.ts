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

  constructor() {}

  ngOnInit(): void {
    /* this.http.get('https://localhost:5209/api/products?pageSize=50').subscribe((response: any) => {
      this.products = response.data;
    }, error => {
      console.log(error);
    }) */
  }
}
