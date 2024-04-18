import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserLogInComponent } from './user-log-in/user-log-in.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { UserRegisterComponent } from './user-register/user-register.component';
import { EventDetailsComponent } from './event-details/event-details.component';
import { EventActivityAddComponent } from './event-activity-add/event-activity-add.component';
import { GetEventComponent } from './get-event/get-event.component';
import { GetEventActivityComponent } from './get-event-activity/get-event-activity.component';
import { PublishEventComponent } from './publish-event/publish-event.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AddPriceComponent } from './add-price/add-price.component';



@NgModule({
  declarations: [
    AppComponent,
    UserLogInComponent,
    UserRegisterComponent,
    EventDetailsComponent,
    EventActivityAddComponent,
    GetEventComponent,
    GetEventActivityComponent,
    PublishEventComponent,
    UserDashboardComponent,
    AdminDashboardComponent,
    AddPriceComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
   
  ],
  providers: [],
  bootstrap: [AppComponent] 
})
export class AppModule { }
