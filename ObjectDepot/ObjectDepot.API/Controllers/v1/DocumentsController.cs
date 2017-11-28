using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using ObjectDepot.Data;
using ObjectDepot.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDepot.API.Controllers.v1
{
    public class DocumentsController : Controller
    {
        MongoDBDataHandler db = new MongoDBDataHandler("192.168.50.225");

        public DocumentsController()
        {
            try
            {
                BsonClassMap.RegisterClassMap<Document>();
            }
            catch { }
        }

        [HttpGet]
        [Route("api/v1/documents/test")]
        public ActionResult Test()
        {
            try
            {
                var document = new Document
                {
                    FileName = "test",
                    FileType = "text/plain",
                    FileExtension = "txt",
                    Data = Encoding.ASCII.GetBytes("Hokey Pokey Mo Fo"),
                    Author = "reodice",
                    Tags = new List<string> { "testing", "attachment" },
                    Timestamp = DateTime.Now
                };

                return Ok(db.WriteObject("Testing", "Documents", document));
            }
            catch
            {
                return NotFound();
            }
            
        }

        [HttpGet]
        [Route("api/v1/documents/{Id}")]
        public ActionResult GetDocument(string Id)
        {
            try
            {
                var oid = new ObjectId(Id);

                var document = db.GetObjects<Document>("Testing", "Documents")
                    .Where(i => i.Id == oid)
                    .FirstOrDefault();

                return Ok(document);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
