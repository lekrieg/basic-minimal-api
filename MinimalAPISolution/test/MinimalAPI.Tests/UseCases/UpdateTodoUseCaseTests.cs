using AutoMapper;
using Microsoft.Extensions.Logging;
using MinimalAPI.Data.Entities;
using MinimalAPI.Data.Repositories;
using MinimalAPI.Dtos;
using MinimalAPI.Mapping;
using MinimalAPI.UseCases;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalAPI.Tests.UseCases;

public class UpdateTodoUseCaseTests : IDisposable
{
    private IMapper _mapper;
    private Mock<ILogger<UpdateTodoUseCase>> _loggerMock;
    private Mock<ITodoRepository> _repositoryMock;
    private UpdateTodoUseCase _useCase;

    public UpdateTodoUseCaseTests()
    {
        var autoMapperProfile = new MappingProfile();
        var config = new MapperConfiguration(cfg => cfg.AddProfile(autoMapperProfile));
        _mapper = new Mapper(config);

        _loggerMock = new Mock<ILogger<UpdateTodoUseCase>>();
        _repositoryMock = new Mock<ITodoRepository>();

        _useCase = new UpdateTodoUseCase(_loggerMock.Object, _mapper, _repositoryMock.Object);
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

        _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Todo>())).Returns(Task.CompletedTask);
        _repositoryMock.Setup(x => x.CreateAsync(It.IsAny<Todo>())).Returns(Task.CompletedTask);
        _repositoryMock.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

        await _useCase.Execute(todoDTO);

        _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Todo>()), Times.Once);
        _repositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Execute_Should_ThrowException_When_Called()
    {
        _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Todo>())).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<Exception>(async () => await _useCase.Execute(new TodoDTO()));
    }
}