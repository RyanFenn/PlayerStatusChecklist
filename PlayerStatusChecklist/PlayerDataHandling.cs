using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PlayerStatusChecklist
{
    public class PlayerDataHandling
    {
        // This filepath is relative to the location of the used executable file. You may need to select the icon to show all files if using Visual Studio.
        // For debug mode, the PlayerList.txt file can be found here: bin/Debug/net8.0-windows/PlayerList.txt .
        // If the project is published you might find another PlayerList.txt file in the same folder as the generated executable file.
        private static readonly string PLAYER_LIST_FILEPATH = Path.Combine(Directory.GetCurrentDirectory(), "PlayerList.txt");

        public static void CreateEmptyFile()
        {
            File.Create(PLAYER_LIST_FILEPATH).Dispose();
        }

        // Gets the player names from the existing text file, and returns a list of player objects.
        // The status property of each player in the list will be set to ComboBoxOptions.noResponse.
        public static ObservableCollection<Player> GetPlayersFromTextFile()
        {
            ObservableCollection<Player> players = new ObservableCollection<Player>();

            string[] names = File.ReadAllLines(PLAYER_LIST_FILEPATH);
            foreach (string n in names)
            {
                players.Add(new Player(n, ComboBoxOptions.noResponse, StatusColorSelection.LightGray));
            }
            return players;
        }

        // Creates a new text file with the player names from the ObservableCollection that is passed in.
        public static void WriteTextFile(ObservableCollection<Player> pList)
        {
            List<string> playerNames = new List<string>();

            // Adds player names to the list of strings, 
            foreach (Player p in pList)
            {
                playerNames.Add(p.Name);
            }

            File.WriteAllLines(PLAYER_LIST_FILEPATH, playerNames);
        }

        // A reference to playerCollection (ObservableCollection) is passed in, so that it can be modified.
        // Sorts the referenced list of player objects alphabetically based on the Name property.
        // This method doesn't break the playerCollection binding.
        // Solution found from here: https://stackoverflow.com/questions/19112922/sort-observablecollectionstring-through-c-sharp
        public static void SortPlayerCollectionAlphabetically(ref ObservableCollection<Player> pCollection)
        {
            var tempPlayerCollection = new ObservableCollection<Player>(pCollection.OrderBy(p => p.Name));
            pCollection.Clear();
            foreach (Player p in tempPlayerCollection) pCollection.Add(p);
        }
    }
}

