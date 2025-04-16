using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

public interface ITodoRepository
{
    Task<List<Todo>> GetAllAsync();
    Task<Todo?> GetByIdAsync(int id);
    Task<Todo> CreateAsync(Todo todo);
    Task<bool> UpdateAsync(int id, TodoDto todoDto);
    Task<bool> DeleteAsync(int id);
}

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Todo>> GetAllAsync()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<Todo?> GetByIdAsync(int id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task<Todo> CreateAsync(Todo todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        return todo;
    }

    public async Task<bool> UpdateAsync(int id, TodoDto todoDto)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return false;

        todo.Title = todoDto.Title;
        todo.IsCompleted = todoDto.IsCompleted;
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) return false;

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
        return true;
    }
}