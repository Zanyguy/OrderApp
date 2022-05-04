using Caliburn.Micro;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.ViewModels
{
    public class OrdersViewModel: Screen
    {
        private UsersModel user;
        private List<FlasksModel> _flasks;
        private List<CharactersModel> _characters;
        private List<PotionsModel> _potions;
        private List<FoodsModel> foodsModels;
        private FlasksModel? _selectedFlask = null;
        private PotionsModel? _selectedPotion = null;
        private FoodsModel? _selectedFood = null;
        private CharactersModel _selectedChar;
        private int _flaskQty;
        private int _potionQty;
        private int _foodQty;
        private List<OrderDisplayModel> _orders;

        public List<OrderDisplayModel> Orders
        {
            get { return _orders; }
            set 
            { 
                _orders = value;
                NotifyOfPropertyChange();
            }
        }

        public int FoodQty
        {
            get { return _foodQty; }
            set { _foodQty = value; }
        }

        public int PotionQty
        {
            get { return _potionQty; }
            set { _potionQty = value; }
        }

        public int FlaskQty
        {
            get { return _flaskQty; }
            set { _flaskQty = value; }
        }

        public CharactersModel SelectedChar
        {
            get { return _selectedChar; }
            set { _selectedChar = value; }
        }

        public FoodsModel SelectedFood
        {
            get { return _selectedFood; }
            set { _selectedFood = value; }
        }


        public PotionsModel SelectedPotion
        {
            get { return _selectedPotion; }
            set { _selectedPotion = value; }
        }

        public List<FoodsModel> Foods
        {
            get { return foodsModels; }
            set { foodsModels = value; }
        }

        public List<PotionsModel> Potions
        {
            get { return _potions; }
            set { _potions = value; }
        }

        public FlasksModel SelectedFlask
        {
            get { return _selectedFlask; }
            set 
            { 
                _selectedFlask = value;
                NotifyOfPropertyChange();
            }
        }

        public List<CharactersModel> Characters
        {
            get { return _characters; }
            set 
            {
                _characters = value;
                NotifyOfPropertyChange();
            }
        }

        public List<FlasksModel> Flasks
        {
            get { return _flasks; }
            set 
            {
                _flasks = value;
                NotifyOfPropertyChange();
            }
        }



        public OrdersViewModel(UsersModel user)
        {
            this.user = user;

            this.Characters = SqliteDataAccess.LoadCharacters(user);
            this.Flasks = SqliteDataAccess.LoadFlasks();
            this.Potions = SqliteDataAccess.LoadPotions();
            this.Foods = SqliteDataAccess.LoadFoods();
            this.Orders = SqliteDataAccess.LoadOrders();

        }
        public void NewOrderButton()
        {
            OrdersModel order = new OrdersModel();
            order.UserID = user.ID;
            order.CharacterID = SelectedChar.ID;
            if (SelectedFlask != null)
            {
                order.FlaskID = SelectedFlask.ID;
                order.FlaskQty = FlaskQty;
            }
            if (SelectedFood != null)
            {
                order.FoodID = SelectedFood.ID;
                order.FoodQty = FoodQty;
            }
            if (SelectedPotion != null) 
            { 
                order.PotionID = SelectedPotion.ID;
                order.PotionQty = PotionQty;
            }

            SqliteDataAccess.SaveOrder(order);

        }
    }
}
