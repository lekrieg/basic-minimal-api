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

public class GetAllTodoUseCaseTests : IDisposable
{
    private IMapper _mapper;
    private Mock<ILogger<GetAllTodoUseCase>> _loggerMock;
    private Mock<ITodoRepository> _repositoryMock;
    private GetAllTodoUseCase _useCase;

    public GetAllTodoUseCaseTests()
    {
        var autoMapperProfile = new MappingProfile();
        var config = new MapperConfiguration(cfg => cfg.AddProfile(autoMapperProfile));
        _mapper = new Mapper(config);

        _loggerMock = new Mock<ILogger<GetAllTodoUseCase>>();
        _repositoryMock = new Mock<ITodoRepository>();

        _useCase = new GetAllTodoUseCase(_loggerMock.Object, _mapper, _repositoryMock.Object);
    }

    public void Dispose()
    {
        _mapper = null;
        _loggerMock = null;
        _repositoryMock = null;
        _useCase = null;
    }

    [Fact]
    public async Task Execute_Should_ReturnAllTodo_When_Called()
    {
        var todos = new List<Todo>
        {
            new Todo() { IsComplete = false, Name = "Task 1" }
        };

        _repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(todos);

        var result = await _useCase.Execute();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsType<List<TodoDTO>>(result);
    }

    [Fact]
    public async Task Execute_Should_ThrowException_When_Called()
    {
        _repositoryMock.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<Exception>(async () => await _useCase.Execute());
    }
}