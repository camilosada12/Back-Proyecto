import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { RouterLink } from '@angular/router';
import { Form } from '../../../Models/Form/form.moduls';
import { FormService } from '../../../Services/From/form.service';
import { RolService } from '../../../Services/Rol/rol.service';
import { Rol } from '../../../Models/Rol/rol.models';

@Component({
  selector: 'app-list-deletes',
  imports: [MatTableModule, MatButtonModule, RouterLink, CommonModule],
  templateUrl: './list-deletes.component.html',
  styleUrl: './list-deletes.component.css'
})
export class ListDeletesComponent {

  RolService = inject(RolService);
  isAdmin: boolean = false;

  forms!: Rol[];
  displayedColumns: string[] = [];


  role = localStorage.getItem("role");


  constructor() {
    this.isAdmin = this.role === "Administrador"

    if (this.isAdmin) {
      this.displayedColumns.push("name", "description", "actions")
      this.load();
    }
  }

  load() {
    this.RolService.getAllDelete().subscribe((data) => {
      this.forms = data;
      console.log(this.forms);

    })
  }


  restore(id: number) {
    this.RolService.logicalRestore(id).subscribe(() => {
      console.log("Se restauro");
      this.load();
    })
  }
}
