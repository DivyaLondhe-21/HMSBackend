import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://localhost:5001/api/values'; // Replace with your API URL

  constructor(private http: HttpClient) { }

  getValues(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
}
