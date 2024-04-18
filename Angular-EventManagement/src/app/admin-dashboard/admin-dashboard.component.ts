import { Component } from '@angular/core';
import { Router, RouterLinkWithHref } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
 
  constructor(private router: Router){}
  onLogOut(){
    // methos for logout the admin
    sessionStorage.clear();
    this.router.navigateByUrl('/LogIn')  // navigate to the log in page
  }
}
