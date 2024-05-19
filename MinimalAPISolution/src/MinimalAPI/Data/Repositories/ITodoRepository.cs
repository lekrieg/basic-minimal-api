using MinimalAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalAPI.Data.Repositories;

public interface ITodoRepository
{
    Task SaveChangesAsync();

    Task<IEnumerable<Todo>> GetAllAsync();
    
    Task<IEnumerable<Todo>> GetAllCompleteAsync();
    
    Task<Todo> GetByIdAsync(int id);
    
    Task CreateAsync(Todo todo);
    
    Task UpdateAsync(Todo todo);
    
    Task Delete(Todo todo);

    Task DeleteAsync(int id);
}
