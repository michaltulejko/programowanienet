import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'DatingApp-SPA';
  users: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get('https://localhost:7168/api/User/getusers').subscribe({
      next: response => {
        this.users = response;
      },
      error: err => {
        console.log(err);
      },
      complete: () => console.log('completed'),
    });
  }
}
