import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ApiService } from './api.service';
import { NgModule } from '@angular/core'; }

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NgModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'HotelApp';
    values: any;

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.apiService.getValues().subscribe(data => {
      this.values = data;
    });
  }
}
