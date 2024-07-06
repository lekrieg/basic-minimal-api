using AutoMapper;
using Microsoft.Extensions.Logging;
using MinimalAPI.Data.Entities;
using MinimalAPI.Data.Repositories;
using MinimalAPI.Dtos;
using MinimalAPI.Mapping;
using MinimalAPI.UseCases;
using Moq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace MinimalAPI.Tests.UseCases;

public class AddTodoUseCaseTests : IDisposable
{
    private IMapper _mapper;
    private Mock<ILogger<AddTodoUseCase>> _loggerMock;
    private Mock<ITodoRepository> _repositoryMock;
    private AddTodoUseCase _useCase;

    public AddTodoUseCaseTests()
    {
        var autoMapperProfile = new MappingProfile();
        var config = new MapperConfiguration(cfg => cfg.AddProfile(autoMapperProfile));
        _mapper = new Mapper(config);

        _loggerMock = new Mock<ILogger<AddTodoUseCase>>();
        _repositoryMock = new Mock<ITodoRepository>();

        _useCase = new AddTodoUseCase(_loggerMock.Object, _mapper, _repositoryMock.Object);
    }

    public void Dispose()
    {
        _mapper = null;
        _loggerMock = null;
        _repositoryMock = null;
        _useCase = null;
    }

    [Fact]
    public async Task Execute_Should_ReturnTodoById_When_Called()
    {
        var todoDTO = new TodoDTO() { IsComplete = true, Name = "Task 1" };
        var todo = new Todo() { IsComplete = true, Name = "Task 1" };

        _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Todo>() { todo });
        _repositoryMock.Setup(x => x.CreateAsync(It.IsAny<Todo>())).Returns(Task.CompletedTask);
        _repositoryMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

        var result = await _useCase.Execute(todoDTO);

        Assert.NotNull(result);
        Assert.IsType<TodoDTO?>(result);
        Assert.Equivalent(result, todoDTO);
    }

    [Fact]
    public async Task Execute_Should_ThrowException_When_Called()
    {
        _repositoryMock.Setup(x => x.CreateAsync(It.IsAny<Todo>())).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<Exception>(async () => await _useCase.Execute(new TodoDTO()));
    }
}