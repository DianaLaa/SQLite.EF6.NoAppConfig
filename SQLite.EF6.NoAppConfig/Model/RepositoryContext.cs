using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite.EF6.NoAppConfig.Model
{
    class RepositoryContext : DbContext
    {
        public RepositoryContext(string databasePath)
            : base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder()
                { DataSource = databasePath, ForeignKeys = false }.ConnectionString
            }, true)
        {
            // Turn off the Migrations, this is not a code first database.
            System.Data.Entity.Database.SetInitializer<RepositoryContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Person> Persons { get; set; }
    }
}
