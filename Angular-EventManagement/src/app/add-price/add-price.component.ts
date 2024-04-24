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
  selector: 'app-add-price',
  templateUrl: './add-price.component.html',
  styleUrls: ['./add-price.component.css'],
})
export class AddPriceComponent implements OnInit {
  eventNames!: EventDetails[];
  eventActivities!: EventActivity[];
  result!: string
  submmited = false

  EventActivityPrice!: FormGroup;
  constructor(private fb: FormBuilder, private apiservice: RestApiService) {
    this.EventActivityPrice = this.fb.group({
      ActivityName: new FormControl(''),
      ActivityPrice: new FormControl('', [Validators.required]),
      EventName: new FormControl(''),
      EventActivity: new FormControl('', [Validators.required]),
      EventId: new FormControl('', [Validators.required])
    });
  }

  ngOnInit() { 
    // method for get the event names which are not published 
    let event = new EventDetails();
    event.Flag = 'GetEventName';
    this.apiservice
      .getEventName(JSON.stringify(event))       // call the service for get the event name 
      .subscribe((data: any) => {
        console.log(data);
        if (data != null && data != undefined && data != '') {
          this.eventNames = data.ArrayOfResponse;        // save the response from the server in the variable
          console.log(data.ArrayOfResponse[0].EventName);
        } else {
          console.log('Something went wrong!');
        }
      });
  }
  getEventActivities() {
     // method to get the event activities of the particular event 
    let eventActivity = new EventActivity();
    eventActivity.Flag = 'GetEventActivities';
    eventActivity.EventId = this.EventActivityPrice.value.EventId;
    this.apiservice
      .getActivity(JSON.stringify(eventActivity))      // call the service to get the event activity
      .subscribe((data: any) => {
        // alert(data.Message)
        if (data != null && data != undefined && data != ' ') {
          console.log(data);
          this.eventActivities = data.ArrayOfResponse;
        } else {
          console.log('Something went wrong!');
        }
      });
  }

  AddPrice() {
   // method to add the (update the price) of the particular event activity
   debugger
   this.submmited = true
    if (this.EventActivityPrice.valid) {
      let eventActivity = new EventActivity();
      eventActivity.Flag = 'Update';
      eventActivity.ActivityName = this.EventActivityPrice.value.EventActivity;
      eventActivity.ActivityPrice = this.EventActivityPrice.value.ActivityPrice;
      this.apiservice
        .updatePrice(JSON.stringify(eventActivity))
        .subscribe((data: any) => {
          if (data != null && data != undefined && data != " ") {
      this.result = data.Message
          }else{
            console.log("something went wrong!");
            
          }
        });
    } else {
     return;
    }
  }
  get EventActivityPriceControl() {      // return the controls of the form
    return this.EventActivityPrice.controls;
  }
}
