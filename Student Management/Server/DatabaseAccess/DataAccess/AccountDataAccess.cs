using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Contracts.DataTransferObjects;
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

    public async Task<List<UserDto>>GetUsersAsync(string role)
    {
        var projection = Builders<User>.Projection.Include(u => u.Username).Include(u => u.Role);
        var filter = Builders<User>.Filter.Eq(user => user.Role, role);

        var users =  await _userCollection.Find(filter).Project(projection).ToListAsync();

        List<UserDto> userToReturn = users.Select(user =>
            new UserDto(user["username"].AsString, user["role"].AsString)).ToList();

        return userToReturn;

    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var filter = Builders<User>.Filter.Eq(user => user.Username, username);

        var user = await _userCollection.Find(filter).FirstOrDefaultAsync();

        return user;
    }

    public async Task<bool> UpdateUsername(string username, string updatedUsername)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Username, updatedUsername);
        var user  = await _userCollection.Find(filter).FirstOrDefaultAsync();

        if (user != null)
        {
            throw new Exception("Username already taken");
        }

        filter = Builders<User>.Filter.Eq(u => u.Username, username);
        user = await _userCollection.Find(filter).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new Exception("User doesn't exist");
        }

        var updateDefinition = Builders<User>.Update.Set(u => u.Username, updatedUsername);

        var updateResult = await _userCollection.UpdateOneAsync(filter, updateDefinition);

        return updateResult.ModifiedCount > 0;


    }

    public async Task<bool> DeleteUserAsync(string username)
    {
        var deleteResult = await _userCollection.DeleteOneAsync(u => u.Username == username);
        return deleteResult.DeletedCount > 0;
    }

   
}
