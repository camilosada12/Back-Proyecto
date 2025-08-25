import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Attraction, AttractionCreate } from '../ApiPublic/api-public.service';
import { GenericService } from '../generic/generic-service.service';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MyApiPublicService {

  // Ruta base del microservicio ExternalDataService a través del Gateway
  private backendApiUrl = environment.apiUrl + '/external-data/api/Data';

  constructor(private http: HttpClient) { }

  // Mi Servicios 

  getAll(): Observable<Attraction[]> {
    return this.http.get<Attraction[]>(`${this.backendApiUrl}`);
  }
    // Enviar datos seleccionados al microservicio (POST /api/external-data/Data/import)
  create(attractions: Attraction[]): Observable<any> {
    return this.http.post(`${this.backendApiUrl}/import`, attractions);
  }

  update(id: number, attraction: Attraction): Observable<any> {
    return this.http.put(`${this.backendApiUrl}/${id}`, attraction);
  }


  public delete(id: number): Observable<any> {
    return this.http.delete(`${this.backendApiUrl}/${id}`);
  }

}
