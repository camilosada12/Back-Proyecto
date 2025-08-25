import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormService } from '../../../Services/From/form.service';
import { Form } from '../../../Models/Form/form.moduls';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-form-list',
  imports: [CommonModule, ReactiveFormsModule, MatButtonModule, MatIconModule, RouterLink, MatTableModule, MatFormFieldModule, MatInputModule],
  templateUrl: './form-list.component.html',
  styleUrl: './form-list.component.css'
})
export class FormListComponent implements OnInit {

  formServices = inject(FormService);
  fb = inject(FormBuilder);

  formForm!: FormGroup;
  forms?: Form[];

  isEditMode = false;
  selectedRolId: number | null = null;
  // columns = ['name', 'description', 'action']

  isAdmin: boolean = false;

  role = localStorage.getItem("role");

  // constructor() {
  //   this.isAdmin = this.role === "Administrador";
  //   this.getAll();

  // }

  ngOnInit(): void {
    this.verificarRol();
    this.initForm();
    this.getAll();
  }
  initForm() {
    this.formForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  public getAll() {
    this.formServices.getAll().subscribe(forms => {
      this.forms = forms;
    });
  }

  saveRol() {
    const rol = this.formForm.value;

    if (this.isEditMode && this.selectedRolId) {
      this.formServices.update(this.selectedRolId, rol).subscribe(() => {
        this.resetForm();
        this.getAll();
      });
    } else {
      this.formServices.create(rol).subscribe(() => {
        this.resetForm();
        this.getAll();
      });
    }
  }

  editform(rol: Form) {
    this.formForm.patchValue(rol);
    this.selectedRolId = rol.id;
    this.isEditMode = true;
  }
  deleteform(id: number) {
    this.formServices.delete(id).subscribe(() => {
      console.log("Se elimino")
      this.getAll();
    })
  }

  deleteLogic(id: number) {
    this.formServices.logicalDelete(id).subscribe(() => {
      console.log("Se elimino Logicamnete")
      this.getAll();
    })
  }


  resetForm() {
    this.formForm.reset();
    this.isEditMode = false;
    this.selectedRolId = null;
  }

  verificarRol() {
    const rolesRaw = localStorage.getItem('roles');
    const roles = rolesRaw ? JSON.parse(rolesRaw) : [];

    if (typeof roles === 'string') {
      this.isAdmin = roles === 'Administrador';
    }

    if (Array.isArray(roles)) {
      this.isAdmin = roles.includes('Administrador');
    }
  }
}
