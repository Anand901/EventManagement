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
  selector: 'app-get-event-activity',
  templateUrl: './get-event-activity.component.html',
  styleUrls: ['./get-event-activity.component.css'],
})
export class GetEventActivityComponent implements OnInit {
  // declare the necessary variables
  eventNames!: EventDetails[];
  result!: EventActivity[];
  imageFormDb!: string;
  GetEventActivity!: FormGroup;
  constructor(public apiservice: RestApiService, private fb: FormBuilder) {
    this.GetEventActivity = this.fb.group({
      // declare the control of the form
      ActivityName: new FormControl('', [Validators.required]),
      ActivityDescription: new FormControl('', Validators.required),
      ActivityStartTime: new FormControl('', Validators.required),
      ActivityEndTime: new FormControl('', Validators.required),
      ActivityPrice: new FormControl('', Validators.required),
      EventName: new FormControl('', Validators.required),
      EventId: new FormControl('', Validators.required),
    });
  }

  eventActivity = new EventActivity();
  ngOnInit() {
    // method to get the event activity
    this.eventActivity.Flag = 'Get';
    this.eventActivity.EventId = this.apiservice.eventIdforActivity;
    this.imageFormDb = this.apiservice.eventImageforActivity!;
    this.apiservice
      .getActivity(JSON.stringify(this.eventActivity)) // call the service to get the event activity
      .subscribe((data: any) => {
        console.log(data);
        if (
          data != null &&
          data != undefined &&
          data != '' &&
          data.ArrayOfResponse.length > 0
        ) {
          this.result = data.ArrayOfResponse; // store the result from the response of the server
        } else {
          console.log('Something went wrong!');
        }
      });
  }
}
