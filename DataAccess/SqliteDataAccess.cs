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
        public static List<OrderDisplayModel> LoadOrders()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

                var output = cnn.Query<OrderDisplayModel>("select Orders.ID, Users.UserName, Characters.CharacterName, Flasks.FlaskName, Orders.FlaskQty, Potions.PotionName, " +
                    "Orders.PotionQty, Foods.FoodName, Orders.FoodQty, Orders.Status, Orders.Crafter" +
                    " from Orders " +
                    "LEFT Join Users on Orders.UserID = Users.ID " +
                    "LEFT Join Characters on Orders.CharacterID = Characters.ID " +
                    "LEFT Join Flasks on Orders.FlaskID = Flasks.ID " +
                    "LEFT Join Potions on Orders.PotionID = Potions.ID " +
                    "LEFT Join Foods on Orders.FoodID = Foods.ID");
                return output.ToList();
            }
        }
        public static List<FlasksModel> LoadFlasks()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<FlasksModel>("select * from Flasks");
                return output.ToList();
            }
        }
        public static List<PotionsModel> LoadPotions()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PotionsModel>("select * from Potions");
                return output.ToList();
            }
        }
        public static List<FoodsModel> LoadFoods()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<FoodsModel>("select * from Foods");
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
        public static void SaveOrder(OrdersModel order)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Orders (UserID, CharacterID, FlaskId, FlaskQty, PotionID, PotionQty, FoodID, FoodQty, Status) " +
                    "values (@UserID, @CharacterID, @FlaskID, @FlaskQty, @PotionID, @PotionQty, @FoodID, @FoodQty, 'Order Placed')", order);
            }
        }
        public static void UpdateCharacter(CharactersModel person)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Characters set CharacterName = @CharacterName where ID = @ID ", person);
            }
        }
        public static void UpdateOrder(OrderDisplayModel order, UsersModel user)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Orders set Status = @Status, Crafter = @Crafter where ID = @ID ",
                    new {Status = order.Status, Crafter = user.UserName, ID = order.ID });
            }
        }
        public static void DeleteOrder(OrderDisplayModel order)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Delete from Orders where ID = @ID", order);
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
                cnn.Execute("insert into Users (UserName, Password, Role) values (@UserName, @Password, @Role)", User);
            }
        }


        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
