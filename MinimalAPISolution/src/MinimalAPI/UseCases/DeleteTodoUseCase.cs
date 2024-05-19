using Microsoft.Extensions.Logging;
using MinimalAPI.Data.Repositories;
using MinimalAPI.UseCases.Interfaces;
using System.Threading.Tasks;

namespace MinimalAPI.UseCases;

public class DeleteTodoUseCase : IDeleteTodoUseCase
{
    private readonly ILogger<DeleteTodoUseCase> _logger;

    private readonly ITodoRepository _repository;

    public DeleteTodoUseCase(ILogger<DeleteTodoUseCase> logger, ITodoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task Execute(int id)
    {
        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
    }
}
