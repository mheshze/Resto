namespace Resto;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

public class MongoConnection
{
    private IMongoDatabase db;

    public MongoConnection(string database="Resto")
    {
        var client = new MongoClient();
        db = client.GetDatabase(database);
    }

    public void InsertRecord<T>(string collec_name , T record)
    {
        var collection = db.GetCollection<T>(collec_name);
        collection.InsertOne(record);
    }
    
    public List<T> LoadAllRecords<T>(string table)
    {
        var collections = db.GetCollection<T>(table);
        
        return collections.Find(new BsonDocument()).ToList();
    }

    public T LoadById<T>(string table, int id)
    {
        var collections = db.GetCollection<T>(table);
        var filter = Builders<T>.Filter.Eq("id", id);
        return collections.Find(filter).First();
    }
}


