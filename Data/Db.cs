using MongoDB.Driver;
public class MongoDbService
{
    private MongoClient mongoClient { get; }

    public IMongoDatabase db { get; }

    public MongoDbService(string connectionString, string mongoDbName)
    {
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        // Set the ServerApi field of the settings object to Stable API version 1
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        // Create a new client and connect to the server
        mongoClient = new MongoClient(settings);

        db = mongoClient.GetDatabase(mongoDbName);
    }

}
