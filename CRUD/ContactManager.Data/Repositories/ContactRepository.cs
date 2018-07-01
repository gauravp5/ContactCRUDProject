using ContactManager.Core.DomainModels;
using ContactManager.Core.IRepositories;
using CRUD.Framework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Data.Repositories
{
    public class ContactRepository : EntityRepository<Contact>, IContactRepository
    {
        public ContactRepository(ContactManagerContext context)
            : base(context)
        {

        }
    }
}
