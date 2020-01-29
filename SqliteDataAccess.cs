using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class SqliteDataAccess
    {


        /// <summary>
        /// Returns a list of People whose are in the database.
        /// </summary>
        /// <returns></returns>
        public static List<Person> LoadPeople()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Person>("select * from Person", new DynamicParameters());
                return output.ToList();
            }
        }


        /// <summary>
        /// Saving the Person to the database.
        /// </summary>
        /// <param name="person"></param>
        public static void SavePerson(Person person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Person (firstName , lastName,time,bestScore) values (@firstName, @lastName, @time, @bestScore)",person);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
