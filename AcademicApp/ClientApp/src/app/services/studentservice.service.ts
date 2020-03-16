import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Student } from '../../models/student';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  myAppUrl: string = "";

  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl + 'api/Students/';
  }

  getStudents() {
    return this._http.get(this.myAppUrl)
      .pipe(map(
        response => {
          return response;
        }));
  }

  getStudentById(id: number) {
    return this._http.get(this.myAppUrl + id)
      .pipe(map(
        response => {
          return response;
        }));
  }

  saveStudent(student: Student) {
    return this._http.post(this.myAppUrl, student)
      .pipe(map(
        response => {
          return response;
        }));
  }

  updateStudent(student: Student) {
    return this._http.put(this.myAppUrl, student)
      .pipe(map(
        response => {
          return response;
        }));
  }

  deleteStudent(id) {
    return this._http.delete(this.myAppUrl + id)
      .pipe(map(
        response => {
          return response;
        }));
  }
}
