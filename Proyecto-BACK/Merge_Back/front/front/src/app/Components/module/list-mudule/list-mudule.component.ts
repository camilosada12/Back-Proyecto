import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Module } from '../../../Models/module/module.models';
import { ModuleService } from '../../../Services/Module/module.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-list-mudule',
  imports: [CommonModule, ReactiveFormsModule, MatButtonModule, MatIconModule, RouterLink, MatTableModule, MatFormFieldModule, MatInputModule],
  templateUrl: './list-mudule.component.html',
  styleUrl: './list-mudule.component.css'
})
export class ListMuduleComponent implements OnInit {

  formServices = inject(ModuleService);
  fb = inject(FormBuilder);

  formForm!: FormGroup;
  modules?: Module[];

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
    this.formServices.getAll().subscribe(modules => {
      this.modules = modules;
    });
  }

  save() {
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

  edit(module: Module) {
    this.formForm.patchValue(module);
    this.selectedRolId = module.id;
    this.isEditMode = true;
  }
  delete(id: number) {
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
}
