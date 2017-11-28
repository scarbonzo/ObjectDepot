using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace ObjectDepot.Data.Models
{
    public class Document
    {
        public ObjectId Id { get; set; } //MongoDB ID
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileType { get; set; }
        public byte[] Data { get; set; } //Serialized Data
        public List<string> Tags { get; set; }
        public string Author { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
