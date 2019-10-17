import { TestBed } from '@angular/core/testing';

import { NotifBarService } from './notif-bar.service';

describe('NotifBarService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: NotifBarService = TestBed.get(NotifBarService);
    expect(service).toBeTruthy();
  });
});
