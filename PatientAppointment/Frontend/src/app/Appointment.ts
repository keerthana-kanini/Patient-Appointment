import { Doctor } from "./Doctor";
import { Patient } from "./Patient";

export class Appointment {
    appointment_ID!: number;
    appointmentDate!: Date;
    appointmentTime: string = '';
    status: string = 'pending';
    patient?: Patient; 
    doctor?: Doctor;    
  isButtonDisabled: any;
}
