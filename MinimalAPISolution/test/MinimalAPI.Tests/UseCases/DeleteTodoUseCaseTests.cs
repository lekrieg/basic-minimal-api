using Microsoft.Extensions.Logging;
using MinimalAPI.Data.Entities;
using MinimalAPI.Data.Repositories;
using MinimalAPI.Dtos;
using MinimalAPI.UseCases;
using Moq;
using System;
using System.Threading.Tasks;

namespace MinimalAPI.Tests.UseCases;

public class DeleteTodoUseCaseTests : IDisposable
{
    private Mock<ILogger<DeleteTodoUseCase>> _loggerMock;
    private Mock<ITodoRepository> _repositoryMock;
    private DeleteTodoUseCase _useCase;

    public DeleteTodoUseCaseTests()
    {
        _loggerMock = new Mock<ILogger<DeleteTodoUseCase>>();
        _repositoryMock = new Mock<ITodoRepository>();

        _useCase = new DeleteTodoUseCase(_loggerMock.Object, _repositoryMock.Object);
    }

    public void Dispose()
    {
        _loggerMock = null;
        _repositoryMock = null;
        _useCase = null;
    }

    [Fact]
    public async Task Execute_Should_ReturnTodoById_When_Called()
    {
        var todoDTO = new TodoDTO() { IsComplete = true, Name = "Task 1" };
        var todo = new Todo() { IsComplete = true, Name = "Task 1" };

        _repositoryMock.Setup(x => x.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);
        _repositoryMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _useCase.Execute(1);

        _repositoryMock.Verify(x => x.DeleteAsync(1), Times.Once);
        _repositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}