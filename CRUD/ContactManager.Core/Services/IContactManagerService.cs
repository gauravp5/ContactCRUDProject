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
        Task<List<Contact>> GetByPage(int pageNumber, int pageCount);
        Task<bool> Modify(Contact existingContact);
        Task<bool> Deactivate(long? id);
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

        public async Task<bool> Deactivate(long? id)
        {
            var contact = Repository.All.FirstOrDefault(x => x.Id == id);
            if (contact == null) //TODO:Throw contactnotfound exception
                return false;
            contact.Status = Status.InActive;
            contact.ObjectState = CRUD.Framework.Repository.ObjectState.Modified;
            Repository.Update(contact);
            return true;
        }

        public async Task<Contact> Get(long? id)
        {
            return Repository.All.FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<Contact>> GetByPage(int pageNumber, int pageCount)
        {
            return Repository.All.Skip(pageCount * pageNumber - 1).Take(pageCount).ToList();
        }

        public async Task<bool> Modify(Contact existingContact)
        {
            existingContact.ModifiedOn = DateTime.Now;
            existingContact.ObjectState = CRUD.Framework.Repository.ObjectState.Modified;
            Repository.Update(existingContact);
            return true;
        }

        public async Task<List<Contact>> GetAll()
        {
            return Repository.All.ToList();
        }
        public void Dispose()
        {
            Repository.Save();
        }
    }
}
