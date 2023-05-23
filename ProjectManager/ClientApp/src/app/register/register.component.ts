import {Component} from '@angular/core';
import {RegisterUser} from "../models/register-user.model";
import {UserService} from "../services/user.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  protected registerData: RegisterUser = {username: '', password: '', confirmPassword: ''};

  constructor(private userService: UserService) {

  }

  register() {
    if (this.registerData.password !== this.registerData.confirmPassword) {
      console.log('Passwords do not match!');
      return;
    }

    this.userService.registerUser(this.registerData)
      .subscribe({
        next(data: any) {
          localStorage.setItem('token', data.token);
          localStorage.setItem('username', data.username);
        }, error(err: any) {
          console.log(err);
        }
      });
  }
}
