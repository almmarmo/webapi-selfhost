using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSelfHost
{
    public class MongoRepository
    {
        IMongoClient client;
        IMongoDatabase database;

        public MongoRepository(string database)
        {
            client = new MongoClient(System.Configuration.ConfigurationManager.ConnectionStrings["mongodb"].ConnectionString);

            this.database = client.GetDatabase(database);
        }

        public async Task<long> CountAsync(string collectionName)
        {
            var collection = GetCollection(collectionName);
            return await collection.CountAsync(new BsonDocument());
        }

        public async Task<IEnumerable<ErroLog>> GetAll(string collectionName)
        {
            List<ErroLog> list = new List<ErroLog>();
            var collection = GetCollection(collectionName);
            Console.WriteLine("Conectando mongo...");
            using (var cursor = await collection.FindAsync(x => x.Data > DateTime.Now.AddDays(-1)))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var doc in batch)
                    {
                        list.Add(doc);
                        Console.WriteLine("Doc " + doc._id.ToString() + " adicionado.");
                    }
                }
            }

            Console.WriteLine("Conexão com mogo finalizada.");

            return list;
        }

        private IMongoCollection<ErroLog> GetCollection(string collection)
        {
            return this.database.GetCollection<ErroLog>(collection);
        }
    }
}
