import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminLoginComponent } from './components/admin/adminlogin/adminlogin.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MainpageComponent } from './components/Home/mainpage/mainpage.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { ServiceListComponent } from './components/service/service-list/service-list.component';
import { ServiceSectionComponent } from './components/service/service-section/service-section.component';
import { DoctorstatusComponent } from './components/admin/doctorstatus/doctorstatus.component';
import { PatientstatusComponent } from './components/admin/patientstatus/patientstatus.component';
import { AdminnavComponent } from './components/admin/adminnav/adminnav.component';
import { AppointmentdetailsComponent } from './components/admin/appointmentdetails/appointmentdetails.component';
import { PatientloginComponent } from './components/patientpage/patientlogin/patientlogin.component';
import { PatientregisterComponent } from './components/patientregister/patientregister.component';
import { DoctorrequestsComponent } from './components/admin/doctorrequests/doctorrequests.component';
import { PatientapproveComponent } from './components/admin/patientapprove/patientapprove.component';
import { PatientdeclineComponent } from './components/admin/patientdecline/patientdecline.component';
import { DoctordeclineComponent } from './components/admin/doctordecline/doctordecline.component';
import { DoctorloginComponent } from './components/doctor/doctorlogin/doctorlogin.component';
import { DoctorregisterComponent } from './components/doctor/doctorregister/doctorregister.component';
import { UserprofileComponent } from './components/patientpage/userprofile/userprofile.component';
import { BookappointmentComponent } from './components/patientpage/bookappointment/bookappointment.component';
import { AppointmentpageComponent } from './components/patientpage/appointmentpage/appointmentpage.component';
import { NavbarComponent } from './components/Home/navbar/navbar.component';
import { NgToastModule } from 'ng-angular-popup';
import { SuccessfulComponent } from './components/patientpage/successful/successful.component';
import { PasswordComponent } from './components/confirmpassword/password/password.component';
import { DoctorpageComponent } from './components/doctor/doctorpage/doctorpage.component';
import { DoctornavComponent } from './components/doctor/doctornav/doctornav.component';
import { AppointmentrequestComponent } from './components/doctor/appointmentrequest/appointmentrequest.component';
import { AppointmentapproveComponent } from './components/doctor/appointmentapprove/appointmentapprove.component';
import { AppointmentdeclineComponent } from './components/doctor/appointmentdecline/appointmentdecline.component';
import { AppointmentpendingComponent } from './components/admin/appointmentpending/appointmentpending.component';


@NgModule({
  declarations: [
    AppComponent,
    AdminLoginComponent,
    MainpageComponent,
    ServiceListComponent,
    ServiceSectionComponent,
    DoctorstatusComponent,
    PatientstatusComponent,
    AdminnavComponent,
    AppointmentdetailsComponent,
    PatientloginComponent,
    PatientregisterComponent,
    DoctorrequestsComponent,
    PatientapproveComponent,
    PatientdeclineComponent,
    DoctordeclineComponent,
    DoctorloginComponent,
    DoctorregisterComponent,
    UserprofileComponent,
    BookappointmentComponent,
    AppointmentpageComponent,
    NavbarComponent,
    SuccessfulComponent,
    PasswordComponent,
    DoctorpageComponent,
    DoctornavComponent,
    AppointmentrequestComponent,
    AppointmentapproveComponent,
    AppointmentdeclineComponent,
    AppointmentpendingComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatDialogModule,
    NgToastModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
