using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerStatusChecklist
{
    public class Player : ObservableObject
    {
        private string name;
        private string status;
        private string listBoxItemColor;

        public Player(string name, string status, string listBoxItemColor)
        {
            this.name = name;
            this.status = status;
            this.listBoxItemColor = listBoxItemColor;
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        public string ListBoxItemColor
        {
            get { return listBoxItemColor; }
            set
            {
                listBoxItemColor = value;
                OnPropertyChanged();
            }
        }
    }

}
