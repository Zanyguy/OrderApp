using Dapper;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SqliteDataAccess
    {
        public static List<CharactersModel> LoadCharacters(UsersModel User)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<CharactersModel>("select * from Characters where UserID = @ID", User);
                return output.ToList();
            }
        }
        public static CharactersModel LoadCharacter(int ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.QuerySingle<CharactersModel>("select * from Characters where ID = @ID", ID);
                return output;
            }
        }
        public static bool UserExists(string UserName)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var exists = cnn.ExecuteScalar<bool>("select count(1) from Users where UserName=@UserName", new { UserName });
                return exists;
            }
        }
        public static void SaveCharacter(CharactersModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Characters (UserID, CharacterName) values (@UserID, @CharacterName)", person);
            }
        }
        public static void UpdateCharacter(CharactersModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Characters set CharacterName = @CharacterName where ID = @ID ", person);
            }
        }

        public static void DeleteCharacter(CharactersModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Delete from Characters where ID = @ID", person);
            }
        }

        public static UsersModel LoginAttempt(String UserName)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.QuerySingle<UsersModel>("select * from Users where UserName = @UserName", new { UserName=UserName });
                return output;
            }
        }
        public static void RegisterUser(UsersModel User)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Users (UserName, Password) values (@UserName, @Password)", User);
            }
        }


        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
