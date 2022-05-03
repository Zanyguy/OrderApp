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
        private string _newCharName;

        public string NewCharName
        {
            get { return _newCharName; }
            set 
            { 
                _newCharName = value;
                NotifyOfPropertyChange();
            }
        }
        public CharactersModel SelectedChar
        {
            get { return _selectedChar; }
            set { 
                _selectedChar = value;
                NotifyOfPropertyChange();
            }
        }

        public string CharacterName
        {
            get { return _characterName; }
            set 
            {
                _characterName = value;
                NotifyOfPropertyChange();
            }
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
            CharacterName = "";
        }
        public void DeleteCharButton()
        {
            SqliteDataAccess.DeleteCharacter(SelectedChar);
            this.People = SqliteDataAccess.LoadCharacters(User);
        }
        public void UpdateCharButton()
        {
            SelectedChar.CharacterName = NewCharName;
            SqliteDataAccess.UpdateCharacter(SelectedChar);
            this.People = SqliteDataAccess.LoadCharacters(User);
            NewCharName = "";
        }

    }

}
