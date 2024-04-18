import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventActivityAddComponent } from './event-activity-add.component';

describe('EventActivityAddComponent', () => {
  let component: EventActivityAddComponent;
  let fixture: ComponentFixture<EventActivityAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EventActivityAddComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EventActivityAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
