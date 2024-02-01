using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;


namespace Patient_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public LoginsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost("Doctor")]
        public async Task<IActionResult> PostDoctor(Doctors _userData)
        {
            try
            {
                if (_userData != null && !string.IsNullOrEmpty(_userData.Doctor_Email) && !string.IsNullOrEmpty(_userData.Doctor_Password))
                {
                    var isDoctorValid = await CheckDoctorCredentials(_userData.Doctor_Email, _userData.Doctor_Password);
                    Doctors doc = CheckDoctorStatus(_userData.Doctor_Email, _userData.Doctor_Password);

                    if (isDoctorValid)
                    {
                        return Ok(new
                        {
                            id = _userData.Doctor_ID,
                            status = doc.Doctor_Status,
                            message = "Doctor login successful"
                        });
                    }
                    else
                    {
                        return BadRequest("Invalid credentials");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in PostDoctor: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        private Doctors CheckDoctorStatus(string doctorEmail, string doctorPassword)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.OpenAsync();
                Doctors doctors = new Doctors();
                using (SqlCommand command = new SqlCommand("GetStatusofDoctor", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Doctor_Email", doctorEmail);
                    command.Parameters.AddWithValue("@Doctor_Password", doctorPassword);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            doctors = MapToDoctor(reader);
                        }
                    }


                    return doctors;
                }
            }
        }
        private Doctors MapToDoctor(SqlDataReader reader)
        {
            return new Doctors
            {
                Doctor_Status = reader["Doctor_Status"].ToString()
                
            };
        }
        private async Task<bool> CheckDoctorCredentials(string doctorEmail, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("CheckDoctorCredentials", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Doctor_Email", doctorEmail);
                    command.Parameters.AddWithValue("@Doctor_Password", password);

                    int count = (int)await command.ExecuteScalarAsync();

                    return count > 0;
                }
            }
        }

        [HttpPost("Admin")]
        public async Task<IActionResult> PostAdmin(FrontEndExecutive _adminData)
        {
            try
            {
                if (_adminData != null && !string.IsNullOrEmpty(_adminData.Executive_Email) && !string.IsNullOrEmpty(_adminData.Executive_Password))
                {
                    var isAdminValid = await CheckAdminCredentials(_adminData.Executive_Email, _adminData.Executive_Password);

                    if (isAdminValid)
                    {
                        return Ok("Admin login successful");
                    }
                    else
                    {
                        return BadRequest("Invalid credentials");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in PostAdmin: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        private async Task<bool> CheckAdminCredentials(string executiveEmail, string executivePassword)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("CheckAdminCredentials", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Executive_Email", executiveEmail);
                    command.Parameters.AddWithValue("@Executive_Password", executivePassword);

                    int count = (int)await command.ExecuteScalarAsync();

                    return count > 0;
                }
            }
        }


        [HttpPost("Patient")]
        public async Task<IActionResult> PostPatient(Patients _patientData)
        {
            
                if (_patientData != null && !string.IsNullOrEmpty(_patientData.Patient_Email) && !string.IsNullOrEmpty(_patientData.Patient_Password))
                {
                    var isPatientValid = await CheckPatientCredentials(_patientData.Patient_Email, _patientData.Patient_Password);
                    Patients pat = CheckPatientStatus(_patientData.Patient_Email,_patientData.Patient_Password);
                    if (isPatientValid)
                    {
                        return Ok(new
                        {
                            id=_patientData.Patient_ID,
                            is_fstlogin = pat.IsFirstLogin,
                            status=pat.Patient_Status,
                            message= "Patient login successful"
                        });
                    }
                    else
                    {
                        return BadRequest("Invalid credentials");
                    }
                }
                else
                {
                    return BadRequest();
                }
           
        }

        private async Task<bool> CheckPatientCredentials(string patientEmail, string patientPassword)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("CheckPatientCredentials", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Patient_Email", patientEmail);
                    command.Parameters.AddWithValue("@Patient_Password", patientPassword);

                    int count = (int)await command.ExecuteScalarAsync();

                    return count > 0;
                }
            }
        }

        private Patients CheckPatientStatus(string patientEmail, string patientPassword)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                 connection.OpenAsync();
                Patients patient = new Patients();
                using (SqlCommand command = new SqlCommand("GetStatusofPatient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Patient_Email", patientEmail);
                    command.Parameters.AddWithValue("@Patient_Password", patientPassword);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patient = MapToPatient(reader);
                        }
                    }


                    return patient;
                }
            }
        }
        private Patients MapToPatient(SqlDataReader reader)
        {
            return new Patients
            {
                Patient_Status = reader["Patient_Status"].ToString(),
                IsFirstLogin = Convert.ToBoolean(reader["is_fstlogin"])
            };
        }
    }
}
