import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminLoginComponent } from './components/admin/adminlogin/adminlogin.component';
import { MainpageComponent } from './components/Home/mainpage/mainpage.component';
import { DoctorstatusComponent } from './components/admin/doctorstatus/doctorstatus.component';
import { PatientstatusComponent } from './components/admin/patientstatus/patientstatus.component';
import { AdminnavComponent } from './components/admin/adminnav/adminnav.component';
import { AppointmentdetailsComponent } from './components/admin/appointmentdetails/appointmentdetails.component';
import { PatientloginComponent } from './components/patientpage/patientlogin/patientlogin.component';
import { PatientregisterComponent } from './components/patientregister/patientregister.component';
import { DoctorrequestsComponent } from './components/admin/doctorrequests/doctorrequests.component';
import { DoctordeclineComponent } from './components/admin/doctordecline/doctordecline.component';
import { DoctorloginComponent } from './components/doctor/doctorlogin/doctorlogin.component';
import { DoctorregisterComponent } from './components/doctor/doctorregister/doctorregister.component';
import { UserprofileComponent } from './components/patientpage/userprofile/userprofile.component';
import { BookappointmentComponent } from './components/patientpage/bookappointment/bookappointment.component';
import { AppointmentpageComponent } from './components/patientpage/appointmentpage/appointmentpage.component';
import { NavbarComponent } from './components/Home/navbar/navbar.component';
import { SuccessfulComponent } from './components/patientpage/successful/successful.component';
import { PasswordComponent } from './components/confirmpassword/password/password.component';
import { DoctorpageComponent } from './components/doctor/doctorpage/doctorpage.component';
import { DoctornavComponent } from './components/doctor/doctornav/doctornav.component';
import { AppointmentapproveComponent } from './components/doctor/appointmentapprove/appointmentapprove.component';
import { AppointmentdeclineComponent } from './components/doctor/appointmentdecline/appointmentdecline.component';
import { AppointmentpendingComponent } from './components/admin/appointmentpending/appointmentpending.component';

const routes: Routes = [
  {path:'adminlogin', component : AdminLoginComponent},
  {path:'main', component : MainpageComponent},
  {path:'docsts', component : DoctorstatusComponent},
  {path:'patsts', component : PatientstatusComponent},
  {path:'adminnav', component : AdminnavComponent},
  {path:'viewapp', component : AppointmentdetailsComponent},
  {path:'patientlogin', component : PatientloginComponent},
  {path:'patientreg', component : PatientregisterComponent},
  {path:'doctorapproved', component : DoctorrequestsComponent},
  {path:'doctordeclined', component : DoctordeclineComponent},
  {path:'doctorlogin', component : DoctorloginComponent},
  {path:'doctorreg', component : DoctorregisterComponent},
  {path:'userprofile', component : UserprofileComponent},
  {path:'bookappointment', component : BookappointmentComponent},
  {path:'appointment/:doctorId', component : AppointmentpageComponent},
  {path:'appointment', component : AppointmentpageComponent},
  {path:'navbar', component : NavbarComponent},
  {path:'success', component : SuccessfulComponent},
  {path:'password', component : PasswordComponent},
  {path:'docapp', component : DoctorpageComponent},
  {path:'docnav', component : DoctornavComponent},
  {path:'appapprove', component : AppointmentapproveComponent},
  {path:'appdecline', component : AppointmentdeclineComponent},
  {path:'appiontmentreq', component : AppointmentpendingComponent},
  { path: '', redirectTo: '/main', pathMatch: 'full' }, 
  { path: '**', redirectTo: '/main' } 

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
