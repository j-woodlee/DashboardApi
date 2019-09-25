using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DashboardApi.Models {

    public class Jenkins {

        // Each variable name is a key, it's type is the value it is mapped to in Mongo

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Project")]
        public string ProjectName { get; set; }

        [BsonElement("Builds")]
        public Build[] Builds { get; set; }
    }
    
}

public class Build {
        [BsonElement("_class")]
        public string _class { get; set; }

        [BsonElement("id")]
        public string identifier { get; set; }

        [BsonElement("number")]
        public string number { get ; set; }

        [BsonElement("result")]
        public string result { get; set; }
        
        [BsonElement("timestamp")]
        public string timestamp { get; set; }
}