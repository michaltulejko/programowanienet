import { Component, OnInit } from '@angular/core';
import { UserSession } from './_models/UserSession';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'DatingApp-SPA';
  users: any;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');

    if (!userString) return;

    const user: UserSession = JSON.parse(userString);
    this.authService.setCurrentUser(user);
  }
}
