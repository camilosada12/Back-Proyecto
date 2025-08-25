import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Rol, RolCreate } from '../../Models/Rol/rol.models';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { GenericService } from '../generic/generic-service.service';

@Injectable({
  providedIn: 'root'
})
export class RolService extends GenericService<Rol, RolCreate> {

  constructor(http: HttpClient) {
    super(http, 'rol');
  }
  // private http = inject(HttpClient);
  // private URLBase = environment.apiUrl + '/rol';


  // public getAll(): Observable<Rol[]> {
  //   return this.http.get<Rol[]>(this.URLBase);
  // }

  public getAllDelete(): Observable<Rol[]> {
    return this.http.get<Rol[]>(this.endpoint + "?GetAllType=1");
  }

  public logicalDelete(id: number): Observable<any> {
    return this.http.delete(`${this.endpoint}/${id}?deleteType=0`, {});
  }

  public logicalRestore(id: number): Observable<any> {
    return this.http.patch(`${this.endpoint}/logical-restore/${id}`, {});
  }
}
