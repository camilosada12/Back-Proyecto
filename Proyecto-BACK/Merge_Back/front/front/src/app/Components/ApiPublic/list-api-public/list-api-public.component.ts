import { Component, inject, OnInit } from '@angular/core';
import { ApiPublicService, Attraction } from '../../../Services/ApiPublic/api-public.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { RouterLink } from '@angular/router';
import { MyApiPublicService } from '../../../Services/MyApiPublic/my-api-public.service';

@Component({
  selector: 'app-list-api-public',
  imports: [CommonModule, ReactiveFormsModule, MatButtonModule, MatIconModule, RouterLink, MatTableModule, MatFormFieldModule, MatInputModule],
  templateUrl: './list-api-public.component.html',
  styleUrl: './list-api-public.component.css'
})
export class ListApiPublicComponent implements OnInit {

  formServices = inject(MyApiPublicService);
  fb = inject(FormBuilder);

  formForm!: FormGroup;
  forms?: Attraction[];

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

  editApiPublic(rol: Attraction) {
    this.formForm.patchValue(rol);
    this.selectedRolId = rol.id;
    this.isEditMode = true;
  }
  deleteApiPublic(id: number) {
    this.formServices.delete(id).subscribe(() => {
      console.log("Se elimino")
      this.getAll();
    })
  }

  // deleteLogic(id: number) {
  //   this.formServices.logicalDelete(id).subscribe(() => {
  //     console.log("Se elimino Logicamnete")
  //     this.getAll();
  //   })
  // }


  resetForm() {
    this.formForm.reset();
    this.isEditMode = false;
    this.selectedRolId = null;
  }
}
