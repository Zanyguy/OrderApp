using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrdersModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int CharacterID { get; set; }
        public int? FlaskID { get; set; }
        public int? FlaskQty { get; set; }
        public int? PotionID { get; set; }
        public int? PotionQty { get; set; }
        public int? FoodID { get; set; }
        public int? FoodQty { get; set; }
    }
}
