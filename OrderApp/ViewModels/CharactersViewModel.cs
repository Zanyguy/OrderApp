using Caliburn.Micro;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.ViewModels
{
    public class CharactersViewModel : Screen
    {
        private UsersModel _user;
        private List<CharactersModel> _people;
        private string _characterName;

        private CharactersModel _selectedChar;

        public CharactersModel SelectedChar
        {
            get { return _selectedChar; }
            set { _selectedChar = value; }
        }



        public string CharacterName
        {
            get { return _characterName; }
            set { _characterName = value; }
        }


        public UsersModel User
        {
            get { return _user; }
            set { _user = value; }
        }

        

        public List<CharactersModel> People
        {
            get { return _people; }
            set 
            {
                _people = value;
                NotifyOfPropertyChange();
            }
        }


        public CharactersViewModel(UsersModel user)
        {
            this.User = user;
            this.People = SqliteDataAccess.LoadCharacters(User);
        }

        public void CreateCharButton()
        {
            SqliteDataAccess.SaveCharacter(new CharactersModel { CharacterName = CharacterName, UserID = User.ID });
            this.People = SqliteDataAccess.LoadCharacters(User);
        }
        public void DeleteCharButton()
        {
            SqliteDataAccess.DeleteCharacter(SelectedChar);
            this.People = SqliteDataAccess.LoadCharacters(User);
        }

    }

}
