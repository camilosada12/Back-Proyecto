import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApiPublicComponent } from './api-public.component';

describe('ApiPublicComponent', () => {
  let component: ApiPublicComponent;
  let fixture: ComponentFixture<ApiPublicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApiPublicComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApiPublicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
