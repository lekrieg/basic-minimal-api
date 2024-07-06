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

public class GetByIdTodoUseCaseTests : IDisposable
{
    private IMapper _mapper;
    private Mock<ILogger<GetByIdTodoUseCase>> _loggerMock;
    private Mock<ITodoRepository> _repositoryMock;
    private GetByIdTodoUseCase _useCase;

    public GetByIdTodoUseCaseTests()
    {
        var autoMapperProfile = new MappingProfile();
        var config = new MapperConfiguration(cfg => cfg.AddProfile(autoMapperProfile));
        _mapper = new Mapper(config);

        _loggerMock = new Mock<ILogger<GetByIdTodoUseCase>>();
        _repositoryMock = new Mock<ITodoRepository>();

        _useCase = new GetByIdTodoUseCase(_loggerMock.Object, _mapper, _repositoryMock.Object);
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
        var todo = new Todo() { IsComplete = true, Name = "Task 1" };

        _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(todo);

        var result = await _useCase.Execute(1);

        Assert.NotNull(result);
        Assert.IsType<TodoDTO?>(result);
        Assert.True(result.IsComplete);
    }

    [Fact]
    public async Task Execute_Should_ThrowException_When_Called()
    {
        _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<Exception>(async () => await _useCase.Execute(1));
    }
}