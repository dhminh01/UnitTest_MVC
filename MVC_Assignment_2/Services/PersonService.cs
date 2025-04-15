using MVC_Assignment_2.Models;
using MVC_Assignment_2.Data;
using OfficeOpenXml;

namespace MVC_Assignment_2.Services
{
    public class PersonService : IPersonService
    {
        private static readonly List<Person> _people = DummyData.GetPeople();

        public List<Person> GetAll()
        {

            return _people;
        }

        public List<Person> GetMales()
        {

            return _people.Where(p => p.Gender == Gender.Male).ToList();
        }

        public Person GetOldest()
        {

            return _people.OrderBy(p => p.DateOfBirth).First();
        }

        public List<string> GetFullNames()
        {

            return _people.Select(p => p.FullName).ToList();
        }

        public List<Person> FilterByBirthYear(string condition) =>
            condition.ToLower() switch
            {
                "equal" => _people.Where(p => p.DateOfBirth.Year == 2000).ToList(),
                "greater" => _people.Where(p => p.DateOfBirth.Year > 2000).ToList(),
                "less" => _people.Where(p => p.DateOfBirth.Year < 2000).ToList(),
                _ => []
            };

        public Person GetById(int id)
        {

            return _people.FirstOrDefault(p => p.ID == id)!;
        }

        public byte[] ExportToExcel()
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("People");
            worksheet.Cells.LoadFromCollection(_people, true);
            int dateColumnIndex = 6;
            for (int row = 2; row <= _people.Count + 1; row++)
            {
                worksheet.Cells[row, dateColumnIndex].Style.Numberformat.Format = "yyyy-mm-dd";
            }

            return package.GetAsByteArray();
        }

        public void Add(Person person)
        {
            person.ID = _people.Max(p => p.ID) + 1;
            _people.Add(person);
        }

        public void Update(Person person)
        {
            var existingPerson = _people.FirstOrDefault(p => p.ID == person.ID);
            if (existingPerson != null)
            {
                existingPerson.FirstName = person.FirstName;
                existingPerson.LastName = person.LastName;
                existingPerson.Gender = person.Gender;
                existingPerson.DateOfBirth = person.DateOfBirth;
                existingPerson.PhoneNumber = person.PhoneNumber;
                existingPerson.BirthPlace = person.BirthPlace;
                existingPerson.IsGraduated = person.IsGraduated;
            }
        }

        public void Delete(int id)
        {
            var person = _people.FirstOrDefault(p => p.ID == id);
            if (person != null)
            {
                _people.Remove(person);
            }
        }

        public (List<Person> People, int TotalPages) Paging(int page, int pageSize)
        {
            var totalPeople = _people.Count();
            var totalPages = (int)Math.Ceiling(totalPeople / (double)pageSize);
            var pagedPeople = _people.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return (pagedPeople, totalPages);
        }
    }
}