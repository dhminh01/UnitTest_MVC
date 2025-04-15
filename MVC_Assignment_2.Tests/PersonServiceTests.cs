using MVC_Assignment_2.Models;
using MVC_Assignment_2.Services;

namespace MVC_Assignment_2.Tests
{
    [TestFixture]
    public class PersonServiceTests
    {
        private PersonService _service;

        [SetUp]
        public void Setup()
        {
            _service = new PersonService();
        }

        [Test]
        public void GetAll_WhenCalled_ReturnsNonEmptyList()
        {
            var people = _service.GetAll();

            Assert.That(people, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void GetMales_WhenCalled_ReturnsOnlyMalePersons()
        {
            var males = _service.GetMales();

            Assert.That(males, Is.Not.Null);
            Assert.That(males.All(p => p.Gender == Gender.Male));
        }

        [Test]
        public void GetOldest_WhenCalled_ReturnsPersonWithEarliestDOB()
        {
            var all = _service.GetAll();
            var oldest = _service.GetOldest();

            Assert.That(oldest, Is.Not.Null);
            Assert.That(oldest.DateOfBirth, Is.EqualTo(all.Min(p => p.DateOfBirth)));
        }

        [Test]
        public void GetFullNames_WhenCalled_ReturnsListOfFullNames()
        {
            var people = _service.GetAll();
            var fullNames = _service.GetFullNames();

            Assert.That(fullNames, Is.Not.Null);
            Assert.That(fullNames, Is.EquivalentTo(people.Select(p => p.FullName)));
        }

        [TestCase("equal")]
        [TestCase("greater")]
        [TestCase("less")]
        [TestCase("invalid")]
        public void FilterByBirthYear_WithVariousConditions_ReturnsFilteredResult(string condition)
        {
            var result = _service.FilterByBirthYear(condition);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetById_WithExistingId_ReturnsMatchingPerson()
        {
            var first = _service.GetAll().First();
            var result = _service.GetById(first.ID);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.ID, Is.EqualTo(first.ID));
            });
        }

        [Test]
        public void Add_WhenCalled_IncreasesListCount()
        {
            var person = new Person
            {
                FirstName = "Unit",
                LastName = "Test",
                Gender = Gender.Other,
                DateOfBirth = DateTime.Now,
                PhoneNumber = "000000",
                BirthPlace = "Test",
                IsGraduated = false
            };
            var countBefore = _service.GetAll().Count;

            _service.Add(person);
            var countAfter = _service.GetAll().Count;

            Assert.That(countAfter, Is.EqualTo(countBefore + 1));
        }

        [Test]
        public void Update_WithValidData_ChangesExistingPerson()
        {
            var person = _service.GetAll().First();
            var originalName = person.FirstName;
            person.FirstName = "UpdatedName";

            _service.Update(person);
            var updated = _service.GetById(person.ID);

            Assert.Multiple(() =>
            {
                Assert.That(updated.FirstName, Is.EqualTo("UpdatedName"));
                Assert.That(updated.FirstName, Is.Not.EqualTo(originalName));
            });
        }

        [Test]
        public void Delete_WithValidId_RemovesPersonFromList()
        {
            var person = _service.GetAll().First();

            _service.Delete(person.ID);
            var result = _service.GetById(person.ID);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Paging_WithPageSize_ReturnsCorrectTotalPages()
        {
            var pageSize = 3;
            var (_, totalPages) = _service.Paging(1, pageSize);
            var expectedPages = (int)Math.Ceiling(_service.GetAll().Count / (double)pageSize);

            Assert.That(totalPages, Is.EqualTo(expectedPages));
        }

        [Test]
        public void ExportToExcel_WhenCalled_ReturnsNonEmptyByteArray()
        {
            var result = _service.ExportToExcel();

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<byte[]>());
                Assert.That(result.Length, Is.GreaterThan(0));
            });
        }
    }
}
