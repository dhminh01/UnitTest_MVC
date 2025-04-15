using Microsoft.AspNetCore.Mvc;
using Moq;
using MVC_Assignment_2.Controllers;
using MVC_Assignment_2.Models;
using MVC_Assignment_2.Services;

namespace MVC_Assignment_2.Tests
{
    [TestFixture]
    public class RookiesControllerTests
    {
        private Mock<IPersonService> _mockService;
        private RookiesController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IPersonService>();
            _controller = new RookiesController(_mockService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        private static IEnumerable<TestCaseData> PagingTestData =>
        [
            new TestCaseData(1, 5, new List<Person> { new() { ID = 1, FirstName = "John", LastName = "Doe" } }, 1),
            new TestCaseData(2, 5, new List<Person>(), 0)
        ];

        [TestCaseSource(nameof(PagingTestData))]
        public void Index_WithPaging_ReturnsCorrectPeople(int page, int size, List<Person> expected, int totalPages)
        {
            _mockService.Setup(s => s.Paging(page, size)).Returns((expected, totalPages));

            var result = _controller.Index(page) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Model, Is.EqualTo(expected));
                Assert.That(result.ViewData["TotalPages"], Is.EqualTo(totalPages));
            });
        }

        [Test]
        public void ShowDetails_ValidId_ReturnsPerson()
        {
            var person = new Person { ID = 1, FirstName = "Test", LastName = "Person" };
            _mockService.Setup(s => s.GetById(1)).Returns(person);

            var result = _controller.ShowDetails(1) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(person));
        }

        [Test]
        public void ShowDetails_InvalidId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetById(1)).Returns((Person)null!);

            var result = _controller.ShowDetails(1);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void ShowMalesOnly_ValidPerson_ReturnsOnlyMales()
        {
            var males = new List<Person>
            {
                new() { ID = 1, FirstName = "John", Gender = Gender.Male },
                new() { ID = 2, FirstName = "Bob", Gender = Gender.Male }
            };
            _mockService.Setup(s => s.GetMales()).Returns(males);

            var result = _controller.ShowMalesOnly() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(males));
        }

        [Test]
        public void ShowOldest_ValidPerson_ReturnsOldestPerson()
        {
            var person = new Person { ID = 1, FirstName = "Old", DateOfBirth = new DateTime(1940, 1, 1) };
            _mockService.Setup(s => s.GetOldest()).Returns(person);

            var result = _controller.ShowOldest() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(person));
        }

        [Test]
        public void FilterByBirthYear_ValidCondition_ReturnsFilteredList()
        {
            var condition = "equal";
            var filtered = new List<Person> { new() { ID = 1, DateOfBirth = new DateTime(1950, 1, 1) } };

            _mockService.Setup(s => s.FilterByBirthYear(condition)).Returns(filtered);

            var result = _controller.FilterByBirthYear(condition) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Model, Is.EqualTo(filtered));
                Assert.That(result.ViewData["SelectedCondition"], Is.EqualTo(condition));
            });
        }

        [Test]
        public void FilterByBirthYear_EmptyCondition_DefaultsToEqual()
        {
            _mockService.Setup(s => s.FilterByBirthYear("equal")).Returns([]);

            var result = _controller.FilterByBirthYear(string.Empty) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Model, Is.EqualTo(new List<Person>()));
                Assert.That(result.ViewData["SelectedCondition"], Is.EqualTo("equal"));
            });
        }

        [Test]
        public void Create_ValidPerson_ReturnsEmptyForm()
        {
            var result = _controller.Create() as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.Null);
        }

        [Test]
        public void Create_ValidPerson_AddsAndRedirects()
        {
            var person = new Person { FirstName = "Test", LastName = "Person" };
            var result = _controller.Create(person) as RedirectToActionResult;

            _mockService.Verify(s => s.Add(person), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public void Create_InvalidModel_ReturnsSameForm()
        {
            var person = new Person { FirstName = "Invalid" };
            _controller.ModelState.AddModelError("error", "invalid");

            var result = _controller.Create(person) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(person));
        }

        [Test]
        public void Edit_ValidId_ReturnsForm()
        {
            var person = new Person { ID = 1, FirstName = "Edit" };
            _mockService.Setup(s => s.GetById(1)).Returns(person);

            var result = _controller.Edit(1) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(person));
        }

        [Test]
        public void Edit_InvalidId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetById(999)).Returns((Person)null!);

            var result = _controller.Edit(999);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Edit_ValidModel_UpdatesAndRedirects()
        {
            var person = new Person { ID = 1, FirstName = "Updated" };

            var result = _controller.Edit(1, person) as RedirectToActionResult;

            _mockService.Verify(s => s.Update(person), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public void Edit_InvalidModel_ReturnsSameView()
        {
            var person = new Person { ID = 1, FirstName = "Fail" };
            _controller.ModelState.AddModelError("error", "invalid");

            var result = _controller.Edit(1, person) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(person));
        }

        [Test]
        public void Delete_ValidId_ReturnsForm()
        {
            var person = new Person { ID = 1, FirstName = "Delete" };
            _mockService.Setup(s => s.GetById(1)).Returns(person);

            var result = _controller.Delete(1) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.EqualTo(person));
        }

        [Test]
        public void Delete_InvalidId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetById(999)).Returns((Person)null!);

            var result = _controller.Delete(999);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void DeleteConfirmed_ValidId_DeletesAndRedirects()
        {
            var person = new Person { ID = 1, FirstName = "Delete", LastName = "Me" };
            _mockService.Setup(s => s.GetById(1)).Returns(person);

            var result = _controller.DeleteConfirmed(1) as RedirectToActionResult;

            _mockService.Verify(s => s.Delete(1), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.That(result?.ActionName, Is.EqualTo("DeleteConfirmation"));
                Assert.That(result?.RouteValues?["deletedPersonName"], Is.EqualTo("Me Delete"));
            });
        }

        [Test]
        public void DeleteConfirmed_InvalidId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetById(999)).Returns((Person)null!);

            var result = _controller.DeleteConfirmed(999);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void ExportToExcel_Always_ReturnsExcelFile()
        {
            var content = new byte[] { 1, 2, 3 };
            _mockService.Setup(s => s.ExportToExcel()).Returns(content);

            var result = _controller.ExportToExcel() as FileContentResult;

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.FileContents, Is.EqualTo(content));
                Assert.That(result.ContentType, Is.EqualTo("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
                Assert.That(result.FileDownloadName, Is.EqualTo("People.xlsx"));
            });
        }

        [Test]
        public void DeleteConfirmation_ValidName_ReturnsConfirmationView()
        {
            var name = "John Doe";

            var result = _controller.DeleteConfirmation(name) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewData["ConfirmationMessage"], Is.EqualTo($"Person {name} was removed from the list successfully!"));
        }
    }
}