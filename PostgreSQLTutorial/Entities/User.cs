using System;

namespace PostgreSQLTutorial.Entities
{
    class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\r\nFirstName: {FirstName}\r\nEmail: {Email}\r\nPhoneNumber: {PhoneNumber}\r\n";
        }
    }
}
