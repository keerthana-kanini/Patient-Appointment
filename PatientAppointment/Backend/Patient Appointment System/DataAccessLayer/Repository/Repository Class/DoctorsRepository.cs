using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.IRepository_Interface;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Repositories
{
    public class DoctorsRepository : IRepository<Doctors>
    {
        private readonly IConfiguration _configuration;
        private readonly IMailRepository mail;

        public DoctorsRepository(IConfiguration configuration, IMailRepository _mail)
        {
            _configuration = configuration;
            mail = _mail;
        }

       public async Task<DataTable> GetAllDoctorsAsync()

        {
            DataTable doctors = new DataTable();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("GetAllDoctors", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        doctors.Load(reader);
                    }
                }
            }

            return doctors;
        }

        public async Task<DataTable> GetDoctorByIdAsync(int id)
        {
            DataTable doctor = new DataTable();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("GetDoctorById", connection))

                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Doctor_ID", id);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        doctor.Load(reader);
                    }
                }
            }

            return doctor;
        }

        public void Insert(Doctors doctor)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("InsertDoctor", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    AddDoctorParameters(cmd, doctor);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(int id, Doctors doctor)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("UpdateDoctor", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    AddDoctorParameters(cmd, doctor);
                    cmd.Parameters.AddWithValue("@Doctor_ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteDoctor", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Doctor_ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void AddDoctorParameters(SqlCommand cmd, Doctors doctor)
        {
            cmd.Parameters.AddWithValue("@Doctor_Name", doctor.Doctor_Name);
            cmd.Parameters.AddWithValue("@Doctor_Gender", doctor.Doctor_Gender);
            cmd.Parameters.AddWithValue("@Doctor_DateOfBirth", doctor.Doctor_DateOfBirth);
            cmd.Parameters.AddWithValue("@Doctor_Email", doctor.Doctor_Email);
            cmd.Parameters.AddWithValue("@Doctor_Phone", doctor.Doctor_Phone);
            cmd.Parameters.AddWithValue("@Doctor_Location", doctor.Doctor_Location);
            cmd.Parameters.AddWithValue("@Doctor_Specialization", doctor.Doctor_Specialization);
            cmd.Parameters.AddWithValue("@Doctor_Password", doctor.Doctor_Password);
            cmd.Parameters.AddWithValue("@Doctor_Status", doctor.Doctor_Status);
        }

        public Patients GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
