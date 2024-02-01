using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Services.IService_Interface
{
    public interface IPatientsService
    {
        IEnumerable<Patients> GetAllPatients();
        Patients GetPatientById(int id);
        Patients GetPatientByEmail(string email);
        void InsertPatient(Patients patient);
        void UpdatePatient(int id, Patients patient);
        void DeletePatient(int id);
        void UpdatePatientPassword(string email, string newPassword);
    }

}
