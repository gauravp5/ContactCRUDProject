using ContactManager.Core.DomainModels;
using CRUD.Framework.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Data
{
    public class ContactManagerContext : BaseContext<ContactManagerContext>
    {
        public DbSet<Contact> Contacts { get; set; }

    }
}
