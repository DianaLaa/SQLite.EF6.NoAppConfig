using SQLite.EF6.NoAppConfig.Migration;
using SQLite.EF6.NoAppConfig.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite.EF6.NoAppConfig
{
    class Program
    {
        public static void Main()
        {
            string fileName = "database.sqlite";
            Console.WriteLine("Create empty database");
            CreateDatabase create = new CreateDatabase(fileName);
            create.Run();

            FileInfo file = new FileInfo(fileName);
            Console.WriteLine($"Database created at {file.FullName}.");

            Console.WriteLine("Create repository and add data.");
            DataRepository repository = new DataRepository(fileName);
            repository.AddPerson("Marc");
            Console.WriteLine("Added data.");

            Console.WriteLine("Retrieve data:");
            Person person1 = repository.GetPerson(1);
            Console.WriteLine($"    Person with id {1} is {person1.Name}");

            Person eric = repository.GetPerson("Eric");
            Debug.Assert(eric == null);
            Console.WriteLine("    There is no person named Eric.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
