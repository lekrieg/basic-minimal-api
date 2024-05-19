using MinimalAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalAPI.UseCases.Interfaces;

public interface IGetAllTodoUseCase : IUseCase<Task<IEnumerable<TodoDTO>>>
{
}
