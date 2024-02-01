using DataAccessLayer.Repository.IRepository_Interface;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;


namespace DataAccessLayer.Repository.Repository_Class
{
    public class MailRepository : IMailRepository
    {
        private readonly IConfiguration _config;

        public MailRepository(IConfiguration config)
        {
            _config = config;
        }

        public string GetByEmail(string toEmail)
        {
            string fromMail = "medicarezin@gmail.com";
            string fromPassword = "dugqjqmozqmcziym";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Hospital Registration Successful";
            message.To.Add(new MailAddress(toEmail));

            string registrationMessage = "<html><body>" +
                                        "<h2>Welcome to Our Hospital!</h2>" +
                                        "<p>Thank you for registering with our hospital. Your registration is successful.</p>" +
                                        "<p>We are committed to providing quality healthcare services to our community.</p>" +
                                        "<p>If you have any questions or need assistance, feel free to contact us.</p>" +
                                          "<p>Thank you for registering. Your temporary password is: <a><strong>{temporaryPassword}</strong></a></p>" +
                                        "<p>Thank you and have a healthy day!</p>" +
                                        "</body></html>";

            string temporaryPassword = GenerateRandomAlphanumericPassword(6);


            registrationMessage = registrationMessage.Replace("{temporaryPassword}", temporaryPassword);

            message.Body = registrationMessage;
            message.IsBodyHtml = true;

            var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
            return temporaryPassword;
        }
        private static string GenerateRandomAlphanumericPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var password = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }

