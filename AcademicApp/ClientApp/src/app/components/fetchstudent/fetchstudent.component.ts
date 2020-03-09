import { Component, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Router, ActivatedRoute } from '@angular/router';
import { StudentService } from '../../services/studentservice.service'
@Component({
  selector: 'fetchemployee',
  templateUrl: './fetchemployee.component.html'
})
export class FetchStudentComponent {
  public empList: StudentData[];
  constructor(public http: Http, private _router: Router, private _studentService: StudentService) {
    this.getStudents();
  }
  getStudents() {
    this._studentService.getStudents().subscribe(
      data => this.empList = data
    )
  }
  delete(studentID) {
    var ans = confirm("Do you want to delete student with personal Id: " + studentID);
    if (ans) {
      this._studentService.deleteStudent(studentID).subscribe((data) => {
        this.getStudents();
      }, error => console.error(error))
    }
  }
}
interface StudentData {
  id: number;
  name: string;
  gender: string;
  type: string;
}
