using System.Collections.Generic;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using BusinessLogicLayer.Services;
using DataAccessLayer.Repository.IRepository_Interface;
using System.Data;

namespace BusinessLogicLayer.Services
{
    public class DoctorsService : IDoctorsService
    {
        private readonly IRepository<Doctors> _doctorsRepository;

        public DoctorsService(IRepository<Doctors> doctorsRepository)
        {
            _doctorsRepository = doctorsRepository;
        }

        public async Task<IEnumerable<Doctors>> GetAllDoctorsAsync()
        {
            List<Doctors> doctorsList = new List<Doctors>();
            var doctorsData = await _doctorsRepository.GetAllDoctorsAsync();

            foreach (DataRow row in doctorsData.Rows)
            {
                Doctors doctor = new Doctors
                {
                    Doctor_ID = Convert.ToInt32(row["Doctor_ID"]),
                    Doctor_Name = row["Doctor_Name"]?.ToString() ?? "",
                    Doctor_Gender = row["Doctor_Gender"]?.ToString() ?? "",
                    Doctor_DateOfBirth = Convert.ToDateTime(row["Doctor_DateOfBirth"]),
                    Doctor_Email = row["Doctor_Email"]?.ToString() ?? "",
                    Doctor_Phone = row["Doctor_Phone"]?.ToString() ?? "",
                    Doctor_Location = row["Doctor_Location"]?.ToString() ?? "",
                    Doctor_Specialization = row["Doctor_Specialization"]?.ToString() ?? "",
                    Doctor_Password = row["Doctor_Password"]?.ToString() ?? "",
                    Doctor_Status = row["Doctor_Status"]?.ToString() ?? ""
                };

                doctorsList.Add(doctor);
            }

            return doctorsList;
        }


        public async Task<Doctors> GetDoctorByIdAsync(int id)
        {
            Doctors doctors = new Doctors();

            DataTable dataTable = await _doctorsRepository.GetDoctorByIdAsync(id);

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];

                doctors.Doctor_ID = row.Field<int>("Doctor_ID");
                doctors.Doctor_Name = row.Field<string>("Doctor_Name") ?? string.Empty;
                doctors.Doctor_Gender = row.Field<string>("Doctor_Gender") ?? string.Empty;
                doctors.Doctor_DateOfBirth = row.Field<DateTime>("Doctor_DateOfBirth");
                doctors.Doctor_Email = row.Field<string>("Doctor_Email") ?? string.Empty;
                doctors.Doctor_Phone = row.Field<string>("Doctor_Phone") ?? string.Empty;
                doctors.Doctor_Location = row.Field<string>("Doctor_Location") ?? string.Empty;
                doctors.Doctor_Specialization = row.Field<string>("Doctor_Specialization") ?? string.Empty;
                doctors.Doctor_Password = row.Field<string>("Doctor_Password") ?? string.Empty;
                doctors.Doctor_Status = row.Field<string>("Doctor_Status") ?? string.Empty;
            }

            return doctors;
        }

     

        public void InsertDoctor(Doctors doctor)
        {
            _doctorsRepository.Insert(doctor);
        }

        public void UpdateDoctor(int id, Doctors doctor)
        {
            _doctorsRepository.Update(id, doctor);
        }

        public void DeleteDoctor(int id)
        {
            _doctorsRepository.Delete(id);
        }

    }
}
