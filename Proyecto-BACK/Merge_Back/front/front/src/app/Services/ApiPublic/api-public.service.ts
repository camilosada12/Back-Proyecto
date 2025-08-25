import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Attraction {
  id: number;
  name: string;
  description: string;
}
export interface AttractionCreate {
  name: string;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class ApiPublicService {
  // Ruta base del microservicio ExternalDataService a través del Gateway
  private backendApiUrl = environment.apiUrl + '/external-data/api/Data';

  constructor(private http: HttpClient) { }

  // Obtener datos desde el microservicio (GET /api/external-data/Data/external)
  getPublicAttractions(): Observable<Attraction[]> {
    return this.http.get<Attraction[]>(`${this.backendApiUrl}/external`);
  }

  // Enviar datos seleccionados al microservicio (POST /api/external-data/Data/import)
  saveAttractions(attractions: Attraction[]): Observable<any> {
    return this.http.post(`${this.backendApiUrl}/import`, attractions);
  }
 
}
