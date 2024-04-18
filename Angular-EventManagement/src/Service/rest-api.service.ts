import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RestApiService {
  constructor(private http: HttpClient) {}
 
  eventIdforActivity?:number;
  eventNameforActivity?:string;
  eventSatrtDateforActivity?:string;
  eventEndDateActivity?:string;
  eventImageforActivity?:string

  httpOptions = { 
    // declare the http hraders 
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Accept: 'application/json',
    }),
  };

  baseUrl = 'http://localhost:44380/api/';     // declare the base url of the api 

  RegisterUser(body: any): Observable<any> { 
    // service for the register user api
    const endPoint = 'User/RegisterUser';
    return this.http.post(this.baseUrl + endPoint, body, this.httpOptions);
  }

  LogInUser(body: any): Observable<any> {
     // service for the log in user api
    const endPoint = 'User/LogInUser';
    return this.http.post(this.baseUrl + endPoint, body, this.httpOptions);
  }

  LogInAdmin(body: any): Observable<any> {
    // service for the logina admin api
    const endPoint = 'Admin/LogInAdmin';
    return this.http.post(this.baseUrl + endPoint, body, this.httpOptions);
  }

  AddEvent(body: FormData): Observable<any> { // give the type form data to the body 
   // service to add the event api
    const endPoint = 'Event/AddEvent';
    return this.http.post(this.baseUrl + endPoint, body); //, this.httpOptions
  }

  GetEvent(body: any): Observable<any> {
    // service to get the event api
    const endPoint = 'Event/GetEvent';
    return this.http.post(this.baseUrl + endPoint, body, this.httpOptions); //, this.httpOptions
  }

    AddEventActivity(body: any): Observable<any> {
      // service to add the event activity api
    const endPoint = 'EventActivity/AddActivity';
    return this.http.post(this.baseUrl + endPoint, body, this.httpOptions);
  }

  getEventName(body: any): Observable<any> {
    // service to get the event name api
    const endPoint = 'Event/GetEventName';
    return this.http.post(this.baseUrl + endPoint, body, this.httpOptions);
  }

  getActivity(body: any): Observable<any> {
    // service to get the event activity api
    const endPoint = 'EventActivity/GetActivity';
    return this.http.post(this.baseUrl + endPoint, body, this.httpOptions);
  }

  publishEvent(body: any): Observable<any>{
    // service to publish the evevnt api
    const endPoint = 'Event/PublishEvent';
    return this.http.post(this.baseUrl + endPoint, body, this.httpOptions);
  }

  getEventDate(body:any):Observable<any>{
    // service to get  the event date
    const endPoint = 'Event/GetEventDate';
    return this.http.post(this.baseUrl + endPoint, body, this.httpOptions);
  }

  updatePrice(body:any):Observable<any>{
    // service to update the price of the event activity api
    const endPoint = 'EventActivity/GetActivity';
    return this.http.post(this.baseUrl + endPoint, body, this.httpOptions);
  }
}
