using System.Text.Json.Serialization;
using frontend.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace frontend.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public List<TodoItem> Todos { get; private set; } = new List<TodoItem>();

    public string? InputValue { get; set; } = "asdkl";

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGet()
    {
        try
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:5121/api/todos");
            var content = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<TodoItem>>(content);

            if (data is not null) Todos = data;
        }
        catch
        {
            Console.WriteLine("Error occured");
        }
    }
}
