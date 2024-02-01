using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.IRepository_Interface
{
    public interface IMailRepository
    {
         string GetByEmail(string toEmail);
        string GetByEmailDoctor(string toEmail);
        void SendApprovalOrDeclineEmail(string toEmail, bool isApproved);
        void SendApprovalOrDeclineEmailtoDoctor(string toEmail, bool isApproved);
        void SendApprovalEmaildoctors(string toEmail, string status);
    void SendDoctorAppointmentNotification(string doctorEmail, string action);
    string ApproveEmail(string toEmail);
    void SendDeclineEmail(string toEmail);
    void SendRescheduleEmail(string toEmail);
    }
}
