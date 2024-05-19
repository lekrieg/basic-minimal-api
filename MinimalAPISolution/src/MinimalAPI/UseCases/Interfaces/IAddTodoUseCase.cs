using MinimalAPI.Dtos;
using System.Threading.Tasks;

namespace MinimalAPI.UseCases.Interfaces;

public interface IAddTodoUseCase : IUseCase<Task<TodoDTO>, TodoDTO>
{
}
