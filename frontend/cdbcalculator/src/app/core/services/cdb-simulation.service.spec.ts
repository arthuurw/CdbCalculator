import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { CdbSimulationService } from './cdb-simulation.service';
import { CdbSimulationResponse } from '../../features/cdb-simulation/models/cdb-simulation-response';
import { environment } from '../../../environments/environments';

const API_URL = `${environment.apiUrl}/investments/cdb/simulations`;

describe('CdbSimulationService', () => {
  let service: CdbSimulationService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        CdbSimulationService
      ]
    });
    service = TestBed.inject(CdbSimulationService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('simulate()', () => {
    it('should POST to the correct URL', () => {
      const request = { initialAmount: 1000, months: 6 };

      service.simulate(request).subscribe();

      const req = httpMock.expectOne(API_URL);
      expect(req.request.method).toBe('POST');
      req.flush({ grossAmount: 1100, netAmount: 1080 });
    });

    it('should send the correct request body', () => {
      const request = { initialAmount: 5000, months: 12 };

      service.simulate(request).subscribe();

      const req = httpMock.expectOne(API_URL);
      expect(req.request.body).toEqual(request);
      req.flush({ grossAmount: 5500, netAmount: 5400 });
    });

    it('should return the response from the API', () => {
      const request = { initialAmount: 1000, months: 6 };
      const mockResponse = { grossAmount: 1100, netAmount: 1080 };
      let result: CdbSimulationResponse | undefined;

      service.simulate(request).subscribe(res => (result = res));

      const req = httpMock.expectOne(API_URL);
      req.flush(mockResponse);

      expect(result).toEqual(mockResponse);
    });
  });
});
