using ContactManager.Core.Services;
using CRUD.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CRUD.Contoller
{
    /// <summary>
    /// Class to handle Contact related operation.
    /// </summary>
    public class ContactController : Controller
    {
        private readonly IContactManagerService ContactManagerService;
        public ContactController(IContactManagerService service)
        {
            ContactManagerService = service;
        }

        /// <summary>
        /// Gives the list of records.
        /// </summary>
        /// <returns>Lis of Contact</returns>
        public async Task<ActionResult> Index()
        {
            var contacts = await ContactManagerService.GetAll();
            var records = new List<ContactViewModel>();
            foreach (var item in contacts)
                records.Add(new ContactViewModel(item));
            return View(records);
        }

        /// <summary>
        /// shows the details of contact based on ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Contact</returns>
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

        /// <summary>
        /// This function used for entering the new records
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpGet]
        public ActionResult CreateContact()
        {
            return View();
        }

        /// <summary>
        /// This is for saving the contact details to DB.
        /// </summary>
        /// <param name="std"></param>
        /// <returns>ActionResult</returns>
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

        /// <summary>
        /// Set status to active or deactive based on ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        public ActionResult ActivateDeactivate(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contactDetail = ContactManagerService.ActiveDeactivate(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the the edit record based on selected record ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Saves the contact details after Edit.
        /// </summary>
        /// <param name="contactDetail"></param>
        /// <returns></returns>
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