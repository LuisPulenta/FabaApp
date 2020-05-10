using FabaApp.Common.Enums;
using System;

namespace FabaApp.Common.Models
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Document { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public LabResponse Lab { get; set; }
        public DateTime DischargeDate { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }
}