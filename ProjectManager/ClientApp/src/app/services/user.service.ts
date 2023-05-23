import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {User} from "../models/user.model";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private url = 'users';

  constructor(private http: HttpClient) {
  }

  public registerUser(user: any): Observable<User> {
    return this.http.post<User>(`${environment.apiUrl}/${this.url}/register`, user);
  }

  public loginUser(user: any): Observable<User> {
    return this.http.post<User>(`${environment.apiUrl}/${this.url}/authenticate`, user);
  }

}
