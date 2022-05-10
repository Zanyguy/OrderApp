using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDisplayModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string CharacterName { get; set; }
        public string FlaskName { get; set; }
        public int FlaskQty { get; set; }
        public string PotionName { get; set; }
        public int PotionQty { get; set; } 
        public string FoodName { get; set; }
        public int  FoodQty { get; set; }
        public string Status { get; set; }
        public string Crafter { get; set; }

    }
}
