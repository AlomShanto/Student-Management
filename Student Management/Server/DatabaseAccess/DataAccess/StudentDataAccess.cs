using System;
using System.Data;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Contracts.DataTransferObjects;
using Server.Contracts.DBSettings;
using Server.Contracts.Entities;
using Server.Contracts.Models;
using Server.DatabaseAccess.IDataAccess;

namespace Server.DatabaseAccess.DataAccess;

public class StudentDataAccess : IStudentDataAccess
{
    private readonly IMongoCollection<Student> _studentsCollection;
    private readonly IMongoCollection<User> _userCollection;

    private readonly IAccountDataAccess _account;

    public StudentDataAccess(IOptions<MongoDBSettings> mongoDbSettings, IAccountDataAccess account)
    {
        _account = account;
        string studentCollectionName = "student";
        string userCollectionName = "user";
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _studentsCollection = database.GetCollection<Student>(studentCollectionName);
        _userCollection = database.GetCollection<User>(userCollectionName);

    }
    public async Task CreateNewStudentAsync(Student student)
    {
        student.CreatedAt = DateTime.UtcNow;
        student.LastUpdatedAt = DateTime.UtcNow;
        await _studentsCollection.InsertOneAsync(student);
    }

    public async Task<List<Student>> GetStudentsAsync()
    {
        var students = await _studentsCollection.Find(new BsonDocument()).ToListAsync();

        return students;
    }


    public async Task<Student> GetStudentByUsernameAsync(string username)
    {
        var filter = Builders<Student>.Filter.Eq(student => student.Username, username);

        var student = await _studentsCollection.Find(filter).FirstOrDefaultAsync();

        return student;
    }

    public async Task<bool> UpdateStudentAsync(string username, UpdateStudent student)
    {
        var userFilter = Builders<User>.Filter.Eq(s => s.Username, student.Username);
        var userExist = await _userCollection.Find(userFilter).FirstOrDefaultAsync();

        if (userExist != null)
        {
            throw new Exception("Username Already Taken");
        }

        var studentFilter = Builders<Student>.Filter.Eq(s => s.Username, username);
        var updateDefinition = Builders<Student>.Update
            .Set(s => s.FullName, student.FullName)
            .Set(s => s.Username, student.Username)
            .Set(s => s.StudentId, student.StudentId)
            .Set(s => s.DateOfBirth, student.DateOfBirth)
            .Set(s => s.Gender, student.Gender)
            .Set(s => s.BloodGroup, student.BloodGroup)
            .Set(s => s.Department, student.Department)
            .Set(s => s.Session, student.Session)
            .Set(s => s.Phone, student.Phone)
            .Set(s => s.LastDonatedAt, student.LastDonatedAt)
            .Set(s => s.Address, student.Address);

        var updateResult = await _studentsCollection.UpdateOneAsync(studentFilter, updateDefinition);
        await _account.UpdateUsername(username, student.Username);
        
        return updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteStudentAsync(string username)
    {
        var deleteResult = await _studentsCollection.DeleteOneAsync(s => s.Username == username);
        return deleteResult.DeletedCount > 0;
    }
}
