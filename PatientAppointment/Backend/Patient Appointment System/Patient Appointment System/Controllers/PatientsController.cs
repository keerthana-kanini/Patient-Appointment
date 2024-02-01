using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;
using BussinessLogicLayer.Services.IService_Interface;

namespace Patient_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _patientsService;

        public PatientsController(IPatientsService patientsService)
        {
            _patientsService = patientsService;

        }

        [HttpGet]
        public IEnumerable<Patients> GetAllPatients()
        {
            return _patientsService.GetAllPatients();
        }

        [HttpGet("{id}")]
        public IActionResult GetPatientById(int id)
        {
            Patients patient = _patientsService.GetPatientById(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
        [HttpGet("email")]
        public IActionResult GetPatientByEmail(string email)
        {
            Patients patient = _patientsService.GetPatientByEmail(email);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpPost]
        public IActionResult InsertPatient([FromBody] Patients patient)
        {
            try
            {
                _patientsService.InsertPatient(patient);
                return Ok(new { message = "Patient inserted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error =ex.Message});
            }
            
        }


        [HttpPut("{id}")]
        public IActionResult UpdatePatient(int id, [FromBody] Patients patient)
        {
            _patientsService.UpdatePatient(id, patient);
            return Ok("Patient updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            _patientsService.DeletePatient(id);
            return Ok("Patient deleted successfully.");
        }
        [HttpPut("updatePassword/{email}")]
        public IActionResult UpdatePatientPassword(string email, string newPassword)
        {
            try
            {
               
                _patientsService.UpdatePatientPassword(email, newPassword);

                return Ok(new
                {
                    message = "Password updated successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }

}
