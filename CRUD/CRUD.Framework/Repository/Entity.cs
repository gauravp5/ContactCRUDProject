using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CRUD.Framework.Repository
{
    [ExcludeFromCodeCoverage]
    public abstract class Entity<TId> : IObjectState
    {
        [Key]
        public virtual TId Id { get; set; }

        // EF requires an empty constructor
        protected Entity()
        {
        }

        [NotMapped]
        public ObjectState ObjectState { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public abstract class EntityWithNoId : IObjectState
    {
        // EF requires an empty constructor
        protected EntityWithNoId()
        {
        }

        [NotMapped]
        public ObjectState ObjectState { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}