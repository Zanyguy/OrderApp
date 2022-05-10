using Caliburn.Micro;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.ViewModels
{
    public class CrafterViewModel: Screen
    {
        private List<CharactersModel> _characters;
        private List<FlasksModel> _flasks;
        private List<PotionsModel> _potions;
        private List<FoodsModel> _foods;
        private BindableCollection<OrderDisplayModel> _orders;
        private UsersModel _user;
        private OrderDisplayModel _selectedOrder;



        public OrderDisplayModel SelectedOrder
        {
            get { return _selectedOrder; }
            set 
            {
                _selectedOrder = value; 
                NotifyOfPropertyChange(() => SelectedOrder);
            }
        }

        public UsersModel user
        {
            get { return _user; }
            set { _user = value; }
        }
        public BindableCollection<OrderDisplayModel> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }
        public List<FoodsModel> Foods
        {
            get { return _foods; }
            set { _foods = value; }
        }
        public List<PotionsModel> Potions
        {
            get { return _potions; }
            set { _potions = value; }
        }
        public List<FlasksModel> Flasks
        {
            get { return _flasks; }
            set { _flasks = value; }
        }
        public List<CharactersModel> Characters
        {
            get { return _characters; }
            set { _characters = value; }
        }
        public CrafterViewModel(UsersModel user)
        {
            this.user = user;

            this.Characters = SqliteDataAccess.LoadCharacters(user);
            this.Flasks = SqliteDataAccess.LoadFlasks();
            this.Potions = SqliteDataAccess.LoadPotions();
            this.Foods = SqliteDataAccess.LoadFoods();
            List<OrderDisplayModel> orders = SqliteDataAccess.LoadOrders();
            this.Orders = new BindableCollection<OrderDisplayModel>(orders);
        }
        public void ClaimOrderButton()
        {
            SelectedOrder.Status = "Order Received";
            SqliteDataAccess.UpdateOrder(SelectedOrder,user);
        }
        public void CompleteOrderButton()
        {
            SelectedOrder.Status = "Order Completed";
            SqliteDataAccess.UpdateOrder(SelectedOrder, user);
        }
    }
}
