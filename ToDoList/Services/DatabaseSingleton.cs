using Microsoft.EntityFrameworkCore;
using ToDoList.Data;

namespace ToDoList.Services
{
    public class DatabaseSingleton
    {

        private static DatabaseSingleton _instance;

        private static readonly object _lock = new object();    
        public ToDoListDBContext DbContext { get; private set; }


        private DatabaseSingleton(DbContextOptions<ToDoListDBContext> options)
        {
            DbContext = new ToDoListDBContext(options);
        }

        public static DatabaseSingleton GetInstance(DbContextOptions<ToDoListDBContext> options)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new DatabaseSingleton(options);
                }
                return _instance;
            }
        }


    }
}
