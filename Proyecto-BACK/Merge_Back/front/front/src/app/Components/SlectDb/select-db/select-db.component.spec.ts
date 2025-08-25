import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectDbComponent } from './select-db.component';

describe('SelectDbComponent', () => {
  let component: SelectDbComponent;
  let fixture: ComponentFixture<SelectDbComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SelectDbComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectDbComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
