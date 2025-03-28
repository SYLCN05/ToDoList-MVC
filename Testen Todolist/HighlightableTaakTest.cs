using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Factories;
using ToDoList.Services;

namespace Testen_Todolist
{
    public class HighlightableTaakTest
    {

        [Fact]
        public void HighlightableTask_ShouldInitializeWithDefaultValues_WhenTaakCreateExecuted()
        {
            // Arrange
            var mockTask = new Mock<ITask>();
            var highlightableTask = new HighlightableTask(mockTask.Object);

            // Act & Assert
            Assert.False(highlightableTask.IsHighlighted);
            Assert.Equal(mockTask.Object.Id, highlightableTask.Id);
            Assert.Equal(mockTask.Object.Title, highlightableTask.Title);
            Assert.Equal(mockTask.Object.Description, highlightableTask.Description);
            Assert.Equal(mockTask.Object.CreatedDateTime, highlightableTask.CreatedDateTime);
        }
    }
}
