import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { GenericService } from '../../../Services/generic/generic-service.service';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { FieldConfig } from '../../../Models/FormGeneric/FieldConfig.models';
import { Observable } from 'rxjs';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-generic-crud',
  templateUrl: './generic-crud.component.html',
  styleUrls: ['./generic-crud.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    NgIf,
    NgFor,
    MatSelectModule
  ],
})
export class GenericCrudComponent<
  TList extends { [key: string]: any },
  TCreate extends { [key: string]: any }
> implements OnInit {


  @Input() formGroup!: FormGroup;
  @Input() service!: {
    getAll: () => Observable<TList[]>,
    create: (item: TCreate) => Observable<any>,
    update: (id: number, item: TCreate) => Observable<any>,
    delete: (id: number) => Observable<any>
  };
  @Input() columns: { key: string, label: string }[] = [];
  @Input() formFields: { key: string, label: string, type?: string }[] = [];
  @Input() getId!: (item: TList) => number;
  @Input() displayItem?: (item: TList, key: string) => string;

  data: TList[] = [];
  selectedId: number | null = null;
  isEditMode = false;

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.service.getAll().subscribe(data => this.data = data);
  }

  save() {
    const item = this.formGroup.value;
    if (this.isEditMode && this.selectedId != null) {
      this.service.update(this.selectedId, item).subscribe(() => {
        this.reset();
        this.loadData();
      });
    } else {
      this.service.create(item).subscribe(() => {
        this.reset();
        this.loadData();
      });
    }
  }

  edit(item: TList) {
    this.formGroup.patchValue(item);
    this.selectedId = this.getId(item);
    this.isEditMode = true;
  }

  delete(id: number) {
    this.service.delete(id).subscribe(() => this.loadData());
  }

  reset() {
    this.formGroup.reset();
    this.isEditMode = false;
    this.selectedId = null;
  }
}