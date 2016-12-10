using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventStoreApp.Models;
using EventStoreApp.Models.Abstract;
using EventStoreApp.Models.Entities;
using EventStoreApp.Models.EventViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NuGet.Protocol.Core.Types;

namespace EventStoreApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository repository;
        private readonly UserManager<ApplicationUser> manager;
        public int PageSize = 9;

        public EventController(IEventRepository repository, UserManager<ApplicationUser> manager)
        {
            this.repository = repository;
            this.manager = manager;
        }

        public ViewResult Index(string searchString, int page=1)
        {
            IEventList builder = new EventListViewBuilder(repository);
            var viewModel = new EventListViewModel
            {
                EventList = builder.ListEvents(searchString, page),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Events.Count()
                }
            };
            return View(viewModel);
        }

        public IActionResult Details(string shortName)
        {
            if (string.IsNullOrEmpty(shortName))
            {
                return BadRequest();
            }
            EventViewModelBuilder builder = new EventViewModelBuilder();
            EventViewModel e = builder.CreateEventViewModel(repository.GetEvent(shortName));
            if (e == null)
            {
                return NotFound();
            }
            return View(e);
        }

        [HttpGet]
        public ActionResult Create() => View("Edit", new Event());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event item)
        {
            var currentUser = manager.FindByNameAsync(User.Identity.Name);
            item.Owner = currentUser.Result;
            if (!ModelState.IsValid) return View(item);
            repository.SaveEvent(item);
            //TempData["message"] = $"{item.Name} добавлен успешно";
            return RedirectToAction("Index");
        }

        public ViewResult Edit(string shortName) => View(repository.Events.FirstOrDefault(i=>i.ShortName == shortName));

        [HttpPost]
        public IActionResult Edit(Event item)
        {
            if (ModelState.IsValid)
            {
                repository.SaveEvent(item);
                //TempData["message"] = $"{item.Name} изменен успешно";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
            
        }

        [HttpPost]
        public IActionResult Delete(int eventId)
        {
            Event eventToDelete = repository.DeleteEvent(eventId);
           /* if (eventToDelete != null)
            {
                TempData["message"] = $"{eventToDelete.Name} ���� ������� �������";
            }*/
            return RedirectToAction("Index");
        }
    }
}