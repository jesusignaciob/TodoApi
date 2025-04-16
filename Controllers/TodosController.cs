using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoRepository _repository;

    public TodosController(ITodoRepository repository)
    {
        _repository = repository;
    }

    // GET: api/todos
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAll()
    {
        var todos = await _repository.GetAllAsync();
        return Ok(todos);
    }

    // GET: api/todos/5
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetById(int id)
    {
        var todo = await _repository.GetByIdAsync(id);
        if (todo == null) return NotFound();
        return Ok(todo);
    }

    // POST: api/todos
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Todo>> Create(TodoDto todoDto)
    {
        var todo = new Todo
        {
            Title = todoDto.Title,
            IsCompleted = todoDto.IsCompleted
        };

        var createdTodo = await _repository.CreateAsync(todo);
        return CreatedAtAction(nameof(GetById), new { id = createdTodo.Id }, createdTodo);
    }

    // PUT: api/todos/5
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TodoDto todoDto)
    {
        var success = await _repository.UpdateAsync(id, todoDto);
        if (!success) return NotFound();
        return NoContent();
    }

    // DELETE: api/todos/5
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _repository.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}