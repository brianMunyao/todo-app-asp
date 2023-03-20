using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models;


public class TodoItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Text { get; set; }
    public bool isComplete { get; set; }

    public string? DateAdded { get; set; }
}