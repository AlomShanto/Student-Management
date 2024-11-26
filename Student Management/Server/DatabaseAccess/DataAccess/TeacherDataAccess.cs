using System;
using System.ComponentModel;
using System.Security.Principal;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Contracts.DBSettings;
using Server.Contracts.Entities;
using Server.Contracts.Models;
using Server.DatabaseAccess.IDataAccess;

namespace Server.DatabaseAccess.DataAccess;

public class TeacherDataAccess : ITeacherDataAccess
{
    private readonly IMongoCollection<Teacher> _teachersCollection;
    private readonly IMongoCollection<User> _userCollection;
    private readonly IAccountDataAccess _account;

    public TeacherDataAccess(IOptions<MongoDBSettings> mongoDbSettingts, IAccountDataAccess account)
    {
        _account = account;
        string teacherCollectionName = "teacher";
        string userCollectionName = "user";
        MongoClient client = new MongoClient(mongoDbSettingts.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDbSettingts.Value.DatabaseName);
        _teachersCollection = database.GetCollection<Teacher>(teacherCollectionName);
        _userCollection = database.GetCollection<User>(userCollectionName);
        _account = account;
    }
    public async Task CreateNewTeacherAsync(Teacher teacher)
    {
        await _teachersCollection.InsertOneAsync(teacher);
        return;
    }

    public async Task<List<Teacher>> GetTeachersAsync()
    {
        var  teachers = await _teachersCollection.Find(new BsonDocument()).ToListAsync();
        return teachers;
    }

    public async Task<Teacher> GetTeacherByUsernameAsync(string username)
    {
        var filter = Builders<Teacher>.Filter.Eq(t => t.Username, username);
        var teacher = await _teachersCollection.Find(filter).FirstOrDefaultAsync();
        return teacher;
    }

    public async Task<bool> UpdateTeacherAsync(string username, UpdateTeacher teacher)
    {
        var userFilter = Builders<User>.Filter.Eq(u => u.Username, teacher.Username);
        var userExist = await _userCollection.Find(userFilter).FirstOrDefaultAsync();

        if (userExist != null)
        {
            throw new Exception("Username already taken");
        }

        var teacherFilter = Builders<Teacher>.Filter.Eq(t => t.Username, username);

        var updateDefinition = Builders<Teacher>.Update
            .Set(t => t.FullName, teacher.FullName)
            .Set(t => t.Username, teacher.Username)
            .Set(t => t.TeacherId, teacher.TeacherId)
            .Set(t => t.JoinedAt, teacher.JoinedAt)
            .Set(t => t.Gender, teacher.Gender)
            .Set(t => t.BloodGroup, teacher.BloodGroup)
            .Set(t => t.Department, teacher.Department)
            .Set(t => t.Phone, teacher.Phone)
            .Set(t => t.Degree, teacher.Degree)
            .Set(t => t.Research, teacher.Research)
            .Set(t => t.Address, teacher.Address);

        var updateResult = await _teachersCollection.UpdateOneAsync(teacherFilter, updateDefinition);
        await _account.UpdateUsername(username, teacher.Username);

        return updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteTeacherAsync(string username)
    {
        var deleteResult = await _teachersCollection.DeleteOneAsync(s => s.Username == username);
        return deleteResult.DeletedCount > 0;
    }
}
