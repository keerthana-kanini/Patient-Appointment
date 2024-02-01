using BussinessLogicLayer.Services.IService_Interface;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.IRepository_Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BussinessLogicLayer.Services.Service_Class
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsRepository _repository;
        private readonly string _connectionString;

        public PatientsService(IPatientsRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Patients> GetAllPatients()
        {
            return _repository.GetAll();
        }

        public Patients GetPatientById(int id)
        {
            return _repository.GetById(id);
        }

        public Patients GetPatientByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }

        public void InsertPatient(Patients patient)
        {
            _repository.Insert(patient);
        }

        public void UpdatePatient(int id, Patients patient)
        {
            _repository.Update(id, patient);
        }

        public void DeletePatient(int id)
        {
            _repository.Delete(id);
        }

        public void UpdatePatientPassword(string email, string newPassword)
        {
            var patient = _repository.GetByEmail(email);
            if (patient != null)
            {
                
                UpdateTemporaryPassword(email, newPassword);
            }
        }

        private void UpdateTemporaryPassword(string email, string temporaryPassword)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UpdateTemporaryPassword";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Patient_Email", email);
                    command.Parameters.AddWithValue("@Temporary_Password", temporaryPassword);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
