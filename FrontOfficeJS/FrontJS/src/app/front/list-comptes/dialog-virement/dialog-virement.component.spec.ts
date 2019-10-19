import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogVirementComponent } from './dialog-virement.component';

describe('DialogVirementComponent', () => {
  let component: DialogVirementComponent;
  let fixture: ComponentFixture<DialogVirementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogVirementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogVirementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
