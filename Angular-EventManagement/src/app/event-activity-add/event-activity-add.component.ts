import { JsonPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';

import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { RestApiService } from 'src/Service/rest-api.service';
import { EventActivity } from 'src/model/EventActivity';
import { EventDetails } from 'src/model/EventDetails';

@Component({
  selector: 'app-event-activity-add',
  templateUrl: './event-activity-add.component.html',
  styleUrls: ['./event-activity-add.component.css'],
})
export class EventActivityAddComponent implements OnInit {
  // derclar the necessary variables
  eventNames!: EventDetails[];
  EventActivity!: FormGroup;
  EventStartDate!: string;
  EventEndDate!: string;
  result!: string
  constructor(private fb: FormBuilder, private apiservice: RestApiService) {
    this.EventActivity = this.fb.group({
      // declare the form controlls
      ActivityName: new FormControl('', [
        Validators.required
      ]),
      ActivityDescription: new FormControl(''),
      ActivityStartTime: new FormControl(''),
      ActivityEndTime: new FormControl(''),
      ActivityPrice: new FormControl(''),
      EventName: new FormControl(''),
      EventId: new FormControl(''), 
    });
  }

  get EventActivityControl() {
    // return form controlls
    return this.EventActivity.controls;
  }

  ngOnInit() {
    // method to get the event names
    let event = new EventDetails();
    event.Flag = 'GetEventName';
    this.apiservice
      .getEventName(JSON.stringify(event))    // call the service to get the event names
      .subscribe((data: any) => {
        console.log(data);
        if (data != null && data != undefined && data != " ") {
          this.eventNames = data.ArrayOfResponse;
          console.log(data.ArrayOfResponse[0].EventName);
        } else {
          console.log('Something went wrong!');
        }
      });
  }

  AddActivity() {
  // method to add the event activity add 
    if(this.EventActivity.valid){
      let activity = new EventActivity();    // create the instance of the event activity
    activity.ActivityName = this.EventActivity.value.ActivityName;
    activity.ActivityDescription = this.EventActivity.value.ActivityDescription;
    activity.ActivityStartTime = this.EventActivity.value.ActivityStartTime;
    activity.ActivityEndTime = this.EventActivity.value.ActivityEndTime;
    activity.ActivityPrice = this.EventActivity.value.ActivityPrice;
    activity.Flag = 'Insert';
    activity.EventName = this.EventActivity.value.EventName;
    activity.EventId = 0;

    this.apiservice
      .AddEventActivity(JSON.stringify(activity))    // call the service to add the event activity
      .subscribe((data: any) => {
        console.log(data);
        if (data != null && data != undefined && data != " ") {
       this.result = data.Message;
        }else{
          console.log("something went wrong");
          
        }
      });
    }else{
      this.result = "form is invalid"
    }
  }

  getEventDate() {
    // method to get the event date
    let event1 = new EventDetails();
    event1.Flag = 'GetEventDate';
    event1.EventName = this.EventActivity.value.EventName;

    this.apiservice
      .getEventDate(JSON.stringify(event1))    // calls the service to get the event activty
      .subscribe((data: any) => {
        alert(data.Message);
        console.log(data);
        if (data != null && data != undefined && data != " ") {
        this.EventStartDate = data.ArrayOfResponse[0].EventStartDate ;
        this.EventEndDate = data.ArrayOfResponse[0].EventEndDate;
        }else{
          console.log("somrthing went wrong");
          
        }
      });
  }
}
