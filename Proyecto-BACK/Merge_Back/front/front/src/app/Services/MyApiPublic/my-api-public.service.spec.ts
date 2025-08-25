import { TestBed } from '@angular/core/testing';

import { MyApiPublicService } from './my-api-public.service';

describe('MyApiPublicService', () => {
  let service: MyApiPublicService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MyApiPublicService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
