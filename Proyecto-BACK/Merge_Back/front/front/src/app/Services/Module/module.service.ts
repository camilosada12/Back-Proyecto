import { Injectable } from '@angular/core';
import { GenericService } from '../generic/generic-service.service';
import { HttpClient } from '@angular/common/http';
import { Module, ModuleCreate } from '../../Models/module/module.models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ModuleService extends GenericService<Module, ModuleCreate> {

  constructor(http: HttpClient) {
    super(http, 'module');
  }

  public getAllDelete(): Observable<Module[]> {
    return this.http.get<Module[]>(this.endpoint + "?GetAllType=1");
  }


  public logicalDelete(id: number): Observable<any> {
    return this.http.delete(`${this.endpoint}/${id}?deleteType=0`, {});
  }

  public logicalRestore(id: number): Observable<any> {
    return this.http.patch(`${this.endpoint}/logical-restore/${id}`, {});
  }
}
