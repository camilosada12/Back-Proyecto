import { Component, inject, OnInit } from '@angular/core';
import { User, UserCreate } from '../../../Models/User/user.models';
import { UserService } from '../../../Services/User/user.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-list-user',
  imports: [MatButtonModule, MatIconModule, CommonModule, FormsModule, ReactiveFormsModule],
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
  showForm = false;

  constructor() { }
  
  ngOnInit(): void {
    this.initForm();
    this.getAll();
  }

  initForm() {
    this.userForm = this.fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  public getAll() {
    this.userServices.getAll().subscribe(users => {
      this.users = users;
    });
  }

  saveUser() {
    // In edit mode, remove password validation if it's empty
    if (this.isEditMode && !this.userForm.value.password) {
      this.userForm.get('password')?.clearValidators();
      this.userForm.get('password')?.updateValueAndValidity();
    }

    if (this.userForm.invalid) {
      console.log('Form is invalid:', this.userForm.errors);
      Object.keys(this.userForm.controls).forEach(key => {
        const control = this.userForm.get(key);
        if (control?.invalid) {
          console.log(`${key} is invalid:`, control.errors);
        }
      });
      return;
    }

    const formData = this.userForm.value;
    console.log('Form data:', formData);

    // Prepare user object with passwordHash instead of password
    const user = {
      name: formData.name,
      email: formData.email,
      passwordHash: formData.password // Send password as passwordHash for now
    };
    console.log('Saving user:', user);

    if (this.isEditMode && this.selectedUserId) {
      // Remove passwordHash from update if it's empty
      if (!formData.password) {
        delete user.passwordHash;
      }
      // Crear objeto UserCreate con id
      const userCreate: UserCreate = {
        id: this.selectedUserId,
        name: user.name,
        email: user.email,
        password: formData.password,
        passwordHash: user.passwordHash
      };
      this.userServices.update(this.selectedUserId, userCreate).subscribe({
        next: () => {
          console.log('User updated successfully');
          this.showForm = false;
          this.resetForm();
          this.getAll();
        },
        error: (error) => {
          console.error('Error updating user:', error);
        }
      });
    } else {
      // Crear objeto UserCreate con id
      const userCreate: UserCreate = {
        id: 0, // O el valor que corresponda si es autoincremental
        name: user.name,
        email: user.email,
        password: formData.password,
        passwordHash: user.passwordHash
      };
      this.userServices.create(userCreate).subscribe({
        next: () => {
          console.log('User created successfully');
          this.showForm = false;
          this.resetForm();
          this.getAll();
        },
        error: (error) => {
          console.error('Error creating user:', error);
        }
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
    this.showForm = true;
  }

  deleteUser(id: number) {
    this.userServices.logicalDelete(id).subscribe({
      next: () => {
        console.log('User deleted successfully');
        this.getAll();
      },
      error: (error) => {
        console.error('Error deleting user:', error);
      }
    });
  }

  deleteLogic(id: number) {
    this.userServices.logicalDelete(id).subscribe(() => {
      console.log("Se elimino Logicamente")
      this.getAll();
    })
  }

  resetForm() {
    this.userForm.reset();
    this.isEditMode = false;
    this.selectedUserId = null;
    
    // Reset password validators
    this.userForm.get('password')?.setValidators([Validators.required]);
    this.userForm.get('password')?.updateValueAndValidity();
  }
}