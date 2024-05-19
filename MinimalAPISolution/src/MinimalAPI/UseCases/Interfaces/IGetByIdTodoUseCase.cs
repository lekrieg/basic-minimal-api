using MinimalAPI.Dtos;
using System.Threading.Tasks;

namespace MinimalAPI.UseCases.Interfaces;

public interface IGetByIdTodoUseCase : IUseCase<Task<TodoDTO>, int>
{
}
