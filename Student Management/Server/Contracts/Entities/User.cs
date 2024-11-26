using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Contracts.Entities;
[BsonIgnoreExtraElements]
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDataAccess { get; set; } = null!;

    [BsonElement("username")]
    public string Username { get; set; }

    [BsonElement("passwordHash")]
    public byte[] PasswordHash { get; set; }

    [BsonElement("passwordSalt")]
    public byte[] PasswordSalt { get; set; }

    [BsonElement("role")]
    public string Role { get; set; }
}
