namespace Server.Contracts.Models
{
    public class UpdateStudent
    {
        public string FullName { get; set; }

        public string Username { get; set; }

        public string StudentId { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string BloodGroup { get; set; }

        public string Department { get; set; }

        public string Session { get; set; }

        public string Phone { get; set; }

        public DateOnly LastDonatedAt { get; set; }

        public string Address { get; set; }
    }
    
}
