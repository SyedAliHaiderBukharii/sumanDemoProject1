import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(
    private http: HttpClient
  ) { }

  get(path: string, skipCount: number, maxResultCount: number, search?: string): Promise<any> {
    let options_: any = {
      observe: "response",
      responseType: "json",
      headers: new HttpHeaders({
      })
    };
    return new Promise((resolve, reject) => {
      let url = environment.baseUrl;
      url += path + '?skipCount=' + skipCount + "&maxResultCount=" + maxResultCount;
      if (search) {
        url + "&search=" + search
      }

      this.http.request("get", url, options_).subscribe((res: any) => {
        if (res.body.Data) {
          resolve(res.body.Data)
        }
        else {
          reject(res)
        }
      }, (err) => {
        reject(err)
      })
    })
  }

  getById(path: string, id: number): Promise<any> {
    let options_: any = {
      observe: "response",
      responseType: "json",
      headers: new HttpHeaders({
      })
    };
    return new Promise((resolve, reject) => {
      let url = environment.baseUrl
      url += path + "?id=" + id
      this.http.request("get", url, options_).subscribe((res: any) => {
        if (res.body.data) {
          resolve(res.body.data)
        }
        else {
          reject(res)
        }
      }, (err) => {
        reject(err)
      })
    })
  }

  create(path: string, body: any): Promise<any> {
    return new Promise((resolve, reject) => {
      let options_: any = {
        body: body,
        observe: "response",
        responseType: "json",
        headers: new HttpHeaders({
        })
      };
      this.http.request("post", environment.baseUrl + path, options_).subscribe((res: any) => {
        if (res.body.Data) {
          resolve(res.body.Data)
        }
        else {
          reject(res)
        }
      }, (err) => {
        reject(err)
      });
    })
  }

  update(path: string, body: any): Promise<any> {
    let options_: any = {
      body: body,
      observe: "response",
      responseType: "json",
      headers: new HttpHeaders({
      })
    };
    return new Promise((resolve, reject) => {
      let url = environment.baseUrl;
      url += path
      this.http.request("put", url, options_).subscribe((res: any) => {
        if (res.body.Data) {
          resolve(res.body.Data)
        }
        else {
          reject(res)
        }
      }, (err) => {
        reject(err)
      })
    })
  }

  delete(path: string, body: any): Promise<any> {
    let options_: any = {
      body: body,
      observe: "response",
      responseType: "json",
      headers: new HttpHeaders({
      })
    };
    return new Promise((resolve, reject) => {
      let url = environment.baseUrl;
      url += path
      this.http.request("delete", url, options_).subscribe((res: any) => {
        if (res.body.Data) {
          resolve(res.body.Data)
        }
        else {
          reject(res)
        }
      }, (err) => {
        reject(err)
      })
    })
  }
}
