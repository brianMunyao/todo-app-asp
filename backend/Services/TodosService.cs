using backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace backend.Services;

public class TodosService
{
    private readonly IMongoCollection<TodoItem> _todosCollection;

    public TodosService(IOptions<TodosDbSettings> todoDbSettings)
    {
        var mongoClient = new MongoClient(todoDbSettings.Value.ConnectionString);

        var mongoDb = mongoClient.GetDatabase(todoDbSettings.Value.DatabaseName);

        _todosCollection = mongoDb.GetCollection<TodoItem>(todoDbSettings.Value.CollectionName);
    }


    public async Task<List<TodoItem>> GetAsync() => await _todosCollection.Find(_ => true).ToListAsync();

    public async Task<TodoItem?> GetAsync(string id) => await _todosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TodoItem newTodo) => await _todosCollection.InsertOneAsync(newTodo);

    public async Task UpdateAsync(string id, TodoItem updatedTodo) => await _todosCollection.ReplaceOneAsync(x => x.Id == id, updatedTodo);

    public async Task RemoveAsync(string id) => await _todosCollection.DeleteOneAsync(x => x.Id == id);
}
