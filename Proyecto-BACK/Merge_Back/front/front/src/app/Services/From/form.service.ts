import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericService } from '../generic/generic-service.service';

import { Form, FormCreate } from '../../Models/Form/form.moduls';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FormService extends GenericService<Form, FormCreate> {
  constructor(http: HttpClient) {
    super(http, 'form');
  }

  public getAllDelete(): Observable<Form[]> {
    return this.http.get<Form[]>(this.endpoint + "?GetAllType=1");
  }


  public logicalDelete(id: number): Observable<any> {
    return this.http.delete(`${this.endpoint}/${id}?deleteType=0`, {});
  }

  public logicalRestore(id: number): Observable<any> {
    return this.http.patch(`${this.endpoint}/logical-restore/${id}`, {});
  }

}
