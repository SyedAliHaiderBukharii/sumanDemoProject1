import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesComponent } from './pages.component';
import { PageRoutingModule } from './pages.routing.module';
import { ToastModule } from 'primeng/toast';
import { EmployeeComponent } from './components/employee/employee.component';
import { RoleComponent } from './components/role/role.component';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { EmployeeActionComponent } from './components/employee/employee-action/employee-action.component';
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { MessageService } from 'primeng/api';
import { HttpService } from 'src/app/services/http.service';
import { GlobalService } from 'src/app/services/global.service';
import { HttpClientModule } from '@angular/common/http';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';

@NgModule({
  declarations: [
    PagesComponent,
    EmployeeComponent,
    RoleComponent,
    EmployeeActionComponent
  ],
  imports: [
    CommonModule,
    PageRoutingModule,
    ToastModule,
    TableModule,
    PaginatorModule,
    DynamicDialogModule,
    HttpClientModule,
    InputTextModule,
    ButtonModule
  ],
  providers: [
    MessageService,
    DialogService,
    HttpService,
    GlobalService
  ]
})
export class PagesModule { }
