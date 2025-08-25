import { Injectable } from '@angular/core';
import { GenericService } from '../generic/generic-service.service';
import { FormModuleCreate, FormModuleEntity } from '../../Models/formModule/formModule.models';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FormModuleService extends GenericService<FormModuleEntity, FormModuleCreate> {

  constructor(http: HttpClient) {
    super(http, 'formModule')
  }
  public getAllDelete(): Observable<FormModuleEntity[]> {
    return this.http.get<FormModuleEntity[]>(this.endpoint + "?GetAllType=1");
  }


  public logicalDelete(id: number): Observable<any> {
    return this.http.delete(`${this.endpoint}/${id}?deleteType=0`, {});
  }

  public logicalRestore(id: number): Observable<any> {
    return this.http.patch(`${this.endpoint}/logical-restore/${id}`, {});
  }
}
