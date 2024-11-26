using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Contracts.Entities;
[BsonIgnoreExtraElements]
public class Teacher
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string BsonId { get; set; } = null!;

    [BsonElement("full_name")]
    public string FullName { get; set; }

    [BsonElement("username")]
    public string Username { get; set; }

    [BsonElement("teacherId")]
    public string TeacherId { get; set; }

    [BsonElement("joined_at")]
    public DateOnly JoinedAt { get; set; }

    [BsonElement("gender")]
    public string Gender { get; set; }

    [BsonElement("blood_group")]
    public string BloodGroup { get; set; }

    [BsonElement("department")]
    public string Department { get; set; }

    [BsonElement("phone")]
    public string Phone { get; set; }

    [BsonElement("degree")]
    public string Degree { get; set; }

    [BsonElement("research")]
    public string[] Research { get; set; }


    [BsonElement("address")]
    public string Address { get; set; }

}
