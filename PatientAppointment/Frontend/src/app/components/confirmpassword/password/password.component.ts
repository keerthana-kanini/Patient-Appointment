import { Component } from '@angular/core';
import { IPass } from '../IPass';
import { PaaswordService } from '../../service/paasword.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-password',
  templateUrl: './password.component.html',
  styleUrls: ['./password.component.css']  
})
export class PasswordComponent {
  pass: IPass = { password: '', confirmPassword: '' };
  passwordMatchMessage: string = '';
 constructor(private service:PaaswordService,private router: Router,){}
  checkPassword() {
    if (this.pass.password === this.pass.confirmPassword) {
      if (this.isPasswordValid()) {
        this.passwordMatchMessage = 'Passwords match and meet the criteria!';
        console.log(this.pass);
      } else {
        this.passwordMatchMessage = 'Password does not meet the criteria!';
      }
    } else {
      this.passwordMatchMessage = 'Passwords do not match!';
    }
  }

  private isPasswordValid(): boolean {
    const passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$/;
    return passwordPattern.test(this.pass.password);
  }

  updatePassword()
  {
    console.log("clicked")
    this.service.updatePassword(this.pass.password).subscribe(dt=>{
      console.log(dt)
      alert("Password Successfully Updated")
      this.router.navigateByUrl('/main');
    })
  }
}
