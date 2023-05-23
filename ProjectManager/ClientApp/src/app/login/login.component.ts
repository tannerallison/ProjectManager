import {Component} from '@angular/core';
import {LoginUser} from "../models/login-user.model";
import {UserService} from "../services/user.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  protected loginData: LoginUser = {username: '', password: ''};

  constructor(private userService: UserService) {
  }

  login() {
    this.userService.loginUser(this.loginData)
      .subscribe({
        next(data: any) {
          localStorage.setItem('token', data.token);
          localStorage.setItem('username', data.username);
        }, error(msg) {
          console.log(msg);
        }
      });
  }
}
