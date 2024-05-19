using AutoMapper;
using Microsoft.Extensions.Logging;
using MinimalAPI.Data.Entities;
using MinimalAPI.Data.Repositories;
using MinimalAPI.Dtos;
using MinimalAPI.UseCases.Interfaces;
using System;
using System.Threading.Tasks;

namespace MinimalAPI.UseCases;

public class AddTodoUseCase : IAddTodoUseCase
{
    private readonly ILogger<AddTodoUseCase> _logger;
    private readonly IMapper _mapper;

    private readonly ITodoRepository _repository;

    public AddTodoUseCase(ILogger<AddTodoUseCase> logger, IMapper mapper, ITodoRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<TodoDTO> Execute(TodoDTO request)
    {
        try
        {
            var t = _mapper.Map<Todo>(request);

            var result = await _repository.GetAllAsync();
            await _repository.CreateAsync(t);
            await _repository.SaveChangesAsync();

            return _mapper.Map<TodoDTO>(t);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[AddTodoUseCase:Execute] {ex.Message}");
            throw;
        }
    }
}
