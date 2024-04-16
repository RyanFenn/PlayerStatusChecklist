using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PlayerStatusChecklist
{
    public class Messages : ObservableObject
    {
        private string userInputString = "";
        private string taggedPlayerNamesString = "";

        public Messages()
        {
            UserInputString = "Please respond to the poll.";
            TaggedPlayerNamesString = "";
        }

        // A string containing the custom input message. Example: "Please respond to the poll."
        public string UserInputString
        {
            get { return userInputString; }
            set
            {
                userInputString = value;
                OnPropertyChanged();
            }
        }

        // A string containing a list of tagged player names. Example: "@Player 1 @Player 2 @ Player 3".
        public string TaggedPlayerNamesString
        {
            get { return taggedPlayerNamesString; }
            set
            {
                taggedPlayerNamesString = value;
                OnPropertyChanged();
            }
        }

        // The player collection that is passed in is looped through to check the status of each player. If the player's
        // status is "ComboBoxOptions.noResponse", this player's name will be added to the string of players who haven't responded.
        public void UpdateTaggedPlayerNamesString(ObservableCollection<Player> pCollection)
        {
            TaggedPlayerNamesString = "\n\n";

            foreach (Player p in pCollection)
            {
                if (p.Status == ComboBoxOptions.noResponse)
                {
                    TaggedPlayerNamesString += $"@{p.Name} ";
                }
            }
        }
    }
}
