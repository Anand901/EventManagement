import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetEventActivityComponent } from './get-event-activity.component';

describe('GetEventActivityComponent', () => {
  let component: GetEventActivityComponent;
  let fixture: ComponentFixture<GetEventActivityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GetEventActivityComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetEventActivityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
