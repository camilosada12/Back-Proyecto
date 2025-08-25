import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { RouterLink } from '@angular/router';
import { Form } from '../../../Models/Form/form.moduls';
import { FormService } from '../../../Services/From/form.service';

@Component({
  selector: 'app-list-deletes',
  imports: [MatTableModule, MatButtonModule, RouterLink, CommonModule],
  templateUrl: './list-deletes.component.html',
  styleUrl: './list-deletes.component.css'
})
export class ListDeletesComponent {

  formService = inject(FormService);
  isAdmin: boolean = false;

  forms!: Form[];
  displayedColumns: string[] = [];


  role = localStorage.getItem("role");


  constructor() {
    this.verificarRol();
    this.displayedColumns.push("name", "description", "actions")
    this.load();
  }

  load() {
    this.formService.getAllDelete().subscribe((data) => {
      this.forms = data;
      console.log(this.forms);

    })
  }


  restore(id: number) {
    this.formService.logicalRestore(id).subscribe(() => {
      console.log("Se restauro");
      this.load();
    })
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
