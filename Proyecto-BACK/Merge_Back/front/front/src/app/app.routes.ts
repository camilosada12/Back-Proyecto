import { Routes } from '@angular/router';
import { LoginComponent } from './Components/Login/login/login.component';
import { ListRolComponent } from './Components/Rol/list-rol/list-rol.component';
import { RegisterComponent } from './Components/Register/register/register.component';
import { authGuard } from './custom/auth.guard';
import { ListRolUserComponent } from './Components/RolUser/list-rol-user/list-rol-user.component';
import { FormListComponent } from './Components/form/form-list/form-list.component';
import { ListDeletesComponent } from './Components/form/list-deletes/list-deletes.component';
import { ListMuduleComponent } from './Components/module/list-mudule/list-mudule.component';
import { ListDeleteModuleComponent } from './Components/module/list-delete-module/list-delete-module.component';
import { ListUserComponent } from './Components/User/list-user/list-user.component';
import { ListApiPublicComponent } from './Components/ApiPublic/list-api-public/list-api-public.component';
import { ApiPublicComponent } from './Components/ApiPublic/api-public/api-public.component';

export const routes: Routes = [

    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },

    { path: 'user', component: ListUserComponent, canActivate: [authGuard] },
    { path: 'apiPublic', component: ApiPublicComponent, canActivate: [authGuard] },

    { path: 'listApiPublic', component: ListApiPublicComponent, canActivate: [authGuard] },


    { path: 'form', component: FormListComponent, canActivate: [authGuard] },
    { path: 'form/listDetele', component: ListDeletesComponent, canActivate: [authGuard] },

    { path: 'module', component: ListMuduleComponent, canActivate: [authGuard] },
    { path: 'module/listDetele', component: ListDeleteModuleComponent, canActivate: [authGuard] },

    { path: 'formModule', component: ListMuduleComponent, canActivate: [authGuard] },
    { path: 'formModule/listDetele', component: ListDeleteModuleComponent, canActivate: [authGuard] },

    { path: 'rol', component: ListRolComponent, canActivate: [authGuard] },
    { path: 'rol/listDetele', component: ListDeleteModuleComponent, canActivate: [authGuard] },
    { path: 'rolUser', component: ListRolUserComponent, canActivate: [authGuard] }
];
