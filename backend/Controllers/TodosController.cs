using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly TodosService _todosService;

    public TodosController(TodosService todosService) => _todosService = todosService;

    [HttpGet]
    public async Task<List<TodoItem>> Get() => await _todosService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> Get(string id)
    {
        var todo = await _todosService.GetAsync(id);

        return todo is not null ? todo : NotFound();
    }
    [HttpPost]
    public async Task<ActionResult<TodoItem>> Post([FromBody] TodoItem newTodo)
    {
        await _todosService.CreateAsync(newTodo);

        return CreatedAtAction(nameof(Get), new { id = newTodo.Id }, newTodo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] TodoItem updatedTodo)
    {
        var todo = await _todosService.GetAsync(id);

        if (todo is null) return NotFound();

        updatedTodo.Id = todo.Id;
        await _todosService.UpdateAsync(id, updatedTodo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var todo = await _todosService.GetAsync(id);

        if (todo is null) return NotFound();

        await _todosService.RemoveAsync(id);
        return NoContent();
    }
}