// src/app/login/login.component.ts
import { Component } from '@angular/core';
import { AuthService } from '../auth/auth.service';  // Ensure you have the AuthService
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  credentials = {
    email: '',
    password: ''
  };
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private router: Router) { }

  onLogin() {
    this.authService.login(this.credentials).subscribe(
      response => {
        console.log('Login successful:', response);
        this.router.navigate(['/dashboard']);  // Redirect to dashboard after successful login
      },
      error => {
        console.error('Login failed:', error);
        this.errorMessage = 'Login failed. Please check your credentials.';
      }
    );
  }
}
