import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CdbSimulationRequest } from '../../features/cdb-simulation/models/cdb-simulation-request';
import { CdbSimulationResponse } from '../../features/cdb-simulation/models/cdb-simulation-response';
import { environment } from '../../../environments/environments';

@Injectable({
  providedIn: 'root'
})
export class CdbSimulationService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/investments/cdb/simulations`;

  simulate(request: CdbSimulationRequest): Observable<CdbSimulationResponse> {
    return this.http.post<CdbSimulationResponse>(this.apiUrl, request);
  }
}