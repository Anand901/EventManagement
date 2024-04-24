import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { RestApiService } from 'src/Service/rest-api.service';
import { EventDetails } from 'src/model/EventDetails';

@Component({
  selector: 'app-get-event',
  templateUrl: './get-event.component.html',
  styleUrls: ['./get-event.component.css'],
})
export class GetEventComponent implements OnInit {
  result!: EventDetails[];
  imageFormDb!: string;
  constructor(private apiService: RestApiService, private route: Router) {}

  ngOnInit() {
    // method to get the event
    let event = new EventDetails();
    var date = new Date();
    event.Flag = 'Get';
    this.apiService
      .GetEvent(JSON.stringify(event)) // call the service for get event
      .subscribe((data: any) => {
        // metod to get the event page
        if (data != null && data != undefined && data != ' ') {
          if (Object.keys(data.ArrayOfResponse[0]).length != 0) {
            console.log(data.Message);
            console.log(data);
            console.log('this is javascript');
            this.result = data.ArrayOfResponse;
          } else {
            console.log('Something went wrong!');
          }
        } else {
          console.log('data is not present or not in correct fromat!');
        }
      });
  }

  ViewMore(
    Id: number,
    Name: string,
    StartDate: string,
    EndDate: string,
    EventImage: string
  ) {
    // event bindig for to click on the veiw more
    this.apiService.eventIdforActivity = Id;
    this.apiService.eventNameforActivity = Name;
    this.apiService.eventSatrtDateforActivity = StartDate;
    this.apiService.eventEndDateActivity = EndDate;
    this.apiService.eventImageforActivity = EndDate;
    this.apiService.eventImageforActivity = EventImage;
    this.route.navigateByUrl('GetEventActivity'); // navigatet to the event activity get page
  }
}
