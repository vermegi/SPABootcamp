using System.Configuration;
using System.Data.Entity;
using System.Data.SQLite;
using System.Diagnostics;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Generators.SQLite;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Initialization.AssemblyLoader;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SQLite;

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
            SQLiteConnection.CreateFile("c:\\temp\\Evenementen.sqlite");
            using (var connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["evenementEntities"].ConnectionString))
            {
                
                var consoleAnnouncer = new TextWriterAnnouncer(s => Debug.WriteLine(s))
                {
                    ShowElapsedTime = false,
                    ShowSql = true
                };

                var assemblyLoader = new AssemblyLoaderFactory();
                var assembly = assemblyLoader.GetAssemblyLoader("EventPlanner.Migrations").Load();

                var runner = new MigrationRunner(assembly, new RunnerContext(consoleAnnouncer),
                    new SQLiteProcessor(connection, new SQLiteGenerator(), consoleAnnouncer, new ProcessorOptions(),
                        new SQLiteDbFactory()));

                runner.MigrateUp();
            }

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