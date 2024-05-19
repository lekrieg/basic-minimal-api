using AutoMapper;
using Microsoft.Extensions.Logging;
using MinimalAPI.Data.Repositories;
using MinimalAPI.Dtos;
using MinimalAPI.UseCases.Interfaces;
using System;
using System.Threading.Tasks;

namespace MinimalAPI.UseCases;

public class GetByIdTodoUseCase : IGetByIdTodoUseCase
{
    private readonly ILogger<GetByIdTodoUseCase> _logger;
    private readonly IMapper _mapper;

    private readonly ITodoRepository _repository;

    public GetByIdTodoUseCase(ILogger<GetByIdTodoUseCase> logger, IMapper mapper, ITodoRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<TodoDTO> Execute(int id)
    {
        try
        {
            var result = await _repository.GetByIdAsync(id);

            return _mapper.Map<TodoDTO>(result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[GetByIdTodoUseCase:Execute] {ex.Message}");
            throw;
        }
    }
}
