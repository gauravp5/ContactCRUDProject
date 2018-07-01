using System.Configuration;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

namespace CRUD.Framework.Repository
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseContext<TContext>
      : DbContext where TContext : DbContext
    {
        static BaseContext()
        {
            Database.SetInitializer<TContext>(null);
        }

        protected BaseContext()
            : base("name=CRUDContext")
        {

        }
    }
}
