using MVC_Assignment_2.Models;

namespace MVC_Assignment_2.Services
{
    public interface IPersonService
    {
        List<Person> GetAll();
        Person GetOldest();
        List<Person> GetMales();
        List<string> GetFullNames();
        List<Person> FilterByBirthYear(string condition);
        Person GetById(int id);
        byte[] ExportToExcel();
        public void Add(Person person);
        public void Update(Person person);
        public void Delete(int id);
        (List<Person> People, int TotalPages) Paging(int page, int pageSize);
    }
}