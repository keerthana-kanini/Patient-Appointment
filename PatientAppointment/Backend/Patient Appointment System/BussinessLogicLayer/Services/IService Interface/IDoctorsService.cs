using System.Collections.Generic;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services
{
    public interface IDoctorsService
    {
        Task<IEnumerable<Doctors>> GetAllDoctorsAsync();
        Task<Doctors> GetDoctorByIdAsync(int id);
        void InsertDoctor(Doctors doctor);
        void UpdateDoctor(int id, Doctors doctor);  
        void DeleteDoctor(int id);

    }
}
