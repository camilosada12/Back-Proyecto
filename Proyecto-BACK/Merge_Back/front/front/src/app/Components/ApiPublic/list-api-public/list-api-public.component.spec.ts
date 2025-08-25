import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListApiPublicComponent } from './list-api-public.component';

describe('ListApiPublicComponent', () => {
  let component: ListApiPublicComponent;
  let fixture: ComponentFixture<ListApiPublicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListApiPublicComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListApiPublicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
