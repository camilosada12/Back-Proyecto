import { Component, inject, OnInit } from '@angular/core';
import { User, UserCreate } from '../../../Models/User/user.models';
import { UserService } from '../../../Services/User/user.service';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-list-user',
  imports: [MatButtonModule, MatTableModule, MatIconModule, CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './list-user.component.html',
  styleUrl: './list-user.component.css'
})
export class ListUserComponent implements OnInit {

  userServices = inject(UserService);
  fb = inject(FormBuilder);

  users?: User[];
  userForm!: FormGroup;
  isEditMode = false;
  selectedUserId: number | null = null;
  columns = ['name', 'email']

  constructor() { }
  ngOnInit(): void {
    this.initForm();
    this.getAll();
  }

  initForm() {
    this.userForm = this.fb.group({
      name: [''],
      email: [''],
      password: ['']
    });
  }

  public getAll() {
    this.userServices.getAll().subscribe(users => {
      this.users = users;
    });
  }

  saveUser() {
    const user = this.userForm.value;

    if (this.isEditMode && this.selectedUserId) {
      this.userServices.update(this.selectedUserId, user).subscribe(() => {
        this.resetForm();
        this.getAll();
      });
    } else {
      this.userServices.create(user).subscribe(() => {
        this.resetForm();
        this.getAll();
      });
    }
  }


  editUser(user: User) {
    this.userForm.patchValue({
      name: user.name,
      email: user.email,
      password: ''
    });
    this.isEditMode = true;
    this.selectedUserId = user.id;
  }


  deleteUser(id: number) {
    this.userServices.delete(id).subscribe(() => this.getAll());
  }


  deleteLogic(id: number) {
    this.userServices.logicalDelete(id).subscribe(() => {
      console.log("Se elimino Logicamnete")
      this.getAll();
    })
  }

  resetForm() {
    this.userForm.reset();
    this.isEditMode = false;
    this.selectedUserId = null;
  }


}
