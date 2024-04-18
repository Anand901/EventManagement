import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { RestApiService } from 'src/Service/rest-api.service';
import { EventDetails } from 'src/model/EventDetails';

@Component({
  selector: 'app-event-details',
  templateUrl: './event-details.component.html',
  styleUrls: ['./event-details.component.css'],
})
export class EventDetailsComponent implements OnInit {
  constructor(private fb: FormBuilder, private apiservice: RestApiService) {
    // set the form controlls
    this.EventDetails = this.fb.group({
      EventName: new FormControl('', [Validators.required]),
      EventDescription: new FormControl('', Validators.required),
      EventStartDate: new FormControl('', Validators.required),
      EventEndDate: new FormControl('', Validators.required),
      EventImage: new FormControl('', Validators.required),
    });
  }
  // decalre the necessary variables
  EventDetails!: FormGroup;
  fileName!: any;
  result!: string;
  AdminEmail = sessionStorage.getItem('AdminEmail'); // get the admin email from the session storage

  ngOnInit() {}

  get getEvenetDetailsControl() {
    // return the form controlls
    return this.EventDetails.controls;
  }

  onAddEvent() {
    // method to add the event details
    if (this.EventDetails.valid) {
      console.log(this.EventDetails.value);
      const formData: FormData = new FormData(); // create the instance of the form data
      formData.append('Flag', 'Insert');
      formData.append('EventName', this.EventDetails.value.EventName);
      formData.append(
        'EventDescription',
        this.EventDetails.value.EventDescription
      );
      formData.append('EventStartDate', this.EventDetails.value.EventStartDate);
      formData.append('EventEndDate', this.EventDetails.value.EventEndDate);
      formData.append('EventImage', this.fileName);
      formData.append('AdminEmail', this.AdminEmail!);

      this.apiservice
        .AddEvent(formData) // call the sevice to add the event
        .subscribe((data: any) => {
          if (data != null && data != undefined && data != ' ') {
            this.result = data.Message;
          } else {
            console.log('data is not in desired format');
          }
          console.log(data);
        });
    } else {
      this.result = 'form is invalid';
    }
  }

  useImage(event: any) {
    // handle the image
    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]); // Read file as data url
      reader.onloadend = (e) => {
        // function call once readAsDataUrl is completed
        this.fileName = event.target.files[0]; // Set image in element
        // Is called because ChangeDetection is set to onPush
      };
    }
  }
}
