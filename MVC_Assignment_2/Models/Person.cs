using System.ComponentModel.DataAnnotations;

namespace MVC_Assignment_2.Models
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{LastName} {FirstName}";
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string DateOfBirthString => DateOfBirth.ToString("yyyy-MM-dd");
        public string PhoneNumber { get; set; } = string.Empty;
        public string BirthPlace { get; set; } = string.Empty;
        public bool IsGraduated { get; set; } = false;
    }
}