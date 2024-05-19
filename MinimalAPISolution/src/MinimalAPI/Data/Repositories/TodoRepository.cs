using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalAPI.Data.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly ApplicationDbContext _context;

    public TodoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Todo todo)
    {
        await _context.Todo.AddAsync(todo);
    }

    public Task Delete(Todo todo)
    {
        _context.Todo.Remove(todo);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var t = await GetByIdAsync(id);
        await Delete(t);
    }

    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
        return await _context.Todo.ToListAsync();
    }

    public async Task<IEnumerable<Todo>> GetAllCompleteAsync()
    {
        return await _context.Todo.Where(t => t.IsComplete).ToListAsync();
    }

    public async Task<Todo> GetByIdAsync(int id)
    {
        return (await _context.Todo.FindAsync(id))!;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Todo todo)
    {
        _context.Todo.Update(todo);
        return Task.CompletedTask;
    }
}
