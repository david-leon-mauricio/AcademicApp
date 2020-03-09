import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
@Injectable()
export class StudentService {
  myAppUrl: string = "";
  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }
  getStudents() {
    return this._http.get(this.myAppUrl + '/api/Students/')
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }
  getStudentById(id: number) {
    return this._http.get(this.myAppUrl + "/api/Students/" + id)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }
  saveStudent(student) {
    return this._http.post(this.myAppUrl + '/api/Students/', student)
      .map((response: Response) => response.json())
      .catch(this.errorHandler)
  }
  updateStudent(student) {
    return this._http.put(this.myAppUrl + '/api/Students/', student)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }
  deleteStudent(id) {
    return this._http.delete(this.myAppUrl + "/api/Students/" + id)
      .map((response: Response) => response.json())
      .catch(this.errorHandler);
  }
  errorHandler(error: Response) {
    console.log(error);
    return Observable.throw(error);
  }
}
