import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Employee } from 'src/app/models/employee.model';
import { Role } from 'src/app/models/role.model';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-employee-action',
  templateUrl: './employee-action.component.html',
  styleUrls: ['./employee-action.component.scss']
})
export class EmployeeActionComponent implements OnInit {
  employee: Employee = {} as Employee;
  roles: Role[] = []
  dialogData: any;
  maxResultCount: number = 10;
  skipCount: number = 0;
  isEdit: boolean = false;


  constructor(
    private http: HttpService,
    public config: DynamicDialogConfig,
    private dialogReference: DynamicDialogRef,
  ) { }

  ngOnInit(): void {
    this.initializeModel()
  }

  private initializeModel() {
    this.dialogData = this.config.data
    if (this.dialogData.id) {
      this.isEdit = true
    }
    if (this.isEdit) {
      this.getEmployeeById(this.dialogData.id);
    }
    this.getRoles()
  }

  getEmployeeById(id: number) {
    this.http.getById('Employee/EmployeeGetByID', id).then((employee: Employee) => {
      this.employee = employee
    }).catch((err) => {

    })
  }

  getRoles() {
    this.http.get('Role', this.skipCount, this.maxResultCount).then((roles: Role[]) => {
      this.roles = roles
    })
  }

  save() {
    //@ts-ignore
    let role = this.roles.find(role => role.Id == this.employee.roleId);
    if (role) {
      //@ts-ignore
      this.employee.roleName = role.Name
    }
    if (this.isEdit) {
      this.http.update('Employee/UpdateEmployee', this.employee).then((employee: Employee) => {
        this.dialogReference.close(employee)
      }).catch((err) => {
        debugger
      })
    }
    else {
      this.http.create('Employee/CreateEmployee', this.employee).then((employee: Employee) => {
        this.dialogReference.close(employee)
      }).catch((err) => {
        debugger
      })
    }
  }


}
