import { Component, inject } from '@angular/core';
import { FormModuleCreate } from '../../../Models/formModule/formModule.models';
import { Router } from '@angular/router';
import { FormFormModuleComponent } from "../form-form-module/form-form-module.component";
import { FormModuleService } from '../../../Services/FormModule/form-module.service';

@Component({
  selector: 'app-create-form-module',
  imports: [ FormFormModuleComponent],
  templateUrl: './create-form-module.component.html',
  styleUrl: './create-form-module.component.css'
})
export class CreateFormModuleComponent {

  private formModuleService = inject(FormModuleService);
  private router = inject(Router);

  save(formModule: FormModuleCreate) {
    this.formModuleService.create(formModule).subscribe(() => {
      this.router.navigate(['formModule'])
    })
  }
}
