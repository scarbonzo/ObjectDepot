using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectDepot.Data.Models
{
    [BsonDiscriminator("image")]
    public class Image
    {
        public ObjectId _id { get; set; } //MongoDB ID
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileType { get; set; }
        public DateTime Timestamp { get; set; }
        public string ImageData { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
