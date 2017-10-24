using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PripravaNaTest
{
    public class PersonDatabase
    {
        public SQLiteAsyncConnection database;

        public PersonDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Person>().Wait();
        }
        public Task<List<Person>> QueryCustomExist(string name)
        {
            return database.QueryAsync<Person>("select ID FROM [Person] where Name ='" + name + "'");
        }
        public Task<List<Person>> QueryCustom()
        {
            return database.QueryAsync<Person>("select * FROM [Person]");
        }
      
        // Query
        public Task<List<Person>> GetItemsAsync()
        {
            return database.Table<Person>().ToListAsync();
        }
        public Task<List<Person>> Add(string data)
        {
            return database.QueryAsync<Person>("INSERT INTO [Person] (userId,id,title,completed) VALUES "+ data);
        }

        // Query using SQL query string
        public Task<List<Person>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Person>("SELECT * FROM [Allergen] WHERE [Done] = 0");
        }

        // Query using LINQ
        public Task<Person> GetItemAsync(int id)
        {
            return database.Table<Person>().Where(i => i.userId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Person item)
        {
                return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(Person item)
        {
            return database.DeleteAsync(item);
        }
    }
}
