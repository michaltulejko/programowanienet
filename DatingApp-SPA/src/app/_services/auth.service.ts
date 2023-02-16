import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { UserSession } from '../_models/UserSession';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baserUrl = 'https://localhost:7168/api/';
  private currentUserSourece = new BehaviorSubject<UserSession | null>(null);
  currentUser$ = this.currentUserSourece.asObservable();

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http
      .post<UserSession>(this.baserUrl + 'auth/login', model)
      .pipe(
        map((response: UserSession) => {
          const user = response;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSourece.next(user);
          }
        })
      );
  }

  setCurrentUser(user: UserSession) {
    this.currentUserSourece.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSourece.next(null);
  }

  register(model: any) {
    return this.http
      .post<UserSession>(this.baserUrl + 'auth/register', model)
      .pipe(
        map(user => {
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSourece.next(user);
          }
        })
      );
  }
}
