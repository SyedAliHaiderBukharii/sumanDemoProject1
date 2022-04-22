import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { Subject } from 'rxjs';
import { MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';

@Injectable({
  providedIn: 'root'
})
export class GlobalService {

  constructor(
    private titleService: Title,
    private router: Router,
    private location: Location,
    private messageService: MessageService,
    private dialogService: DialogService,
  ) { }

  setTitle(title: string = '') {
    this.titleService.setTitle('Demo - ' + title);
  }

  getLocalStorage(key: string) {
    return new Promise((resolve, reject) => {
      let item = localStorage.getItem(key);
      if (item) {
        resolve(JSON.parse(item));
      }
      else {
        reject(null)
      }
    })
  }

  setLocalStorage(key: string, value: any) {
    localStorage.setItem(key, JSON.stringify(value));
  }

  clearStorage() {
    localStorage.clear()
  }

  goToPage(page: string, arg: any = {}) {
    this.router.navigate([page, arg])
  }

  goBack() {
    this.location.back()
  }

  showToast(severity: string, summary: string, detail: string) {
    this.messageService.add({
      severity: severity,
      summary: summary,
      detail: detail
    });
  }

  setObservable(variable: any, value: string) {
    //@ts-ignore
    this[variable].next(value);
  }

  getObservable(variable: any): Subject<any> {
    //@ts-ignore
    return this[variable];
  }

  showDynamicDialog(component: any, params: any): Promise<any> {
    const ref = this.dialogService.open(component, {
      data: params,
      header: params.header,
      width: '50vw',
      styleClass: 'alert-msg'
    });
    return new Promise((resolve, reject) => {
      ref.onClose.subscribe((data: any) => {
        if (data) {
          resolve(data)
        }
        else {
          reject(false)
        }
      });
    })

  }
}