            return password.ToString();
        }
        public string GetByEmailDoctor(string toEmail)
        {
            string fromMail = "medicarezin@gmail.com";
            string fromPassword = "dugqjqmozqmcziym";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Hospital Registration Successful";
            message.To.Add(new MailAddress(toEmail));

            string registrationMessage = "<html><body>" +
                                        "<h2>Welcome to Our Hospital!</h2>" +
                                        "<p>Thank you for registering with our hospital. Your registration is successful.</p>" +
                                        "<p>We are committed to providing quality healthcare services to our community.</p>" +
                                        "<p>If you have any questions or need assistance, feel free to contact us.</p>" +
                                          "<p>Thank you for registering. Your temporary password is: <a><strong>{temporaryPassword}</strong></a></p>" +
                                        "<p>Thank you and have a healthy day!</p>" +
                                        "</body></html>";

            string temporaryPassword = GenerateaRandomAlphanumericPassword(6);


            registrationMessage = registrationMessage.Replace("{temporaryPassword}", temporaryPassword);

            message.Body = registrationMessage;
            message.IsBodyHtml = true;

            var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
            return temporaryPassword;
        }
        private static string GenerateaRandomAlphanumericPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var password = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }

            return password.ToString();
        }
        public void SendApprovalOrDeclineEmail(string toEmail, bool isApproved)
        {
            string fromMail = "medicarezin@gmail.com";
            string fromPassword = "dugqjqmozqmcziym";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.To.Add(new MailAddress(toEmail));

            string subject = isApproved ? "Registration Approved" : "Registration Declined";
            string body = isApproved
                ? "<h3>Thank you for registering</h3>" +
                  "<h4>Your account has been verified.</h4>" +
                  "<p>Please login using your temporary password that we sent you during your registration.</p>"
                : "<h3>Registration Declined</h3>" +
                  "<p>We regret to inform you that your registration has been declined.</p>" +
                  "<p>If you have any questions or concerns, please contact our support team.</p>";

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }

        public void SendApprovalOrDeclineEmailtoDoctor(string toEmail, bool isApproved)
        {
            string fromMail = "medicarezin@gmail.com";
            string fromPassword = "dugqjqmozqmcziym";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.To.Add(new MailAddress(toEmail));

            string subject = isApproved ? "Registration Approved" : "Registration Declined";
            string body = isApproved
                ? "<h3>Thank you for registering</h3>" +
                  "<h4>Your account has been verified.</h4>" +
                  "<p>Please login using your  password</p>"
                : "<h3>Registration Declined</h3>" +
                  "<p>We regret to inform you that your registration has been declined.</p>" +
                  "<p>If you have any questions or concerns, please contact our support team.</p>";

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }
        public void SendApprovalEmaildoctors(string toEmail, string status)
        {
            string fromMail = "medicarezin@gmail.com";
            string fromPassword = "dugqjqmozqmcziym";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.To.Add(new MailAddress(toEmail));

            string subject, body;

            if (status.Equals("approve", StringComparison.OrdinalIgnoreCase))
            {
                subject = "Account Approved";
                body = "<html><body>" +
                       "<h3>Thank you for registering</h3>" +
                       "<h4>Your account has been verified.</h4>" +
                       "<p>Please login using your temporary password that we sent you during registration.</p>" +
                       "</body></html>";
            }
            else if (status.Equals("decline", StringComparison.OrdinalIgnoreCase))
            {
                subject = "Account Declined";
                body = "<html><body>" +
                       "<h3>We're sorry, but your account registration has been declined.</h3>" +
                       "<p>If you have any questions or concerns, please contact our support team.</p>" +
                       "</body></html>";
            }
            else
            {

                throw new ArgumentException("Invalid status provided.");
            }

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;


            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }

        public void SendDoctorAppointmentNotification(string doctorEmail, string action)
        {
            string fromMail = "medicarezin@gmail.com";
            string fromPassword = "dugqjqmozqmcziym";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.To.Add(new MailAddress("keerthanar310502@gmail.com"));


            string subject, body;

            switch (action.ToLower())
            {
                case "approve":
                    subject = "Appointment Approved";
                    body = "The appointment for the doctor has been approved.";
                    break;
                case "decline":
                    subject = "Appointment Declined";
                    body = "The appointment for the doctor has been declined.";
                    break;
                case "reschedule":
                    subject = "Appointment Rescheduled";
                    body = "The appointment for the doctor has been rescheduled.";
                    break;
                default:
                    throw new ArgumentException("Invalid action provided.");
            }

            message.Subject = subject;
            message.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }

        public string ApproveEmail(string toEmail)
        {
            string fromMail = "medicarezin@gmail.com";
            string fromPassword = "dugqjqmozqmcziym";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Appointment Confirmation";
            message.To.Add(new MailAddress(toEmail));

            string confirmationMessage = "<html><body>" +
                                        "<h2>Your Appointment has been Confirmed!</h2>" +
                                        "<p>Thank you for scheduling an appointment with our hospital. Your appointment has been confirmed.</p>" +
                                        "<p>We look forward to providing you with quality healthcare services.</p>" +
                                        "<p>If you have any questions or need assistance, feel free to contact us.</p>" +
                                        "<p>Thank you for choosing our hospital. We wish you a healthy day!</p>" +
                                        "</body></html>";

            message.Body = confirmationMessage;
            message.IsBodyHtml = true;

            var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);

            return confirmationMessage;
        }

        public void SendDeclineEmail(string toEmail)
        {
            string fromMail = "medicarezin@gmail.com";
            string fromPassword = "dugqjqmozqmcziym";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Appointment Declined";
            message.To.Add(new MailAddress(toEmail));

            string declineMessage = "<html><body>" +
                                    "<h2>Appointment Declined</h2>" +
                                    "<p>We regret to inform you that your appointment has been declined.</p>" +
                                    "<p>If you have any questions or concerns, please contact our support team.</p>" +
                                    "<p>We appreciate your understanding.</p>" +
                                    "</body></html>";

            message.Body = declineMessage;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);

        }
        public void SendRescheduleEmail(string toEmail)
        {
            string fromMail = "medicarezin@gmail.com";
            string fromPassword = "dugqjqmozqmcziym";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Appointment Rescheduled";
            message.To.Add(new MailAddress(toEmail));

            string rescheduleMessage = "<html><body>" +
                                       "<h2>Appointment Rescheduled</h2>" +
                                       "<p>Your appointment has been rescheduled.</p>" +
                                       "<p>We apologize for any inconvenience caused. Please check your email for the updated details.</p>" +
                                       "<p>If you have any questions or concerns, please contact our support team.</p>" +
                                       "<p>Thank you for your understanding.</p>" +
                                       "<p>You are welcome to book another appointment at your convenience.</p>" +

                                       "</body></html>";

            message.Body = rescheduleMessage;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }


    }
}


