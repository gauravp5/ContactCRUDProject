using ContactManager.Core.Services;
using CRUD.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CRUD.Contoller
{
    public class ContactController : Controller
    {
        private readonly IContactManagerService ContactManagerService;
        public ContactController(IContactManagerService service)
        {
            ContactManagerService = service;
        }

        public async Task<ActionResult> Index()
        {
            var contacts = await ContactManagerService.GetAll();
            var records = new List<ContactViewModel>();
            foreach (var item in contacts)
                records.Add(new ContactViewModel(item));
            return View(records);
        }

        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contactDetail = await ContactManagerService.Get(id);
            var records = new ContactViewModel();

            if (contactDetail != null)
            {
                records.ID = contactDetail.Id;
                records.Email = contactDetail.Email;
                records.FirstName = contactDetail.FirstName;
                records.LastName = contactDetail.LastName;
                records.PhoneNumber = contactDetail.PhoneNumber;
                records.Status = contactDetail.Status;
            }
            if (contactDetail == null)
            {
                return HttpNotFound();
            }
            return View(records);
        }

        [HttpGet]
        public ActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateContact(ContactViewModel std)
        {
            if (ModelState.IsValid)
            {
                var result = await ContactManagerService.Create(std.GetModel());
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Deactivate(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contactDetail = ContactManagerService.Deactivate(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contactDetail = await ContactManagerService.Get(id);
            var records = new ContactViewModel();
            
            if (contactDetail != null)
            {
                records.ID = contactDetail.Id;
                records.Email = contactDetail.Email;
                records.FirstName = contactDetail.FirstName;
                records.LastName = contactDetail.LastName;
                records.PhoneNumber = contactDetail.PhoneNumber;
                records.Status = contactDetail.Status;
            }
            if (contactDetail == null)
            {
                return HttpNotFound();
            }
            return View(records);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactViewModel contactDetail)
        {
            if (ModelState.IsValid)
            {
                var result = ContactManagerService.Modify(contactDetail.GetModel());
                return RedirectToAction("Index");
            }
            return View(contactDetail);
        }
    }
}