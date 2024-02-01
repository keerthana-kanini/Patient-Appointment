using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.IRepository_Interface;
using Microsoft.AspNetCore.Mvc;


namespace Patient_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrontEndExecutiveController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMailRepository _mailService;

        public FrontEndExecutiveController(IConfiguration configuration, IMailRepository mailService)
        {
            _configuration = configuration;
            _mailService = mailService;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<FrontEndExecutive>> GetFrontEndExecutives()
        {
            List<FrontEndExecutive> executives = new List<FrontEndExecutive>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("GetFrontEndExecutives", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FrontEndExecutive executive = new FrontEndExecutive
                            {
                                Executive_ID = Convert.ToInt32(reader["Executive_ID"]),
                                Executive_Name = reader["Executive_Name"].ToString(),
                                Executive_Email = reader["Executive_Email"].ToString(),
                                Executive_Password = reader["Executive_Password"].ToString()
                            };

                            executives.Add(executive);
                        }
                    }
                }
            }

            return executives;
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<FrontEndExecutive> GetFrontExecutivesById(int id)
        {
            FrontEndExecutive executive = new FrontEndExecutive();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("GetFrontExecutivesById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    command.Parameters.AddWithValue("@Executive_ID", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            executive.Executive_ID = Convert.ToInt32(reader["Executive_ID"]);
                            executive.Executive_Name = reader["Executive_Name"].ToString();
                            executive.Executive_Email = reader["Executive_Email"].ToString();
                            executive.Executive_Password = reader["Executive_Password"].ToString();
                        }
                    }
                }
            }

            return executive;
        }

        [HttpPost("Insert")]
        public IActionResult InsertFrontEndExecutives([FromBody] FrontEndExecutive executive)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("InsertFrontEndExecutives", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Executive_Name", executive.Executive_Name);
                    command.Parameters.AddWithValue("@Executive_Email", executive.Executive_Email);
                    command.Parameters.AddWithValue("@Executive_Password", executive.Executive_Password);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return Ok();
        }


        [HttpPut("Update/{id}")]
        public IActionResult UpdateFrontEndExecutive(int id, [FromBody] FrontEndExecutive executive)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("UpdateFrontEndExecutive", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Executive_ID", id);
                    command.Parameters.AddWithValue("@Executive_Name", executive.Executive_Name);
                    command.Parameters.AddWithValue("@Executive_Email", executive.Executive_Email);
                    command.Parameters.AddWithValue("@Executive_Password", executive.Executive_Password);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteFrontEndExecutive(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("DeleteFrontEndExecutive", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Executive_ID", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            return Ok();
        }
        [HttpGet("DoctorRequests")]
        public ActionResult<IEnumerable<Doctors>> GetDoctorRequests()
        {
            List<Doctors> doctorRequests = new List<Doctors>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("GetDoctorRequests", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Doctors doctor = new Doctors
                            {
                                Doctor_ID = Convert.ToInt32(reader["Doctor_ID"]),
                                Doctor_Name = reader["Doctor_Name"].ToString(),
                                Doctor_Gender = reader["Doctor_Gender"].ToString(),
                                Doctor_DateOfBirth = Convert.ToDateTime(reader["Doctor_DateOfBirth"]),
                                Doctor_Email = reader["Doctor_Email"].ToString(),
                                Doctor_Phone = reader["Doctor_Phone"].ToString(),
                                Doctor_Location = reader["Doctor_Location"].ToString(),
                                Doctor_Specialization = reader["Doctor_Specialization"].ToString(),
                                Doctor_Password = reader["Doctor_Password"].ToString(),
                                Doctor_Status = reader["Doctor_Status"].ToString()
                            };

                            doctorRequests.Add(doctor);
                        }
                    }
                }
            }

            return Ok(doctorRequests);
        }



        [HttpPost("ApproveDoctorRequest/{id}")]
        public IActionResult ApproveDoctorRequest(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("ApproveDoctorRequest", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DoctorID", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }

                  
                    string doctorEmail = GetDoctorEmailById(id);

                    if (!string.IsNullOrEmpty(doctorEmail))
                    {
                        
                        _mailService.SendApprovalEmaildoctors(doctorEmail, "approve");
                    }
                }
            }

            return NoContent();
        }

        [HttpPost("DeclineDoctorRequest/{id}")]
        public IActionResult DeclineDoctorRequest(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("DeclineDoctorRequest", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DoctorID", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }

                   
                    string doctorEmail = GetDoctorEmailById(id);

                    if (!string.IsNullOrEmpty(doctorEmail))
                    {
                      
                        _mailService.SendApprovalEmaildoctors(doctorEmail, "decline");
                    }
                }
            }

            return NoContent();
        }

        private string GetDoctorEmailById(int id)
        {
            string doctorEmail = string.Empty;

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT Doctor_Email FROM Doctors WHERE Doctor_ID = @DoctorID", connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doctorEmail = reader["Doctor_Email"].ToString();
                        }
                    }
                }
            }

            return doctorEmail;
        }


        [HttpGet("PatientRequests")]
        public ActionResult<IEnumerable<Patients>> GetPatientRequests()
        {
            List<Patients> patientRequests = new List<Patients>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("GetPatientRequests", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Patients patient = new Patients
                            {
                                Patient_ID = Convert.ToInt32(reader["Patient_ID"]),
                                Patient_Name = reader["Patient_Name"].ToString(),
                                Patient_Gender = reader["Patient_Gender"].ToString(),
                                Patient_DateOfBirth = Convert.ToDateTime(reader["Patient_DateOfBirth"]),
                                Patient_Email = reader["Patient_Email"].ToString(),
                                Patient_Phone = reader["Patient_Phone"].ToString(),
                                Patient_Location = reader["Patient_Location"].ToString(),
                                Patient_Password = reader["Patient_Password"].ToString(),
                                Patient_Status = reader["Patient_Status"].ToString()
                            };

                            patientRequests.Add(patient);
                        }
                    }
                }
            }

            return Ok(patientRequests);
        }

        [HttpPost("ApprovePatientRequest/{id}")]
        public IActionResult ApprovePatientRequest(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ApprovePatientRequest", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PatientID", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            return NotFound("Patient not found with the provided ID.");
                        }

                       
                        string patientEmail = GetPatientEmailById(id);

                        if (!string.IsNullOrEmpty(patientEmail))
                        {
                            
                            _mailService.SendApprovalOrDeclineEmail(patientEmail, isApproved: true);
                        }

                        return NoContent();
                    }
                }
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost("DeclinePatientRequest/{id}")]
        public IActionResult DeclinePatientRequest(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("DeclinePatientRequest", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PatientID", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            return NotFound("Patient not found with the provided ID.");
                        }

                       
                        string patientEmail = GetPatientEmailById(id);

                        if (!string.IsNullOrEmpty(patientEmail))
                        {
                            
                            _mailService.SendApprovalOrDeclineEmail(patientEmail, isApproved: false);
                        }

                        return NoContent();
                    }
                }
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("Searchpending")]
        public IActionResult GetPendingRequests()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SearchPendingRequests", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }


            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            foreach (DataRow row in dataTable.Rows)
            {
                var rowData = new Dictionary<string, object>();

                foreach (DataColumn col in dataTable.Columns)
                {
                    rowData[col.ColumnName] = row[col];
                }

                result.Add(rowData);
            }

            return Ok(result);
        }

        [HttpGet("DoctorRequests/{status}")]
        public ActionResult<IEnumerable<Doctors>> GetDoctorRequestsByStatus(string status)
        {
            if (string.IsNullOrEmpty(status) || (!status.Equals("Approved", StringComparison.OrdinalIgnoreCase) && !status.Equals("Declined", StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("Invalid status. Please provide 'Approved' or 'Declined'.");
            }

            List<Doctors> doctorRequests = new List<Doctors>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("GetDoctorRequestsByStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Status", status);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Doctors doctor = new Doctors
                            {
                                Doctor_ID = Convert.ToInt32(reader["Doctor_ID"]),
                                Doctor_Name = reader["Doctor_Name"].ToString(),
                                Doctor_Gender = reader["Doctor_Gender"].ToString(),
                                Doctor_DateOfBirth = Convert.ToDateTime(reader["Doctor_DateOfBirth"]),
                                Doctor_Email = reader["Doctor_Email"].ToString(),
                                Doctor_Phone = reader["Doctor_Phone"].ToString(),
                                Doctor_Location = reader["Doctor_Location"].ToString(),
                                Doctor_Specialization = reader["Doctor_Specialization"].ToString(),
                                Doctor_Password = reader["Doctor_Password"].ToString(),
                                Doctor_Status = reader["Doctor_Status"].ToString()
                            };

                            doctorRequests.Add(doctor);
                        }
                    }
                }
            }

            return Ok(doctorRequests);
        }

        [HttpGet("PatientRequestsStatus")]
        public ActionResult<IEnumerable<Patients>> GetPatientRequests([FromQuery] string status)
        {
            List<Patients> patientRequests = new List<Patients>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("GetPatientRequestsByStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Status", status); 
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Patients patient = new Patients
                            {
                                Patient_ID = Convert.ToInt32(reader["Patient_ID"]),
                                Patient_Name = reader["Patient_Name"].ToString(),
                                Patient_Gender = reader["Patient_Gender"].ToString(),
                                Patient_DateOfBirth = Convert.ToDateTime(reader["Patient_DateOfBirth"]),
                                Patient_Email = reader["Patient_Email"].ToString(),
                                Patient_Phone = reader["Patient_Phone"].ToString(),
                                Patient_Location = reader["Patient_Location"].ToString(),
                                Patient_Password = reader["Patient_Password"].ToString(),
                                Patient_Status = reader["Patient_Status"].ToString()
                            };

                            patientRequests.Add(patient);
                        }
                    }
                }
            }

            return Ok(patientRequests);
        }
        private string GetPatientEmailById(int id)
        {
            string patientEmail = string.Empty;

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT Patient_Email FROM Patients WHERE Patient_ID = @PatientID", connection))
                {
                    command.Parameters.AddWithValue("@PatientID", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            patientEmail = reader["Patient_Email"].ToString();
                        }
                    }
                }
            }

            return patientEmail;
        }




    }
}