using DataAccessLayer.Models;
using System.Data;


namespace DataAccessLayer.Repository.IRepository_Interface
{
    public interface IRepository<T>
    {
        Task<DataTable> GetAllDoctorsAsync();

        Task<DataTable> GetDoctorByIdAsync(int id);

        void Insert(T entity);
        void Update(int id, T entity);
        void Delete(int id);
        Patients GetByEmail(string email);
    }
}
