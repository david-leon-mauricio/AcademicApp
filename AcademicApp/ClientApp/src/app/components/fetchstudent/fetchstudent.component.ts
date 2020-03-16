import { Component } from '@angular/core';
import { StudentService } from '../../services/studentservice.service'
import { Student } from '../../../models/student';

@Component({
  selector: 'fetchstudent',
  templateUrl: './fetchstudent.component.html',
  styleUrls: ['./fetchstudent.component.css']
})
export class FetchStudentComponent {

  public studentList: Student[];

  constructor(private _studentService: StudentService) {
    this.getStudents();
  }
  getStudents() {
    this._studentService.getStudents().subscribe(
      (data: Student[]) => this.studentList = data
    )
  }
  delete(personalIdentifier) {
    var ans = confirm("Do you want to delete student with personal Id: " + personalIdentifier);
    if (ans) {
      this._studentService.deleteStudent(personalIdentifier).subscribe(() => {
        this.getStudents();
      }, error => console.error(error))
    }
  }
}
