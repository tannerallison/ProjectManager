import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, shareReplay} from "rxjs";
import {User} from "../models/user.model";
import {environment} from "../../environments/environment";
import * as moment from "moment";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private url = 'users';

  constructor(private http: HttpClient) {
  }

  public registerUser(user: any): Observable<User> {
    return this.http
      .post<User>(`${environment.apiUrl}/${this.url}/register`, user);
  }

  public loginUser(user: any): Observable<User> {
    let userObservable = this.http
      .post<User>(`${environment.apiUrl}/${this.url}/authenticate`, user)
      .pipe(shareReplay(1));
    userObservable.subscribe(result => this.setSession(result));
    return userObservable;
  }

  private setSession(authResult: any) {
    const expiresAt = moment().add(authResult.expiresIn, 'second');

    localStorage.setItem('id_token', authResult.token);
    localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()));
  }

  logout() {
    localStorage.removeItem("id_token");
    localStorage.removeItem("expires_at");
  }

  public isLoggedIn() {
    return moment().isBefore(this.getExpiration());
  }

  isLoggedOut() {
    return !this.isLoggedIn();
  }

  getExpiration() {
    const expiration = localStorage.getItem("expires_at");
    if (!expiration) {
      return moment();
    }
    const expiresAt = JSON.parse(expiration);
    return moment(expiresAt);
  }
}
