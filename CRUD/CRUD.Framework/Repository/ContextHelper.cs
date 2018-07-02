using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

namespace CRUD.Framework.Repository
{
    [ExcludeFromCodeCoverage]
    public static class ContextHelpers
    {
        //Only use with short lived contexts
        public static void ApplyStateChanges(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectState>())
            {
                IObjectState stateInfo = entry.Entity;
                entry.State = StateHelper.ConvertState(stateInfo.ObjectState);
            }
        }

        public static void ResetStates(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectState>())
                entry.Entity.ObjectState = ObjectState.Unchanged;
        }
    }
}