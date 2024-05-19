using AutoMapper;
using Microsoft.Extensions.Logging;
using MinimalAPI.Data.Repositories;
using MinimalAPI.Dtos;
using MinimalAPI.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalAPI.UseCases;

public class GetAllTodoUseCase : IGetAllTodoUseCase
{
    private readonly ILogger<GetAllTodoUseCase> _logger;
    private readonly IMapper _mapper;

    private readonly ITodoRepository _repository;

    public GetAllTodoUseCase(ILogger<GetAllTodoUseCase> logger, IMapper mapper, ITodoRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<TodoDTO>> Execute()
    {
        try
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<List<TodoDTO>>(result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[GetAllTodoUseCase:Execute] {ex.Message}");
            throw;
        }
    }
}
