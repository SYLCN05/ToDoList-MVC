using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ToDoListDBContext: DbContext
    {

        public ToDoListDBContext(DbContextOptions<ToDoListDBContext> options):base(options)
        {
            
        }

        public DbSet<Taak> Taken { get; set; }

        // dit was een eis volgens de rubric. dus ik heb wat test data toegevoegd voor de Taak model op het moment dat de DBContext wordt initialized.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Taak>().HasData(
                new Taak { Id = 1, Title = "Test Taak1", Description = "Dit is de beschrijving voor taak 1" , Type=TaakType.Normaal, CreatedDateTime= new DateTime(2025, 3,1, 19,30,0) },
                new Taak { Id = 2, Title = "Test Taak2", Description = "Dit is de beschrijving voor taak 2", Type=TaakType.Urgent, CreatedDateTime = new DateTime(2025, 3,2, 20, 30,0)},
                new Taak { Id = 3, Title = "Test Taak3", Description = "Dit is de beschrijving voor taak 3", Type=TaakType.Onzeker, CreatedDateTime = new DateTime(2025, 3,3, 21,30,0)},
                new Taak { Id = 4 , Title = "Test Taak4", Description="Je kunt het al raden dit is ook een beschrijving, maar dan voor taak 4 :)", CreatedDateTime = new DateTime(2025, 3 , 4, 22,30,0)}
                );
        }
    }
}
