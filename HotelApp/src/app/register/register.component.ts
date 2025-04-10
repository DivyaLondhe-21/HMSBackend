import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  user = {
    email: '',
    password: '',
    role: ''
  };
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private router: Router) { }

  onRegister() {
    this.authService.register(this.user).subscribe(
      response => {
        console.log('Registration successful:', response);
        this.router.navigate(['/login']);  // Redirect to login page after successful registration
      },
      error => {
        console.error('Registration failed:', error);
        this.errorMessage = 'Registration failed. Please try again later.';
      }
    );
  }
}
