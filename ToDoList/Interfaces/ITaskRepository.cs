using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITaskRepository
    {

        Task<List<Taak>> GetAllAsync();
        Task<Taak> GetByIdAsync(int id);
        Task<bool> AddAsync(Taak taak);
        Task<bool> UpdateAsync(Taak taak);
        Task<bool> DeleteAsync(int id);

    }
}
