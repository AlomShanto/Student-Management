using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Contracts.Entities;
[BsonIgnoreExtraElements]
public class Student
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string BsonId { get; set; } = null!;

    [BsonElement("full_name")]
    public string FullName { get; set; }

    [BsonElement("username")]
    public string Username { get; set; }

    [BsonElement("studentId")]
    public string StudentId { get; set; }

    [BsonElement("dateOfBirth")]
    public DateOnly DateOfBirth { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("last_updated_at")]
    public DateTime LastUpdatedAt { get; set; }

    [BsonElement("gender")]
    public string Gender { get; set; }

    [BsonElement("blood_group")]
    public string BloodGroup { get; set; }

    [BsonElement("department")]
    public string Department { get; set; }

    [BsonElement("session")]
    public string Session { get; set; }

    [BsonElement("phone")]
    public string Phone { get; set; }

    [BsonElement("last_donated_at")]
    public DateOnly LastDonatedAt { get; set; }

    [BsonElement("address")]
    public string Address { get; set; }

}
