using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WebApiSelfHost
{
    [BsonIgnoreExtraElements]
    public class ErroLog
    {
        [BsonId]
        public Guid _id { get; set; }
        public DateTime Data { get; set; }
        public string Aplicacao { get; set; }
        public string Protocolo { get; set; }
        public object Exception { get; set; }
    }
}
