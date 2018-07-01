using ContactManager.Core.DomainModels;
using ContactManager.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.UnitTests
{
    [TestClass]
    public class ContactUnitTest
    {
        public ContactUnitTest()
        {
            // create some mock contacts to play with
            IList<Contact> contacts = new List<Contact>
                {
                    new Contact {Id=1, Email = "gaurav@a.com",FirstName= "Gaurav",
                        LastName = "Prasad",  PhoneNumber= "8087483641", Status=0 },
                     new Contact {Id=2, Email = "saurav@a.com",FirstName= "Saurav",
                        LastName = "Prasad",  PhoneNumber= "9087483642", Status=0 },
                     new Contact {Id=3, Email = "rahul@a.com",FirstName= "Rahul",
                        LastName = "Kumar",  PhoneNumber= "7087483643", Status=0 }
                };

            // Mock the contacts Repository using Moq
            Mock<IContactManagerService> mockContactRepository = new Mock<IContactManagerService>();

            // Return all the contacts
            mockContactRepository.Setup(x=>x.GetAll().AsyncState).Returns(contacts);

            // return a contact by Id
            mockContactRepository.Setup(mr => mr.Get(
                It.IsAny<int>()).AsyncState).Returns((int i) => contacts.Where(x => x.Id == i).Single());

            // Allows us to test saving a contact
            mockContactRepository.Setup(mr => mr.Create(It.IsAny<Contact>()).AsyncState).Returns(
                (Contact target) =>
                {
                    DateTime now = DateTime.Now;

                    if (target.Id.Equals(default(int)))
                    {
                        target.ModifiedOn = now;
                        target.CreatedOn = now;
                        target.Id = contacts.Count + 1;
                        contacts.Add(target);
                    }
                    else
                    {
                        var original = contacts.Where(
                            q => q.Id == target.Id).Single();

                        if (original == null)
                        {
                            return false;
                        }

                        original.FirstName = target.FirstName;
                        original.LastName = target.LastName;
                        original.PhoneNumber = target.PhoneNumber;
                        original.Email = target.Email;
                        original.Status = target.Status;
                    }

                    return true;
                });

            // Complete the setup of our Mock Contact Repository
            this.MockContactsRepository = mockContactRepository.Object;
        }

        /// <summary>
        /// Our Mock Contacts Repository for use in testing
        /// </summary>
        public readonly IContactManagerService MockContactsRepository;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Can we return a contact By Id?
        /// </summary>
        [TestMethod]
        public async Task CanReturnContactById()
        {
            // Try finding a contact by id
            Contact testContact = await this.MockContactsRepository.Get(2);

            Assert.IsNotNull(testContact); // Test if null
            Assert.IsInstanceOfType(testContact, typeof(Contact)); // Test type
            Assert.AreEqual("Saurav", testContact.FirstName); // Verify it is the right contact
        }

        [TestMethod]
        public async Task CanReturnAllContacts()
        {
            // Try finding all contacts
            IList<Contact> testContacts =await this.MockContactsRepository.GetAll();

            Assert.IsNotNull(testContacts); // Test if null
            Assert.AreEqual(3, testContacts.Count); // Verify the correct Number
        }


        /// <summary>
        /// Can we insert a new contact?
        /// </summary>
        [TestMethod]
        public async Task CanInsertContact()
        {
            // Create a new contact, not I do not supply an id
            Contact newContact = new Contact
            {
                Email = "Raurav@a.com",
                FirstName = "Raurav",
                LastName = "Srasad",
                PhoneNumber = "8237483652",
                Status = 0
            };

            IList<Contact> contact =await this.MockContactsRepository.GetAll();
            int contactCount = contact.Count();
            Assert.AreEqual(3, contactCount); // Verify the expected Number pre-insert

            // try saving our new contact
            await this.MockContactsRepository.Create(newContact);

            // demand a recount and verify that our new contact has been saved
            contact = await this.MockContactsRepository.GetAll();
            contactCount = contact.Count();
            Assert.AreEqual(4, contactCount); // Verify the expected Number post-insert
        }

        /// <summary>
        /// Can we update a Contact?
        /// </summary>
        [TestMethod]
        public async Task CanUpdateContact()
        {
            // Find a Contact by id
            Contact testContact = await MockContactsRepository.Get(1);

            // Change one of its properties
            testContact.FirstName = "C# 3.5 Unleashed";

            // Save our changes.
            await this.MockContactsRepository.Create(testContact);

            // Verify the change
            Contact retestContact = await this.MockContactsRepository.Get(1);
            Assert.AreEqual("C# 3.5 Unleashed",retestContact.FirstName);
        }
    }
}
