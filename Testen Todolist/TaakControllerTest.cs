using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Runtime.CompilerServices;
using ToDoList.API;
using ToDoList.Data;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace Testen_Todolist
{
    public class TaakControllerTest
    {

        private readonly TaakController _sut;
        private readonly Mock<ITaskRepository> _taskRepositoryMock = new Mock<ITaskRepository>();
       
        public TaakControllerTest()
        {
            _sut = new TaakController(_taskRepositoryMock.Object, null);
        }

        [Fact]
        public async Task GetTaken_ShouldReturnTaken_WhenTaakExists()
        {
            // Arrange
            int taakId = 1;
            var taak = new Taak { Id = taakId, Title = "Test Taak", Description = "Test Beschrijving" };

            _taskRepositoryMock.Setup(repo => repo.GetByIdAsync(taakId))
                               .ReturnsAsync(taak);

            // *Act
            var result = await _sut.GetTaak(taakId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Taak>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedTaak = Assert.IsType<Taak>(okResult.Value);

            Assert.Equal(taak.Id, returnedTaak.Id);
            Assert.Equal(taak.Title, returnedTaak.Title);
            Assert.Equal(taak.Description, returnedTaak.Description);

        }


        [Fact]
        public async Task GetTaak_ShouldReturnNotFound_WhenTaakDoesNotExist()
        {
            // Arrange
            int taakId = 2;

            _taskRepositoryMock.Setup(repo => repo.GetByIdAsync(taakId))
                               .ReturnsAsync((Taak)null);

            // Act
            var result = await _sut.GetTaak(taakId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Taak>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);

        }


        [Fact]
        public async Task GetTaken_ShouldReturnTaken_WhenTakenExist()
        {

            //Arange
            var taak1 = new Taak{Id=1, Title="taak1"};
            var taak2 = new Taak { Id  = 2, Title="taak2"};
            var taak3 = new Taak { Id = 3, Title="taak3"};
            List<Taak> taken = new List<Taak> {taak1,taak2, taak3};    
            
            _taskRepositoryMock.Setup(repo => repo.GetAllAsync())
                    .ReturnsAsync(taken);
            //Act
            var result = await _sut.GetTaken();

            //Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Taak>>>(result);
            var okresult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var takenFromList = Assert.IsAssignableFrom<IEnumerable<Taak>>(okresult.Value);

            var  takenlijst = takenFromList.ToList();

            Assert.Equal(taken.Count, takenlijst.Count);
            for(int i =0; i< taken.Count; i++)
            {
                Assert.Equal(taken[i].Id , takenlijst[i].Id);
                Assert.Equal(taken[i].Title, takenlijst[i].Title);

            }
        }

        [Fact]
        public async Task PostTaak_ShouldReturnNewTaak_WhenModelStateIsValid()
        {

            //Arrange
            var moqTaak= new Taak { Id =0, Title="Test", Description = "Test"};
            
            _taskRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Taak>()))
                .ReturnsAsync(true);
            //Act
            var taak = await _sut.PostTaak(moqTaak);

            var actionResult = Assert.IsType<ActionResult<Taak>>(taak);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var taakFromPost = Assert.IsType<Taak>(okResult.Value);
            //Assert
            Assert.NotNull(taakFromPost);
            Assert.Equal(moqTaak.Title, taakFromPost.Title);
            Assert.Equal(moqTaak.Description, taakFromPost.Description);
            Assert.IsType<Taak>(taakFromPost);

        }

        [Fact]
        public async Task PostTaak_ShouldReturnBadRequest_WhenTaakIsNull()
        {
            //Arrange
            Taak invalidTaak = null;

            _taskRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Taak>()))
                .ReturnsAsync(false);

            //Act

            var invalidResult = await _sut.PostTaak(invalidTaak);

            //Assert

            var actionResult = Assert.IsType<ActionResult<Taak>>(invalidResult);
            var badResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            Assert.NotNull(badResult);
           
        }

        [Fact]
        public async Task UpdateTaak_ShouldReturnOk_WhenValidTaakGiven()
        {
            //Arrange
            var taak = new Taak { Id = 1, Title = "Test", Description = "Test" };

            _taskRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Taak>()))
               .ReturnsAsync(true);

            //Act
            var taakUpdate = await _sut.UpdateTaak(1, taak);

            //Assert
            var okResult = Assert.IsType<OkResult>(taakUpdate);

            Assert.NotNull(okResult);
        }

        [Fact]
        public async Task UpdateTaak_ShouldReturnNotFound_WhenInvalidTaskAndIdGiven()
        {
            //Arrange
            var taak = new Taak { Id = 1, Title = "Test", Description = "Test" };

            _taskRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Taak>()))
                .ReturnsAsync(false);

            //Act

            var taakUpdate = _sut.UpdateTaak(1, taak);

            //Assert
            var actionResult = Assert.IsType<Task<ActionResult>>(taakUpdate);
            var badResult = Assert.IsType<NotFoundResult>(taakUpdate.Result);

            Assert.NotNull(badResult);
        }

        [Fact]
        public async Task UpdateTaak_ShouldReturnBadRequest_WhenOIdDoesNotMatchTaakId()
        {
            //Arrange
            var taakInvalid = new Taak { Id = 1, Title = "", Description = "" };


            //Act
            var taakUpdate = await _sut.UpdateTaak(2,taakInvalid);

            //Assert

            var badRequestResult = Assert.IsType<BadRequestResult>(taakUpdate);

            Assert.NotNull(badRequestResult);

        }

        public async Task UpdateTaak_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            //Arrange
            var taak = new Taak { Id = 1, Title = "", Description = "Test" };
            _sut.ModelState.AddModelError("Title", "Required");

            //Act 
            var TaakUpdate = await _sut.UpdateTaak(1,taak);
             
            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(TaakUpdate);

            Assert.NotNull(badRequestResult);
        }


        [Fact]
        public async Task DeleteTaak_ShouldReturnOk_WhenValidIdGiven()
        {
            //Arrange
            int taakId = 2;

            _taskRepositoryMock.Setup(repo => repo.DeleteAsync(taakId))
                .ReturnsAsync(true);

            //Act
            var TaakDelete = await _sut.DeleteTaak(taakId);


            //Assert
            var okResult = Assert.IsType<OkResult>(TaakDelete);

            Assert.NotNull(okResult);

        }

        [Fact]
        public async Task DeleteTaak_ShouldReturnNotFound_WhenInvalidIdGiven()
        {
            //Arrange

            int taakId = 0;

            _taskRepositoryMock.Setup(repo=> repo.DeleteAsync(taakId))
                .ReturnsAsync(false);

            //Act

            var taakDelete = await _sut.DeleteTaak(taakId);

            var notFoundResult = Assert.IsType<NotFoundResult>(taakDelete);

            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task DeleteTaak_ShouldReturnBadRequest_WhenInvalidIdIdGiven()
        {
            //Arrange

            int taakId = 0123;
            _taskRepositoryMock.Setup(repo => repo.DeleteAsync(taakId))
                .ThrowsAsync(new Exception("Test exception"));

            //Act

            var taakDelete = await _sut.DeleteTaak(taakId);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(taakDelete);

            Assert.NotNull(badRequestResult);
            Assert.Equal("Test exception", badRequestResult.Value);
        }
    }
    
}