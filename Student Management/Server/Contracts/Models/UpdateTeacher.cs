using MongoDB.Bson.Serialization.Attributes;

namespace Server.Contracts.Models
{
    public class UpdateTeacher
    {
        public string FullName { get; set; }

        public string Username { get; set; }

        public string TeacherId { get; set; }

        public DateOnly JoinedAt { get; set; }

        public string Gender { get; set; }

        public string BloodGroup { get; set; }

        public string Department { get; set; }

        public string Phone { get; set; }

        public string Degree { get; set; }

        public string[] Research { get; set; }

        public string Address { get; set; }
    }
}
