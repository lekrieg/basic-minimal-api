using AutoMapper;
using Microsoft.Extensions.Logging;
using MinimalAPI.Data.Repositories;
using MinimalAPI.Dtos;
using MinimalAPI.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalAPI.UseCases;

public class GetAllCompleteTodoUseCase : IGetAllCompleteTodoUseCase
{
    private readonly ILogger<GetAllCompleteTodoUseCase> _logger;
    private readonly IMapper _mapper;

    private readonly ITodoRepository _repository;

    public GetAllCompleteTodoUseCase(ILogger<GetAllCompleteTodoUseCase> logger, IMapper mapper, ITodoRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<TodoDTO>> Execute()
    {
        try
        {
            var result = await _repository.GetAllCompleteAsync();
            return _mapper.Map<List<TodoDTO>>(result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"[GetAllCompleteTodoUseCase:Execute] {ex.Message}");
            throw;
        }
    }
}
