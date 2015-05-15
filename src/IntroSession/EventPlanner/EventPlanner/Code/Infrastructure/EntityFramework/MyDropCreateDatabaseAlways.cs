using System.Data.Entity;

namespace EventPlanner.Code.Infrastructure.EntityFramework
{
    public class MyDropCreateDatabaseAlways<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Executes the strategy to initialize the database for the given context.
        /// 
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void InitializeDatabase(TContext context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                    context.Database.Connection.Database));

            context.Database.Delete();
            context.Database.Create();
            this.Seed(context);
            context.SaveChanges();
        }

        /// <summary>
        /// A that should be overridden to actually add data to the context for seeding.
        ///                 The default implementation does nothing.
        /// 
        /// </summary>
        /// <param name="context">The context to seed.</param>
        protected virtual void Seed(TContext context)
        {
        }
    }
}