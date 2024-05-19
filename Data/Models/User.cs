namespace Data.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    public string Username { get; set; } = default!;

    public string Password { get; set; } = default!;
}

