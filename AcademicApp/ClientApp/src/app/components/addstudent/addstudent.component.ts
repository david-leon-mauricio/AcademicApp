import { Component, OnInit } from '@angular/core';  
import { Http, Headers } from '@angular/http';  
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';  
import { Router, ActivatedRoute } from '@angular/router';  
import { FetchStudentComponent } from '../fetchstudent/fetchstudent.component';  
import { StudentService } from '../../services/studentservice.service';  
  
@Component({  
    selector: 'createstudent',  
    templateUrl: './addstudent.component.html'  
})  
  
export class createstudent implements OnInit {  
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
            id: 0,  
            name: ['', [Validators.required]],  
            gender: ['', [Validators.required]],  
            type: ['', [Validators.required]]  
        })  
    }  
  
    ngOnInit() {  
        if (this.id > 0) {  
            this.title = "Edit";  
            this._studentService.getStudentById(this.id)  
                .subscribe(resp => this.studentForm.setValue(resp)  
                , error => this.errorMessage = error);  
        }  
    }  
  
    save() {  
  
        if (!this.studentForm.valid) {  
            return;  
        }  
  
        if (this.title == "Create") {  
            this._studentService.saveStudent(this.studentForm.value)  
                .subscribe((data) => {  
                    this._router.navigate(['/fetch-student']);  
                }, error => this.errorMessage = error)  
        }  
        else if (this.title == "Edit") {  
            this._studentService.updateStudent(this.studentForm.value)  
                .subscribe((data) => {  
                    this._router.navigate(['/fetch-student']);  
                }, error => this.errorMessage = error)   
        }  
    }  
  
    cancel() {  
        this._router.navigate(['/fetch-student']);  
    }  
  
    get name() { return this.studentForm.get('name'); }  
    get gender() { return this.studentForm.get('gender'); }  
    get type() { return this.studentForm.get('type'); }  
    get updated() { return this.studentForm.get('updated'); }  
}