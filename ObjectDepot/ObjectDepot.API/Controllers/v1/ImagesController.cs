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

namespace ObjectDepot.API.Controllers
{
    public class ImagesController : Controller
    {
        MongoDBDataHandler db = new MongoDBDataHandler("192.168.50.225");

        public ImagesController()
        {
            try
            {
                BsonClassMap.RegisterClassMap<Image>();
            }
            catch { }
        }

        [HttpGet]
        [Route("api/v1/images/{Id}")]
        public FileContentResult GetImage(string Id)
        {
            try
            {
                var oid = new ObjectId(Id);

                var image = db.GetObjects<Image>("Testing", "Images")
                    .Where(i => i._id == oid)
                    .FirstOrDefault();

                byte[] imageData = Encoding.ASCII.GetBytes(image.ImageData);
                string imageFileName = image.FileName + "." + image.FileExtension;

                var file = new FileContentResult(imageData, image.FileType);

                return file;
            }
            catch
            {
                return null;
            }
        }
    }
}
