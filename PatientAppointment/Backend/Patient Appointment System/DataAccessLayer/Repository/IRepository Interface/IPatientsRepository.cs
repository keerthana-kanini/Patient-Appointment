using DataAccessLayer.Models;

namespace DataAccessLayer.Repository.IRepository_Interface
{
    public interface IPatientsRepository
    {
        IEnumerable<Patients> GetAll();
        Patients GetById(int id);
        Patients GetByEmail(string email);
        void Insert(Patients entity);
        void Update(int id, Patients entity);
        void Delete(int id);
    }
}
