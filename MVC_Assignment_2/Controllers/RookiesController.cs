using Microsoft.AspNetCore.Mvc;
using MVC_Assignment_2.Services;
using MVC_Assignment_2.Models;

namespace MVC_Assignment_2.Controllers
{
    public class RookiesController(IPersonService personService) : Controller
    {
        private readonly IPersonService _personService = personService;
        private const int PageSize = 5;

        public IActionResult Index(int page = 1)
        {
            var (pagedPeople, totalPages) = _personService.Paging(page, PageSize);

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;

            return View(pagedPeople);
        }

        public IActionResult ShowDetails(int ID)
        {
            var person = _personService.GetById(ID);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }


        public IActionResult ShowMalesOnly()
        {
            var person = _personService.GetMales();

            return View(person);
        }

        public IActionResult ShowOldest()
        {
            var oldest = _personService.GetOldest();

            return View(oldest);
        }

        public IActionResult FilterByBirthYear(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                condition = "equal";
            }
            var people = _personService.FilterByBirthYear(condition);
            ViewData["SelectedCondition"] = condition;

            return View(people);
        }

        public IActionResult ExportToExcel()
        {
            var fileContent = _personService.ExportToExcel();

            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "People.xlsx");
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                _personService.Add(person);
                return RedirectToAction("Index");
            }

            return View(person);
        }

        public IActionResult Edit(int id)
        {
            var person = _personService.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(int id, Person person)
        {
            if (id != person.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _personService.Update(person);
                return RedirectToAction("Index");
            }

            return View(person);
        }

        public IActionResult Delete(int id)
        {
            var person = _personService.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var person = _personService.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            _personService.Delete(id);

            return RedirectToAction("DeleteConfirmation", new { deletedPersonName = person.FullName });
        }

        public IActionResult DeleteConfirmation(string deletedPersonName)
        {
            ViewData["ConfirmationMessage"] = $"Person {deletedPersonName} was removed from the list successfully!";
            return View();
        }
    }
}