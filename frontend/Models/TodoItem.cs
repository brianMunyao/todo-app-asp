
namespace frontend.Models;


public class TodoItem
{

    public string? Id { get; set; }
    public string? Text { get; set; }
    public bool IsComplete { get; set; }
    public string? DateAdded { get; set; }
}