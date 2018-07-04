using ContactManager.Core.DomainModels;
using ContactManager.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.Services
{
    public interface IContactManagerService : IDisposable
    {
        Task<bool> Create(Contact newContact);
        Task<Contact> Get(long? id);
        Task<List<Contact>> GetAll();
        Task<bool> Modify(Contact existingContact);
        Task<bool> ActiveDeactivate(long? id);
    }

    public class ContactManagerService : IContactManagerService
    {
        private readonly IContactRepository Repository;
        public ContactManagerService(IContactRepository repository)
        {
            Repository = repository;
        }

        public async Task<bool> Create(Contact newContact)
        {
            try
            {
                newContact.ObjectState = CRUD.Framework.Repository.ObjectState.Added;
                Repository.Create(newContact);
            }
            catch (Exception exception)
            {
                //TODO: Log exception
                return false;
            }
            return true;
        }

        public async Task<bool> ActiveDeactivate(long? id)
        {
            var contact = Repository.All.FirstOrDefault(x => x.Id == id);
            if (contact == null) //TODO:Throw contactnotfound exception
                return false;
            if (contact.Status == Status.Active)
                contact.Status = Status.InActive;
            else
                contact.Status = Status.Active;
            contact.ObjectState = CRUD.Framework.Repository.ObjectState.Modified;
            Repository.Update(contact);
            return true;
        }

        public async Task<Contact> Get(long? id)
        {
            return Repository.All.FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> Modify(Contact existingContact)
        {
            existingContact.ModifiedOn = DateTime.Now;
            existingContact.ObjectState = CRUD.Framework.Repository.ObjectState.Modified;
            Repository.Update(existingContact);
            return true;
        }

        public async virtual Task<List<Contact>> GetAll()
        {
            return Repository.All.ToList();
        }
        public void Dispose()
        {
            Repository.Save();
        }
    }
}
