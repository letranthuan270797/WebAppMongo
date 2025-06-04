using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;


namespace DataAccess.Models
{
    public class Customer
    {
        //[BsonId]
        //public int Id { get; set; }

        //[BsonElement]
        //[Required]
        //public int CustomerId { get; set; }

        //[BsonElement]
        //[Required]
        //public string Name { get; set; }

        //[BsonElement]
        //[Required]
        //public int Age { get; set; }
        //[BsonElement]
        //public int Salary { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MongoId { get; set; }  // Tương ứng với _id

        [BsonElement("Id")]
        public int Id { get; set; }          // Tương ứng với "Id" trong document

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        public int Salary { get; set; }
    }
}
