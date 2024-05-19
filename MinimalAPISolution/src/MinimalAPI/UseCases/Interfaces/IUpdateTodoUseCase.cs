using MinimalAPI.Dtos;
using System.Threading.Tasks;

namespace MinimalAPI.UseCases.Interfaces;

public interface IUpdateTodoUseCase : IUseCase<Task, TodoDTO>
{
}
