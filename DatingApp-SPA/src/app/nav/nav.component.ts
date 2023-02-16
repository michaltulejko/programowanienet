import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(
    public authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    console.log(null);
  }

  login() {
    this.authService.login(this.model).subscribe({
      next: () => this.router.navigateByUrl('/members'),
      error: error => {
        this.toastr.error(error.error.title);
      },
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigateByUrl('/');
  }
}
