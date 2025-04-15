using MVC_Assignment_2.Models;

namespace MVC_Assignment_2.Data
{
    public static class DummyData
    {
        public static List<Person> GetPeople() =>
        [
            new() { ID = 1, FirstName = "John", LastName = "Doe", Gender = Gender.Male, DateOfBirth = new(2000, 5, 15), PhoneNumber = "123456789", BirthPlace = "Hanoi", IsGraduated = true },
            new() { ID = 2, FirstName = "Jane", LastName = "Smith", Gender = Gender.Female, DateOfBirth = new(1998, 7, 22), PhoneNumber = "987654321", BirthPlace = "Saigon", IsGraduated = false },
            new() { ID = 3, FirstName = "Tom", LastName = "Brown", Gender = Gender.Other, DateOfBirth = new(2002, 1, 10), PhoneNumber = "1122334455", BirthPlace = "Danang", IsGraduated = true },
            new() { ID = 4, FirstName = "Alice", LastName = "Johnson", Gender = Gender.Female, DateOfBirth = new(1995, 3, 30), PhoneNumber = "2233445566", BirthPlace = "Hue", IsGraduated = true },
            new() { ID = 5, FirstName = "Bob", LastName = "Wilson", Gender = Gender.Male, DateOfBirth = new(2001, 8, 25), PhoneNumber = "3344556677", BirthPlace = "Can Tho", IsGraduated = false },
            new() { ID = 6, FirstName = "Charlie", LastName = "Davis", Gender = Gender.Male, DateOfBirth = new(1997, 11, 5), PhoneNumber = "4455667788", BirthPlace = "Hai Phong", IsGraduated = true },
            new() { ID = 7, FirstName = "Daisy", LastName = "Miller", Gender = Gender.Other, DateOfBirth = new(2003, 6, 18), PhoneNumber = "5566778899", BirthPlace = "Nha Trang", IsGraduated = false },
            new() { ID = 8, FirstName = "Edward", LastName = "Anderson", Gender = Gender.Male, DateOfBirth = new(1999, 2, 12), PhoneNumber = "6677889900", BirthPlace = "Da Lat", IsGraduated = true },
            new() { ID = 9, FirstName = "Fiona", LastName = "Clark", Gender = Gender.Female, DateOfBirth = new(2004, 9, 8), PhoneNumber = "7788990011", BirthPlace = "Vung Tau", IsGraduated = false },
            new() { ID = 10, FirstName = "George", LastName = "Lewis", Gender = Gender.Male, DateOfBirth = new(2000, 12, 1), PhoneNumber = "8899001122", BirthPlace = "Quang Ninh", IsGraduated = true },
            new() { ID = 11, FirstName = "Hannah", LastName = "Walker", Gender = Gender.Female, DateOfBirth = new(1996, 4, 22), PhoneNumber = "9900112233", BirthPlace = "Binh Duong", IsGraduated = true },
            new() { ID = 12, FirstName = "Ian", LastName = "White", Gender = Gender.Male, DateOfBirth = new(1998, 10, 19), PhoneNumber = "1011121314", BirthPlace = "Dong Nai", IsGraduated = false },
            new() { ID = 13, FirstName = "Jack", LastName = "Hall", Gender = Gender.Male, DateOfBirth = new(2005, 7, 11), PhoneNumber = "1213141516", BirthPlace = "Bac Ninh", IsGraduated = false }
        ];
    }
}
