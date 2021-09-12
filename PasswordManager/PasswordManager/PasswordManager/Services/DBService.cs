using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PasswordManager.Models;
using SQLite;
using Xamarin.Essentials;

namespace PasswordManager.Services {
    public class DBService {
        public static SQLiteAsyncConnection db;
        static async Task Init() {
            if (db != null) {
                return;
            }

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "NewDB.db");

            
            db = new SQLiteAsyncConnection(databasePath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);
            

            await db.CreateTableAsync<Credentials>();
            
        }

        #region Goals
        public static async Task AddCredential(string account, string email, string username, string password) {
            await Init();

            var cred = new Credentials() {
                Account = account,
                Email = email,
                Username = username,
                Password = password
            };

            var id = await db.InsertAsync(cred);
        }

        public static async Task UpdateCredential(int inputId, string account, string email, string username, string password) {
            await Init();

            var cred = new Credentials() {
                Id = inputId,
                Account = account,
                Email = email,
                Username = username, 
                Password = password
            };
            var id = await db.UpdateAsync(cred);
        }

        public static async Task RemoveCredential(int id) {

            await Init();

            await db.DeleteAsync<Credentials>(id);
        }

        public static async Task<IEnumerable<Credentials>> GetCredentials() {
            await Init();

            var creds = await db.Table<Credentials>().ToListAsync();
            return creds;
        }

        public static async Task<Credentials> GetCredential(int id) {
            await Init();
            var query =  db.Table<Credentials>().Where(v => id.Equals(v.Id)).FirstOrDefaultAsync();
            return query.Result;
        }
        #endregion
    }
}
