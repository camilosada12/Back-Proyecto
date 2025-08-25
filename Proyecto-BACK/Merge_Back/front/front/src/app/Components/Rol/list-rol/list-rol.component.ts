import { Component, inject, OnInit } from '@angular/core';
import { RolService } from '../../../Services/Rol/rol.service';
import { Rol, RolCreate } from '../../../Models/Rol/rol.models';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { GenericCrudComponent } from "../../GenerciCrud/generic-crud/generic-crud.component";
import { HttpClient } from '@angular/common/http';
import { FieldConfig } from '../../../Models/FormGeneric/FieldConfig.models';
import { GenericService } from '../../../Services/generic/generic-service.service';
import { FormService } from '../../../Services/From/form.service';
import { Form } from '../../../Models/Form/form.moduls';
@Component({
  selector: 'app-list-rol',
  templateUrl: './list-rol.component.html',
  styleUrls: ['./list-rol.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatInputModule]
})
export class ListRolComponent implements OnInit {
  rolService = inject(RolService);
  fb = inject(FormBuilder);

  rolForm!: FormGroup;
  rols: Rol[] = [];

  isEditMode = false;
  selectedRolId: number | null = null;

  ngOnInit(): void {
    this.initForm();
    this.getAll();
  }

  initForm() {
    this.rolForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  getAll() {
    this.rolService.getAll().subscribe(rols => this.rols = rols);
  }

  saveRol() {
    const rol = this.rolForm.value;

    if (this.isEditMode && this.selectedRolId) {
      this.rolService.update(this.selectedRolId, rol).subscribe(() => {
        this.resetForm();
        this.getAll();
      });
    } else {
      this.rolService.create(rol).subscribe(() => {
        this.resetForm();
        this.getAll();
      });
    }
  }

  editRol(rol: Rol) {
    this.rolForm.patchValue(rol);
    this.selectedRolId = rol.id;
    this.isEditMode = true;
  }

  deleteRol(id: number) {
    this.rolService.delete(id).subscribe(() => this.getAll());
  }

  resetForm() {
    this.rolForm.reset();
    this.isEditMode = false;
    this.selectedRolId = null;
  }

  // fb = inject(FormBuilder);
  // formService = inject(FormService);

  // form = this.fb.group({
  //   name: [''],
  //   description: ['']
  // });

  // fields = [
  //   { key: 'name', label: 'Name' },
  //   { key: 'description', label: 'Description', type: 'text' }
  // ];

  // columns = [
  //   { key: 'name', label: 'Name' },
  //   { key: 'description', label: 'Description' }
  // ];

  // getId = (item: Form) => item.id!;
}
