import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminGaurdService {

  constructor(private router: Router) { }


  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    let x = sessionStorage.getItem('isAdminLogIn')?.toString();    // store the result to the variable
    if (x == 'true') {
      return true;
    } else {
      this.router.navigateByUrl('LogIn');   // navigate to the log in page
      return false;
    }
  }
}
