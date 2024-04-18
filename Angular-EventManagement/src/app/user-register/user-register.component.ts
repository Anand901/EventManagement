import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { RestApiService } from 'src/Service/rest-api.service';
import { User } from 'src/model/User';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css'],
})
export class UserRegisterComponent implements OnInit {
  UserRegisterForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private apiService: RestApiService,
    private http: HttpClient
  ) {}

  ngOnInit() { 
    // decalre the form controlls
    this.UserRegisterForm = this.fb.group({
      UserName: new FormControl('', [Validators.required]),
      UserMobile: new FormControl('', Validators.required),
      UserAdress: new FormControl('', Validators.required),
      UserEmail: new FormControl('', [Validators.required, Validators.email]),
      UserPassword: new FormControl('', Validators.required),
    });
  }

 get getRegisterFormControl() {
  // rturn the form controlls
    return this.UserRegisterForm.controls;
  }

  onRegister() {
    // method to call the register api or register the user 
    console.log(this.UserRegisterForm.value);
    if(this.UserRegisterForm.valid){
    let user = new User();
    user.Flag= 'Register'
    // set the form detais to the body of the api
    user.UserName= this.UserRegisterForm.value.UserName
    user.UserEmail= this.UserRegisterForm.value.UserEmail
    user.UserAdress= this.UserRegisterForm.value.UserAdress
    user.UserPassword= this.UserRegisterForm.value.UserPassword
    user.UserPhoneNo = this.UserRegisterForm.value.UserMobile
  
    this.apiService
    .RegisterUser( JSON.stringify(user))          // call the service for register the user
    .subscribe((data:any)=>{
     alert(data.Message)
     console.log(data);
    })
  } else{
    alert("Form is invalid")
  }
    }

    vallidName(event :any){     // validation for the name
      var k; 
      k= event.charCode;
      return ((k>64 && k<91) || (k>96 && k<123) || k==8 )
    }

    vallidMobile(event :any){     // validation fo rthe mobile number
      var k;
      k= event.charCode;
      return ((k>47 && k<58) || k==8 )
    }
  }

