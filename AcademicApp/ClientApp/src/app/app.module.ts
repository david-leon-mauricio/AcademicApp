import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';  
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpModule } from '@angular/http';  
import { RouterModule } from '@angular/router';  
  
import { AppComponent } from './app.component';  
import { NavMenuComponent } from './nav-menu/nav-menu.component';  
import { HomeComponent } from './home/home.component';  
import { FetchStudentComponent } from './components/fetchstudent/fetchstudent.component'  
import { CreateStudent } from './components/addstudent/addstudent.component'  
import { BrowserModule } from '@angular/platform-browser';
  
@NgModule({  
    declarations: [  
        AppComponent,  
        NavMenuComponent,  
        HomeComponent,  
        FetchStudentComponent,  
        CreateStudent,  
    ],  
    imports: [ 
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }), 
        CommonModule,  
        HttpClientModule,
        HttpModule,
        FormsModule,  
        ReactiveFormsModule,  
        RouterModule.forRoot([  
            { path: '', redirectTo: 'home', pathMatch: 'full' },  
            { path: 'home', component: HomeComponent },  
            { path: 'fetchstudent', component: FetchStudentComponent },  
            { path: 'register-student', component: CreateStudent },  
            { path: 'student/edit/:id', component: CreateStudent },  
            { path: '**', redirectTo: 'home' }  
        ])  
    ],
    bootstrap: [AppComponent],  
    providers: []  
}) 
export class AppModule { }
