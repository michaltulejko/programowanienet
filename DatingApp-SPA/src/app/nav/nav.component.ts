import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(public authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    console.log(null);
  }

  login() {
    this.authService.login(this.model).subscribe({
      next: () => this.router.navigateByUrl('/members'),
      error: error => {
        console.log(error);
      },
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigateByUrl('/');
  }
}
