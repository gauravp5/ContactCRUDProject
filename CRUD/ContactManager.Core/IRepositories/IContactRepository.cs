using CRUD.Framework.Repository;
using ContactManager.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.IRepositories
{
    public interface IContactRepository : IEntityRepository<Contact>
    {
    }
}
