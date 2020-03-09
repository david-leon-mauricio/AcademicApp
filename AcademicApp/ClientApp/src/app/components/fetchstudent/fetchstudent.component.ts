import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { StudentService } from '../../services/studentservice.service'
@Component({
  selector: 'fetchstudent',
  templateUrl: './fetchstudent.component.html'
})
export class FetchStudentComponent {
  public studentList: StudentData[];
  constructor(public http: HttpClient, private _router: Router, private _studentService: StudentService) {
    this.getStudents();
  }
  getStudents() {
    this._studentService.getStudents().subscribe(
      data => this.studentList = data
    )
  }
  delete(personalIdentifier) {
    var ans = confirm("Do you want to delete student with personal Id: " + personalIdentifier);
    if (ans) {
      this._studentService.deleteStudent(personalIdentifier).subscribe((data) => {
        this.getStudents();
      }, error => console.error(error))
    }
  }
}
interface StudentData {
  name: string;
  personalIdentifier: number;
  gender: string;
  type: string;
  updated: string;
}
