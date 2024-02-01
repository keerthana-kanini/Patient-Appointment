using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.IRepository_Interface;
using Microsoft.Extensions.Configuration;

public class PatientsRepository : IPatientsRepository
{
    private readonly string _connectionString;
    private readonly IMailRepository mail;
    public PatientsRepository(IConfiguration configuration, IMailRepository _mail)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        mail = _mail;
    }

    public IEnumerable<Patients> GetAll()
    {
        List<Patients> patients = new List<Patients>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string query = "EXEC GetAllPatients";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Patients patient = MapToPatient(reader);
                        patients.Add(patient);
                    }
                }
            }
        }

        return patients;
    }

    public Patients GetById(int id)
    {
        Patients patient = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string query = "EXEC GetPatientByID @Patient_ID";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Patient_ID", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        patient = MapToPatient(reader);
                    }
                }
            }
        }

        return patient;
    }
    public Patients GetByEmail(string email)
    {
        Patients patient = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string query = "EXEC GetPatientByEmail @Patient_Email";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Patient_Email", email);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        patient = MapToPatient(reader);
                    }
                }
            }
        }

        return patient;
    }

    public void Insert(Patients entity)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string query = "EXEC InsertPatient @Patient_Name, @Patient_Gender, @Patient_DateOfBirth, @Patient_Email, @Patient_Phone, @Patient_Location, @Patient_Password, @Patient_Status";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                AddPatientParameters(command, entity);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    throw new DataException("Failed to insert patient.");
                }
            }
        }
    }

    public void Update(int id, Patients entity)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string query = "EXEC UpdatePatient @Patient_ID, @Patient_Name, @Patient_Gender, @Patient_DateOfBirth, @Patient_Email, @Patient_Phone, @Patient_Location, @Patient_Password, @Patient_Status";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                AddPatientParameters(command, entity);
                command.Parameters.AddWithValue("@Patient_ID", id);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    throw new DataException("Failed to update patient.");
                }
            }
        }
    }

    public void Delete(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string query = "EXEC DeletePatient @Patient_ID";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Patient_ID", id);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    throw new DataException("Failed to delete patient.");
                }
            }
        }
    }

    private Patients MapToPatient(SqlDataReader reader)
    {
        return new Patients
        {
            Patient_ID = (int)reader["Patient_ID"],
            Patient_Name = reader["Patient_Name"].ToString(),
            Patient_Gender = reader["Patient_Gender"].ToString(),
            Patient_DateOfBirth = (DateTime)reader["Patient_DateOfBirth"],
            Patient_Email = reader["Patient_Email"].ToString(),
            Patient_Phone = reader["Patient_Phone"].ToString(),
            Patient_Location = reader["Patient_Location"].ToString(),
            Patient_Password = reader["Patient_Password"].ToString(),
            Patient_Status = reader["Patient_Status"].ToString(),
        };
    }

    private void AddPatientParameters(SqlCommand command, Patients patient)
    {
        command.Parameters.AddWithValue("@Patient_Name", patient.Patient_Name);
        command.Parameters.AddWithValue("@Patient_Gender", patient.Patient_Gender);
        command.Parameters.AddWithValue("@Patient_DateOfBirth", patient.Patient_DateOfBirth);
        string pass = mail.GetByEmail(patient.Patient_Email);
        command.Parameters.AddWithValue("@Patient_Email", patient.Patient_Email);
        command.Parameters.AddWithValue("@Patient_Phone", patient.Patient_Phone);
        command.Parameters.AddWithValue("@Patient_Location", patient.Patient_Location);
        command.Parameters.AddWithValue("@Patient_Password", pass);
        command.Parameters.AddWithValue("@Patient_Status", patient.Patient_Status);
    }

}
