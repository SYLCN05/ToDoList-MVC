using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace Testen_Todolist
{
    public class TaakFactoryTest
    {

        [Fact]
        public void CreateTask_ShoudNotReturnNull_WhenNormaalTypeGiven()
        {
            //Arrange
            var type = TaakType.Normaal;

            //Act
            var taak = TaakFactory.CreateTask(type);

            //Assert

            Assert.NotNull(taak);
            Assert.IsType<Taak>(taak);
        }

        [Fact]
        public void CreateTask_ShouldNotReturnNull_WhenUrgentTypeGiven()
        {
            //Arrange
            var type = TaakType.Urgent;

            //Act
            var taak = TaakFactory.CreateTask(type);

            //Assert
            Assert.NotNull(taak);
            Assert.IsType<Taak>(taak);
        }

        [Fact]
        public void CreateTask_ShouldNotReturnNull_WhenOnzekerTypeGiven()
        {
            //Arrange
            var type = TaakType.Onzeker;

            //Act
            var taak = TaakFactory.CreateTask(type);

            //Assert

            Assert.NotNull(taak);
            Assert.IsType<Taak>(taak);
        }

        [Fact]
        public void CreateTask_ShouldThrowArgumentException_WhenTypeNotGiven()
        {
            //Arrange
            var type = (TaakType)9999;

            //Act&Assert
            Assert.Throws<ArgumentException>(()=> TaakFactory.CreateTask(type));
        }
    }
}
