
using frontend.Models;

namespace frontend.Services;

public class TodosService
{
    private readonly HttpClient httpClient;

    public TodosService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<TodoItem>> GetTodos()
    {
        try
        {
            var httpClient = new HttpClient();

            List<TodoItem> data = await httpClient.GetFromJsonAsync<List<TodoItem>>("http://localhost:5121/api/todos") ?? new List<TodoItem>();
            return data;
        }
        catch
        {
            return new List<TodoItem>();
        }
    }

    public async Task<TodoItem> AddNewTodo(string NewTodo)
    {
        try
        {
            var httpClient = new HttpClient();

            var data = await httpClient.PostAsJsonAsync("http://localhost:5121/api/todos", new TodoItem
            {
                Text = NewTodo,
                IsComplete = false,
                DateAdded = DateTime.Now.ToString()
            });

            if (data.IsSuccessStatusCode)
            {
                var createdTodo = await data.Content.ReadFromJsonAsync<TodoItem>();
                return createdTodo!;
            }
            else
            {
                throw new Exception("Error Adding");
                //TODO: Use proper error handling
            }

        }
        catch
        {
            throw new Exception("Error Adding");
            //TODO: Use proper error handling
        }
    }

    public async Task<TodoItem> UpdateTodo(string TodoId, TodoItem updatedTodo)
    {
        try
        {
            var httpClient = new HttpClient();
            var data = await httpClient.PutAsJsonAsync("http://localhost:5121/api/todos/" + TodoId, updatedTodo);

            if (data.IsSuccessStatusCode) return updatedTodo;

            throw new Exception("Error Adding");
        }
        catch
        {
            throw new Exception("Error updating todo");
        }
    }

    public async Task DeleteTodo(string TodoId)
    {
        try
        {
            var httpClient = new HttpClient();
            var data = await httpClient.DeleteAsync("http://localhost:5121/api/todos/" + TodoId);
            if (!data.IsSuccessStatusCode)
            {
                throw new Exception("Error Adding");
            }
        }
        catch
        {
            throw new Exception("Error deleting todo");
        }
    }
}