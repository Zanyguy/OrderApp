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
        public static List<CharactersModel> LoadCharacter(int ID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<CharactersModel>("select * from Characters where ID = @ID", ID);
                return output.ToList();
            }
        }
        public static void SaveCharacter(CharactersModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Characters (UserID, CharacterName) values (@UserID, @CharacterName)", person);
            }
        }

        public static void DeleteCharacter(CharactersModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Delete from Characters where ID = @ID", person);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
