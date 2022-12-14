using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.WebApi.Application.DTOs.ValueObject;
using ToDo.WebApi.Domain;
using ToDo.WebApi.Tests.Unit.Setups.Controllers;

namespace ToDo.WebApi.Tests.Unit.Interface.Controllers.TodoItem
{
    public class ControllerUpdateTests
    {
        [Fact]
        public void Update_OnFailure_ReturnsProblemDetails_WithStatus500()
        {
            //Arrange
            var sut = TodoItemControllerSetups.UpdateOnFailureReturnsUpdateFailedError();

            //Act
            var response = sut.Update(new(3, "", null, null));

            //Assert
            response.Should().BeOfType<ObjectResult>();

            var result = (ObjectResult)response;
            result.Value.Should().BeOfType<ProblemDetails>();
            result.StatusCode.Should().Be((int)StatusCodes.Status500InternalServerError);

            var problemDetails = (ProblemDetails)result.Value!;
            problemDetails.Status.Should().Be((int)StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public void Update_OnSuccess_ReturnsTodoItem()
        {
            //Arrange
            var (sut, account) = TodoItemControllerSetups.UpdateOnSuccessReturnsTodoItem();

            //Act
            var response = sut.Update(new(3, "", null, null));

            //Assert
            response.Should().BeOfType<OkObjectResult>();
            var accountR = (OkObjectResult)response;
            accountR.Value.Should().BeOfType<WebApi.Domain.Entities.TodoItem>();
            accountR.Value.Should().BeEquivalentTo(account);
        }
    }
}
