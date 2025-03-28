using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Interfaces;
using ToDoList.Models;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Services
{
    public class TaakRepository : ITaskRepository
    {
        private readonly ToDoListDBContext _context;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1,1);

        public TaakRepository(ToDoListDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Taak taak)
        {
            if(taak == null)
            {
                return false;
            }
            try
            {
                await _semaphore.WaitAsync();

                _context.Taken.Add(taak);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _semaphore.WaitAsync();

                var taak = await _context.Taken.FindAsync(id);
                if(taak == null)
                {
                    return false;
                }
                _context.Taken.Remove(taak);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                _semaphore.Release(); 
            }
        }

        public async Task<List<Taak>> GetAllAsync()
        {
            return await _context.Taken.ToListAsync();
        }

        public async Task<Taak> GetByIdAsync(int id)
        {
            return await _context.Taken.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Taak taak)
        {
            if (taak == null)
            {
                return false;
            }

            try
            {
                await _semaphore.WaitAsync();
                _context.Taken.Update(taak);   
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
            finally {
            
                _semaphore.Release(); 
            }
        }
    }
}
