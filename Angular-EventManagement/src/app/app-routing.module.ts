import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserLogInComponent } from './user-log-in/user-log-in.component';
import { UserRegisterComponent } from './user-register/user-register.component';
import { AuthGuard } from 'src/Service/auth-gaurd.service';
import { EventDetailsComponent } from './event-details/event-details.component';
import { EventActivityAddComponent } from './event-activity-add/event-activity-add.component';
import { GetEventComponent } from './get-event/get-event.component';
import { GetEventActivityComponent } from './get-event-activity/get-event-activity.component';
import { PublishEventComponent } from './publish-event/publish-event.component';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AdminGaurdService } from 'src/Service/admin-gaurd.service';
import { AddPriceComponent } from './add-price/add-price.component';


const routes: Routes = [
  {
    path:'LogIn',
    component:UserLogInComponent,
    title:'EventManagementLogInPage'
  },
  {
    path:'Register',
    component:UserRegisterComponent,
    title:'UserRegisterPage'
  },
  {
    path:'EventPage',
    component:EventDetailsComponent,
    title:'EventPage',
    canActivate:[AdminGaurdService]
  },
  {
    path:'EventActivity',
    component:EventActivityAddComponent,
    title:'EventActivityPage',
    canActivate:[AdminGaurdService]
  },
  {
    path:'GetEvent',
    component:GetEventComponent,
    title:'EventGetPage',
    canActivate:[AuthGuard]
  },
  {
    path:'GetEventActivity',
    component:GetEventActivityComponent,
    title:'GetEventActivityPage',
    canActivate:[AuthGuard]
  },
  {
    path:'PublishEvent',
    component:PublishEventComponent,
    title:'PublishEventPage',
    canActivate:[AdminGaurdService]
  },
  {
    path:'UserDashboard',
    component:UserDashboardComponent,
    title:'UserDashboardPage',
    canActivate:[AuthGuard]
  },
  {
    path:'AdminDashboard',
    component:AdminDashboardComponent,
    title:'AdminDashboardPage',
    canActivate:[AdminGaurdService]
  },
  {
    path:'ActivityPrice',
    component:AddPriceComponent,
    title:'ActivityPricePage',
    canActivate:[AdminGaurdService]
  }
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
