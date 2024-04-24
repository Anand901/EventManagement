import { HttpClient } from '@angular/common/http';
import { VariableBinding } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { RestApiService } from 'src/Service/rest-api.service';
import { Admin } from 'src/model/Admin';
import { User } from 'src/model/User';

@Component({
  selector: 'app-user-log-in',
  templateUrl: './user-log-in.component.html',
  styleUrls: ['./user-log-in.component.css'],
  providers: [RestApiService],
})
export class UserLogInComponent implements OnInit {
  //declaration of the necessary variables
  UserLogIn!: FormGroup;
  SelectedRole!: string;
  email!: string;
  isAdminLogIn!: boolean;
  isUserLogIn!: boolean;
  AdminEmail!: string;
  result!: string;
  submitted = false;

  constructor(
    private fb: FormBuilder,
    private apiService: RestApiService,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit() {
    // form controlls declaration
    this.UserLogIn = this.fb.group({
      LogInType: new FormControl('', [Validators.required]),
      Email: new FormControl('', [Validators.required, Validators.email]),
      Password: new FormControl('', Validators.required),
    });
  }

  get userloginControl() {
    // return the form controlls
    return this.UserLogIn.controls;
  }

  OnLogIn() {
    this.submitted = true;
    // check if the login type is user then do the user login
    if (this.UserLogIn.value.LogInType == 'User') {
      if (this.UserLogIn.invalid) {
        return;
      } else {
        console.log(this.UserLogIn.value);
        let user = new User();
        user.UserEmail = this.UserLogIn.value.Email;
        user.UserPassword = this.UserLogIn.value.Password;
        user.Flag = 'LogIn';
        this.apiService
          .LogInUser(JSON.stringify(user)) // call the servie for user login
          .subscribe(
            (data: any) => {
              console.log(data);
              if (data != null && data != undefined && data != '') {
                // check if the response is not empty
                console.log('this is javascript');
                this.result = data.Message;
                this.email = data.ArrayOfResponse[0].UserEmail;
                console.log(this.email);
                if (data.Code == '200') {
                  this.isUserLogIn = true;
                  this.isAdminLogIn = false;
                  sessionStorage.setItem('isUserLogIn', 'true');
                  sessionStorage.setItem('isAdminLogin', 'false');
                  sessionStorage.setItem('UserEmail', user.UserEmail); // store the user email in the session storage
                  this.router.navigateByUrl('/UserDashboard'); // navigate to the dashboard of the user
                } else {
                  sessionStorage.setItem('isUserLogIn', 'false');
                }
              } else {
                console.log('data is not in corret format');
              }
            },
            (error) => {
              console.log(error);
            }
          );
      }
    } else if (this.UserLogIn.value.LogInType == 'Admin') {
      // check if the login type is user then do the admin login
      if (this.UserLogIn.invalid) {
        return;
      } else {
        console.log(this.UserLogIn.value);
        let admin = new Admin();
        admin.AdminEmail = this.UserLogIn.value.Email;
        admin.AdminPassword = this.UserLogIn.value.Password;
        admin.Flag = 'LogIn';
        console.log(admin);
        this.apiService
          .LogInAdmin(JSON.stringify(admin)) // calls the service of the admin lpgin
          .subscribe(
            (data: any) => {
              console.log(data);
              this.result = data.Message;
              if (data != '' && data != null && data != undefined) {
                console.log('this is javascript');
                this.AdminEmail = data.ArrayOfResponse[0].AdminEmail; // save the response from the serve into the variable
                console.log(this.AdminEmail);
              if (data.Code == '200') {
                this.isAdminLogIn = true;
                this.isUserLogIn = false;
                sessionStorage.setItem('isAdminLogIn', 'true'); // store the variable in session storage
                sessionStorage.setItem('isUserLogIn', 'false');
                sessionStorage.setItem('AdminEmail', admin.AdminEmail); // store the admin email in session storage
                this.router.navigateByUrl('/AdminDashboard'); // navigate to the dashboard to the admin
              } else {
                sessionStorage.setItem('isAdminLogIn', 'false');
              }
            }else{
              console.log("response from server  is invalid")
            }},
            (error) => {
              console.log(error);
            }
          );
      }
    } else {
      alert('plesase select the login type');
    }
  }
}
