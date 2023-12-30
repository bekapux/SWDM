import { HttpClient } from '@angular/common/http';
import { Injectable, Signal, signal } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable, catchError, tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public isAuthenticated = signal(false);
  
  constructor(private http: HttpClient, private router: Router) { }

  public checkAuthStatus(): Observable<void>{
    return this.http.get<void>(`${environment.webapi}/auth/is-authenticated`);
  }

  public logOut(): Observable<void>{
    return this.http.post<void>(`${environment.webapi}/auth/logout`, {});
  }
}
