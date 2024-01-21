using Api.Features.Employees;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests;

public class EmployeesControllerTests
{
  private readonly Mock<ISender> _senderMock;
  private readonly EmployeesController _employeesController;

  public EmployeesControllerTests()
  {
    _senderMock = new Mock<ISender>();
    _employeesController = new EmployeesController(_senderMock.Object);
  }

  [Fact]
  public async Task GetSingleEmployeeAsyncReturnsOk()
  {
    // Arrange
    _ = _senderMock.Setup(x => x.Send(It.IsAny<GetEmployee.Query>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(() => new GetEmployee.Result
      {
        Id = 1,
        FirstName = "Peter",
        LastName = "Rabbit"
      });

    // Act
    var actionResult = await _employeesController.GetEmployeeAsync(
        "1",
        new CancellationToken());

    // Assert
    var result = actionResult.Result as OkObjectResult;
    result.Should().NotBeNull();
    result?.Value.As<GetEmployee.Result>().Id.Should().Be(1);
  }
}
