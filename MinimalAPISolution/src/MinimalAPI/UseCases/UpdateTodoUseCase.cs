using AutoMapper;
using Microsoft.Extensions.Logging;
using MinimalAPI.Data.Entities;
using MinimalAPI.Data.Repositories;
using MinimalAPI.Dtos;
using MinimalAPI.UseCases.Interfaces;
using System;
using System.Threading.Tasks;

namespace MinimalAPI.UseCases;

public class UpdateTodoUseCase : IUpdateTodoUseCase
{
    private readonly ILogger<UpdateTodoUseCase> _logger;
    private readonly IMapper _mapper;

    private readonly ITodoRepository _repository;

    public UpdateTodoUseCase(ILogger<UpdateTodoUseCase> logger, IMapper mapper, ITodoRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task Execute(TodoDTO request)
    {
        try
        {
            var t = _mapper.Map<Todo>(request);
            await _repository.UpdateAsync(t);
            await _repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[UpdateTodoUseCase:Execute] {ex.Message}");
            throw;
        }
    }
}
