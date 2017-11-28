using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace ObjectDepot.Data
{
    public class MongoDBDataHandler
    {
        private string connectionString;
        private MongoClient client;

        public MongoDBDataHandler()
        { }

        /// <summary>
        /// Connect to MongoDB during the constructor
        /// </summary>
        /// <param name="Server">The MongoDB hostname or IP</param>
        /// <param name="Port">Non-default port if necessary</param>
        /// <param name="Username">Optional: This is MongoDB Username</param>
        /// <param name="Password">Optional: This is MongoDB Password</param>
        public MongoDBDataHandler(string Server, int Port = 27017, string Username = null, string Password = null)
        {
            connectionString = Server + ":" + Port.ToString();

            if (Username != null)
            {
                if (Password != null)
                {
                    connectionString = "mongodb://" + Username + ":" + Password + "@" + connectionString;
                }
                else
                {
                    connectionString = "mongodb://" + Username + "@" + connectionString;
                }
            }
            else
            {
                connectionString = "mongodb://" + connectionString;
            }

            try
            {
                client = new MongoClient(connectionString);
            }
            catch
            { }
        }

        // Manually connect to the database if not already done via the constructor, or if the database information changes
        private bool Connect(string Server, int Port = 27017, string Username = null, string Password = null)
        {
            connectionString = Server + ":" + Port.ToString();

            if (Username != null)
            {
                if (Password != null)
                {
                    connectionString = "mongodb://" + Username + ":" + Password + "@" + connectionString;
                }
                else
                {
                    connectionString = "mongodb://" + Username + "@" + connectionString;
                }
            }
            else
            {
                connectionString = "mongodb://" + connectionString;
            }

            try
            {
                client = new MongoClient(connectionString);
                if (client != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // This function will attempt to create a document in the MongoDB Database (and passed collection) and return the created ObjectId if possible
        public ObjectId? WriteObject(string Database, string Collection, object o)
        {

            try
            {
                if (client != null)
                {
                    IMongoCollection<BsonDocument> collection =
                        client
                        .GetDatabase(Database)
                        .GetCollection<BsonDocument>(Collection);

                    var doc = o.ToBsonDocument();

                    collection.InsertOne(doc);

                    var id = (ObjectId)doc.Elements.ElementAt(1).Value;

                    return id;
                }
                else
                {
                    return null;
                }
            }
            catch { return null; }
        }

        // This function returns a queryable collection of objects
        public IQueryable<T> GetObjects<T>(string Database, string Collection)
        {
            try
            {
                return client
                    .GetDatabase(Database)
                    .GetCollection<T>(Collection)
                    .AsQueryable();
            }
            catch { return null; }
        }
    }
}
