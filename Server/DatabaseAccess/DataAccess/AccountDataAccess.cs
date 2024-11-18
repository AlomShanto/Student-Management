using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Server.Contracts.DBSettings;
using Server.Contracts.Entities;
using Server.Contracts.Models;
using Server.DatabaseAccess.IDataAccess;

namespace Server.DatabaseAccess.DataAccess;

public class AccountDataAccess : IAccountDataAccess
{
    private readonly IMongoCollection<User> _userCollection;

    public AccountDataAccess(IOptions<MongoDBSettings> mongoDbSettings){
        string collectionName = "user";
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _userCollection = database.GetCollection<User>(collectionName);
    }

    public async Task CreateNewUser(UserForm user)
    {
        user.Username = user.Username.ToLower();
        user.Role = user.Role.ToLower();

        var existingUser = await _userCollection.Find(u => 
                u.Username == user.Username).FirstOrDefaultAsync();

        if(existingUser != null){
            throw new ArgumentException("Username has already taken");
        }

        if(user.Password != user.ConfirmPassword){
            throw new ArgumentException("Both password need to be same");
        }

        using var hmac = new HMACSHA512();

        var newUser = new User{
            Username = user.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
            PasswordSalt = hmac.Key,
            Role = user.Role
        };

        await _userCollection.InsertOneAsync(newUser);
    }
}
