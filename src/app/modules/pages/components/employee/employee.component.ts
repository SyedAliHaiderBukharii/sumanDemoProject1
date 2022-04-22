import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/models/employee.model';
import { GlobalService } from 'src/app/services/global.service';
import { HttpService } from 'src/app/services/http.service';
import { EmployeeActionComponent } from './employee-action/employee-action.component';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent implements OnInit {
  maxResultCount: number = 10;
  skipCount: number = 0;
  employees: Employee[] = [
    {
      age:'12'
    } as Employee
  ]
  rows: number = 10;
  page: number = 1;
  totalCount: number = 0

  constructor(
    private http: HttpService,
    private global: GlobalService
  ) { }

  ngOnInit(): void {
    this.initializeModel()
  }

  private initializeModel() {
    this.getAllEmployees()
  }

  getAllEmployees() {
    this.http.get('Employee', this.skipCount, this.maxResultCount).then((employees: Employee[]) => {
      this.employees = employees
    }).catch((err) => {

    })
  }

  search(event: any) {

  }

  create() {
    this.global.showDynamicDialog(EmployeeActionComponent, {}).then((employee: Employee) => {
      this.employees.push(employee)
    })
  }

  edit(id: number, idx: number) {
    this.global.showDynamicDialog(EmployeeActionComponent, { id }).then((employee: Employee) => {
      this.employees.splice(idx, 1, employee)
    }).catch((err) => {
    })
  }

  delete(employee: Employee, idx: number) {
    this.http.delete('Employee/DeleteEmployee', employee).then(value => {
      this.employees.splice(idx, 1)
    }).catch(reason => {
    })
  }

  paginate(event: any) {
    this.rows = event.rows;
    this.maxResultCount = this.rows;
    this.skipCount = event.first;
    this.page = event.page
    this.getAllEmployees()
  }

}
