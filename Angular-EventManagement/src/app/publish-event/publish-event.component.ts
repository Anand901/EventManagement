import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupName, Validators } from '@angular/forms';
import { RestApiService } from 'src/Service/rest-api.service';
import { EventDetails } from 'src/model/EventDetails';

@Component({
  selector: 'app-publish-event',
  templateUrl: './publish-event.component.html',
  styleUrls: ['./publish-event.component.css']
})
export class PublishEventComponent implements OnInit {
  // declartion of the necessary variables
  PublishEvent! : FormGroup
  eventNames!:EventDetails[]
  result!: string
  constructor(private fb: FormBuilder, private apiservice: RestApiService){
    this.PublishEvent = this.fb.group({
      EventName: new FormControl('', [
        Validators.required
      ]),
   
  })
}
ngOnInit(): void {
  
}
getEventName(){
  // method to get the event names
  let event = new EventDetails();
  event.Flag = 'GetEventName';
  this.apiservice
    .getEventName(JSON.stringify(event))     // call the service to get the event name
    .subscribe((data: any) => {
      console.log(data);
      if (data != null && data != undefined) {
        this.eventNames = data.ArrayOfResponse;
        console.log(data.ArrayOfResponse[0].EventName)
      } else {
        console.log('Something went wrong!');
      }
    });
  
}

Publish_Event(){
  // method to publish the event 
  if(this.PublishEvent.valid){    // check is the form is valid as per form controll declartion
let Event = new EventDetails();
Event.Flag = 'Update'
Event.EventId=this.PublishEvent.value.EventId
console.log(this.PublishEvent.value.EventId)
this.apiservice
.publishEvent(JSON.stringify(Event))    //calls the service to publish the event and call the api 
.subscribe((data:any)=>{
 this.result = data.Message;  // save the response of the servet in the variable
  console.log(data)
})
}else{
  this.result = "form is invalid"
}

}
  
}
