using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;
using System.Data.SqlClient;
using System.Data;
using DataAccessLayer.Models.DTO;
using DataAccessLayer.Repository.IRepository_Interface;

namespace Patient_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly IMailRepository _mailService;

        public AppointmentsController(IConfiguration configuration, IMailRepository mailService)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _mailService = mailService;
        }

        // GET: api/Appointments
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("GetAllAppointments", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var appointments = new List<Appointments>();
                            while (reader.Read())
                            {
                                appointments.Add(new Appointments
                                {
                                    Appointment_ID = (int)reader["Appointment_ID"],
                                    AppointmentDate = (DateTime)reader["AppointmentDate"],
                                    AppointmentTime = (string)reader["AppointmentTime"],
                                    Status = reader["Status"].ToString()
                                });
                            }
                            return Ok(appointments);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, "Internal Server Error");
            }

        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("GetAppointmentById", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Appointment_ID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var appointment = new Appointments
                                {
                                    Appointment_ID = (int)reader["Appointment_ID"],
                                    AppointmentDate = (DateTime)reader["AppointmentDate"],
                                    AppointmentTime = (string)reader["AppointmentTime"],
                                    Status = reader["Status"].ToString()
                                };
                                return Ok(appointment);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal Server Error");
            }
        }

        // POST: api/Appointments
        [HttpPost]
        public IActionResult Post([FromBody] Appointments appointment)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("InsertAppointment", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                        command.Parameters.AddWithValue("@AppointmentTime", appointment.AppointmentTime);
                        command.Parameters.AddWithValue("@Status", appointment.Status);

                        command.ExecuteNonQuery();
                        return CreatedAtAction("Get", new { id = appointment.Appointment_ID }, appointment);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        // PUT: api/Appointments/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Appointments appointment)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UpdateAppointment", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Appointment_ID", id);
                        command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                        command.Parameters.AddWithValue("@AppointmentTime", appointment.AppointmentTime);
                        command.Parameters.AddWithValue("@Status", appointment.Status);

                        command.ExecuteNonQuery();
                        return NoContent();
                    }
                }
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal Server Error");
            }
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DeleteAppointment", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Appointment_ID", id);

                        command.ExecuteNonQuery();
                        return NoContent();
                    }
                }
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost("BookAppointment")]
        public ActionResult BookAppointment(AppointmentDTO appointment)
        {
            try
            {
                // Validate if the appointment time is available for the given doctor
                string errorMessage = ValidateAppointmentTime(appointment.Doctor_ID, appointment.AppointmentDate, appointment.AppointmentTime);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand command = new SqlCommand("BookAppointment", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@Patient_ID", appointment.Patient_ID);
                            command.Parameters.AddWithValue("@Doctor_ID", appointment.Doctor_ID);
                            command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                            command.Parameters.AddWithValue("@AppointmentTime", appointment.AppointmentTime);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        return Ok(appointment);
                    }
                }
                else
                {
                    return BadRequest(errorMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        private string ValidateAppointmentTime(int doctorId, DateTime appointmentDate, string appointmentTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CheckAppointmentAvailability", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Doctor_ID", doctorId);
                        command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                        command.Parameters.AddWithValue("@AppointmentTime", appointmentTime);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                return $"The selected appointment time ({appointmentTime}) on {appointmentDate.ToShortDateString()} is not available. Please choose an alternate time or date.";
                            }
                            return string.Empty;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "An error occurred while checking the appointment availability.";
            }
        }


        [HttpGet("GetDoctorAppointmentDetails/{doctorId}")]
        public IActionResult GetDoctorAppointmentDetails(int doctorId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetDoctorAppointmentDetails", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Doctor_ID", doctorId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var doctorAppointments = new List<Appointments>();

                            while (reader.Read())
                            {
                                doctorAppointments.Add(new Appointments
                                {
                                    Appointment_ID = (int)reader["Appointment_ID"],
                                    AppointmentDate = (DateTime)reader["AppointmentDate"],
                                    AppointmentTime = (string)reader["AppointmentTime"],
                                    Status = reader["Status"].ToString(),
                                    Doctor = new Doctors
                                    {
                                        Doctor_ID = (int)reader["Doctor_ID"],
                                        Doctor_Name = (string)reader["Doctor_Name"],
                                        Doctor_Email = (string)reader["Doctor_Email"],
                                        Doctor_Location = (string)reader["Doctor_Location"]
                                    },
                                    Patient = new Patients
                                    {
                                        Patient_ID = (int)reader["Patient_ID"],
                                        Patient_Name = (string)reader["Patient_Name"]
                                    }
                                });
                            }

                            return Ok(doctorAppointments);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}\nStack Trace: {ex.StackTrace}");


                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("GetAllAppointments")]
        public IActionResult GetAllAppointments()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetAllAppointments", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var allAppointments = new List<Appointments>();

                            while (reader.Read())
                            {
                                allAppointments.Add(new Appointments
                                {
                                    Appointment_ID = (int)reader["Appointment_ID"],
                                    AppointmentDate = (DateTime)reader["AppointmentDate"],
                                    AppointmentTime = (string)reader["AppointmentTime"],
                                    Status = reader["Status"].ToString(),
                                    Patient = new Patients
                                    {
                                        Patient_ID = (int)reader["Patient_ID"],
                                        Patient_Name = (string)reader["Patient_Name"],
                                        Patient_Gender = (string)reader["Patient_Gender"],
                                        Patient_Email = (string)reader["Patient_Email"]
                                    },
                                    Doctor = new Doctors
                                    {
                                        Doctor_ID = (int)reader["Doctor_ID"],
                                        Doctor_Name = (string)reader["Doctor_Name"],
                                        Doctor_Gender = (string)reader["Doctor_Gender"],
                                        Doctor_Email = (string)reader["Doctor_Email"],
                                        Doctor_Location = (string)reader["Doctor_Location"],
                                        Doctor_Specialization = (string)reader["Doctor_Specialization"]
                                    }
                                });
                            }

                            return Ok(allAppointments);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpGet("AppointmentRequests")]
        public ActionResult<IEnumerable<Appointments>> GetAppointmentRequestsPending()
        {
            List<Appointments> appointmentRequests = new List<Appointments>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("GetAppointmentRequestsPending", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Appointments appointment = new Appointments
                            {
                                Appointment_ID = Convert.ToInt32(reader["Appointment_ID"]),
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                AppointmentTime = reader["AppointmentTime"].ToString(),
                                Status = reader["Status"].ToString(),
                                Doctor = new Doctors
                                {
                                    Doctor_ID = Convert.ToInt32(reader["Doctor_ID"]),
                                    Doctor_Name = reader["Doctor_Name"].ToString(),
                                    Doctor_Email = reader["Doctor_Email"].ToString(),
                                    Doctor_Location = reader["Doctor_Location"].ToString(),
                                    Doctor_Specialization = (string)reader["Doctor_Specialization"]
                                },
                                Patient = new Patients
                                {
                                    Patient_ID = Convert.ToInt32(reader["Patient_ID"]),
                                    Patient_Name = reader["Patient_Name"].ToString(),
                                    Patient_Email = reader["Patient_Email"].ToString(),
                                    Patient_Gender = (string)reader["Patient_Gender"],
                                  

                                }
                            };

                            appointmentRequests.Add(appointment);
                        }
                    }
                }
            }

            return Ok(appointmentRequests);
        }

        [HttpPost("ApproveAppointmentRequest/{id}")]
        public IActionResult ApproveAppointmentRequest(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("ApproveAppointmentRequest", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AppointmentID", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                    string doctorEmail = GetDoctorEmailByAppointmentId(id);

                    // Send notification email
                    _mailService.SendDoctorAppointmentNotification(doctorEmail, "approve");

                    return NoContent();
                }
            }


            return NoContent();


        }

        [HttpPost("DeclineAppointmentRequest/{id}")]
        public IActionResult DeclineAppointmentRequest(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("DeclineAppointmentRequest", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AppointmentID", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                 
                    string doctorEmail = GetDoctorEmailByAppointmentId(id);

                    // Send notification email
                    _mailService.SendDoctorAppointmentNotification(doctorEmail, "decline");

                    return NoContent();
                }

            }
        }  

            
        [HttpGet("AppointmentRequests/{status}")]
        public ActionResult<IEnumerable<Appointments>> GetAppointmentRequestsByStatus(string status)
        {
            if (string.IsNullOrEmpty(status) || (!status.Equals("Approved", StringComparison.OrdinalIgnoreCase) && !status.Equals("Declined", StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("Invalid status. Please provide 'Approved' or 'Declined'.");
            }

            List<Appointments> appointmentRequests = new List<Appointments>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("GetAppointmentRequestsByStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Status", status);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Appointments appointment = new Appointments
                            {
                                Appointment_ID = Convert.ToInt32(reader["Appointment_ID"]),
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                AppointmentTime = reader["AppointmentTime"].ToString(),
                                Status = reader["Status"].ToString(),
                                Doctor = new Doctors
                                {
                                    Doctor_ID = Convert.ToInt32(reader["Doctor_ID"]),
                                    Doctor_Name = reader["Doctor_Name"].ToString(),
                                    Doctor_Email = reader["Doctor_Email"].ToString()
                                },
                                Patient = new Patients
                                {
                                    Patient_ID = Convert.ToInt32(reader["Patient_ID"]),
                                    Patient_Name = reader["Patient_Name"].ToString(),
                                    Patient_Email = reader["Patient_Email"].ToString()
                                }
                            };

                            appointmentRequests.Add(appointment);
                        }
                    }
                }
            }

            return Ok(appointmentRequests);
        }
        [HttpGet("GetDoctorAppointmentByEmail/{doctorEmail}")]
        public IActionResult GetDoctorAppointmentByEmail(string doctorEmail)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("GetDoctorAppointmentByEmail", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Doctor_Email", doctorEmail);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var doctorAppointments = new List<Appointments>();

                            while (reader.Read())
                            {
                                doctorAppointments.Add(new Appointments
                                {
                                    Appointment_ID = (int)reader["Appointment_ID"],
                                    AppointmentDate = (DateTime)reader["AppointmentDate"],
                                    AppointmentTime = (string)reader["AppointmentTime"],
                                    Status = reader["Status"].ToString(),
                                    Doctor = new Doctors
                                    {
                                        Doctor_ID = (int)reader["Doctor_ID"],
                                        Doctor_Name = (string)reader["Doctor_Name"],
                                        Doctor_Email = (string)reader["Doctor_Email"],
                                        Doctor_Location = (string)reader["Doctor_Location"]
                                    },
                                    Patient = new Patients
                                    {
                                        Patient_ID = (int)reader["Patient_ID"],
                                        Patient_Name = (string)reader["Patient_Name"],
                                        Patient_Gender = (string)reader["Patient_Gender"],
                                        Patient_Email = reader["Patient_Email"].ToString()
                                    }
                                });
                            }

                            return Ok(doctorAppointments);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("GetDoctorAppointmentsByEmailAndStatus")]
        public IActionResult GetDoctorAppointmentsByEmailAndStatus(string doctorEmail, string status)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetDoctorAppointmentsByEmailAndStatus", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Doctor_Email", doctorEmail);
                        command.Parameters.AddWithValue("@Status", status);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var doctorAppointments = new List<Appointments>();

                            while (reader.Read())
                            {
                                doctorAppointments.Add(new Appointments
                                {
                                    Appointment_ID = (int)reader["Appointment_ID"],
                                    AppointmentDate = (DateTime)reader["AppointmentDate"],
                                    AppointmentTime = (string)reader["AppointmentTime"],
                                    Status = reader["Status"].ToString(),
                                    Doctor = new Doctors
                                    {
                                        Doctor_ID = (int)reader["Doctor_ID"],
                                        Doctor_Name = (string)reader["Doctor_Name"],
                                        Doctor_Email = (string)reader["Doctor_Email"],
                                        Doctor_Location = (string)reader["Doctor_Location"]
                                    },
                                    Patient = new Patients
                                    {
                                        Patient_ID = (int)reader["Patient_ID"],
                                        Patient_Name = (string)reader["Patient_Name"],
                                        Patient_Gender = (string)reader["Patient_Gender"],
                                        Patient_Email = (string)reader["Patient_Email"]
                                    }
                                });
                            }

                            return Ok(doctorAppointments);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("RescheduleAppointment/{id}")]
        public IActionResult RescheduleAppointment(int id, [FromBody] RescheduleAppointmentDTO rescheduleData)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("RescheduleAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", id);
                        command.Parameters.AddWithValue("@NewDate", rescheduleData.NewDate);
                        command.Parameters.AddWithValue("@NewTime", rescheduleData.NewTime);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            return NotFound("Appointment not found with the provided ID.");
                        }
                    }
                }

                string doctorEmail = GetDoctorEmailByAppointmentId(id);

                // Send notification email
                _mailService.SendDoctorAppointmentNotification(doctorEmail, "reschedule");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private string GetDoctorEmailByAppointmentId(int appointmentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetDoctorEmailByAppointmentId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AppointmentID", appointmentId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int doctorId = reader.GetInt32(reader.GetOrdinal("Doctor_ID"));

                            string doctorEmail = GetDoctorEmailByAppointmentId(doctorId); 
                            return doctorEmail;
                        }
                    }
                }
            }

            return null; 
        }
        [HttpGet("AppointmentRequests/Approved")]
        public ActionResult<IEnumerable<Appointments>> GetApprovedAppointmentRequests()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetApprovedAppointmentRequests", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var approvedAppointmentRequests = new List<Appointments>();

                            while (reader.Read())
                            {
                                Appointments appointment = new Appointments
                                {
                                    Appointment_ID = Convert.ToInt32(reader["Appointment_ID"]),
                                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                    AppointmentTime = reader["AppointmentTime"].ToString(),
                                    Status = reader["Status"].ToString(),
                                    Doctor = new Doctors
                                    {
                                        Doctor_ID = Convert.ToInt32(reader["Doctor_ID"]),
                                        Doctor_Name = reader["Doctor_Name"].ToString(),
                                        Doctor_Email = reader["Doctor_Email"].ToString(),
                                        Doctor_Specialization = (string)reader["Doctor_Specialization"]
                                    },
                                    Patient = new Patients
                                    {
                                        Patient_ID = Convert.ToInt32(reader["Patient_ID"]),
                                        Patient_Name = reader["Patient_Name"].ToString(),
                                        Patient_Email = reader["Patient_Email"].ToString()
                                    }
                                };

                                approvedAppointmentRequests.Add(appointment);
                            }

                            return Ok(approvedAppointmentRequests);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }



    }
}