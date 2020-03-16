import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';  
import { Router, ActivatedRoute } from '@angular/router';
import { StudentService } from '../../services/studentservice.service';
import { Student } from '../../../models/student';
  
@Component({  
    selector: 'createstudent',  
    templateUrl: './addstudent.component.html',
    styleUrls: ['./addstudent.component.css'] 
})  
  
export class CreateStudent implements OnInit {  
    studentForm: FormGroup;  
    title: string = "Create";  
    id: number;  
    errorMessage: any;  
  
    constructor(private _fb: FormBuilder, private _avRoute: ActivatedRoute,  
        private _studentService: StudentService, private _router: Router) {  
        if (this._avRoute.snapshot.params["id"]) {  
            this.id = this._avRoute.snapshot.params["id"];  
        }  
  
        this.studentForm = this._fb.group({  
            personalIdentifier: ['', [Validators.required]],  
            name: ['', [Validators.required]],  
            gender: ['', [Validators.required]],  
            type: ['', [Validators.required]]  
        })  
    }  
  
    ngOnInit() {  
        if (this.id > 0) {  
            this.title = "Edit";  
            this._studentService.getStudentById(this.id)  
              .subscribe((response: Student) => {
                this.studentForm.setValue(response);
              }, error => this.errorMessage = error);  
        }  
    }  
  
    save() {  
  
        if (!this.studentForm.valid) {  
            return;  
        }  
  
        if (this.title === "Create") {  
            this._studentService.saveStudent(this.studentForm.value)  
                .subscribe(() => {  
                    this._router.navigate(['/fetchstudent']);  
                }, error => this.errorMessage = error)  
        }  
        else if (this.title === "Edit") {  
            this._studentService.updateStudent(this.studentForm.value)  
                .subscribe(() => {  
                    this._router.navigate(['/fetchstudent']);  
                }, error => this.errorMessage = error)   
        }  
    }  
  
    cancel() {  
        this._router.navigate(['/fetchstudent']);  
    }  

    get personalIdentifier() { return this.studentForm.get('personalIdentifier'); }
    get name() { return this.studentForm.get('name'); }  
    get gender() { return this.studentForm.get('gender'); }  
    get type() { return this.studentForm.get('type'); }
}
