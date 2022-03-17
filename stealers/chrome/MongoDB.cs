using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoConn
{
    public class MongoCRUD{
        public IMongoDatabase db;

        public MongoCRUD(string database)
        {
            // MongoDB Initialization
            var client = new MongoClient("mongodb+srv://root:Grau5.56@cookiestealer.bpxwg.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }
    }
}
