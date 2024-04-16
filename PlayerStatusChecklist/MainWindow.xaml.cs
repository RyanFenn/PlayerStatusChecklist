using System.Collections.ObjectModel;
using System.IO;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlayerStatusChecklist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Player> playerCollection = new ObservableCollection<Player>();
        private Messages messages = new Messages();
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                playerCollection = PlayerDataHandling.GetPlayersFromTextFile();
            }
            catch (FileNotFoundException)
            {
                PlayerDataHandling.CreateEmptyFile();
            }

            SelectActionComboBox.ItemsSource = ComboBoxOptions.playerSelectActions;
            SelectActionComboBox.SelectedItem = ComboBoxOptions.noResponse;

            // Create an anonomous type.
            DataContext = new
            {
                messageStrings = messages,
                statusColorSelection = new StatusColorSelection()
            };

            PlayerNameListBox.ItemsSource = playerCollection;
            messages.UpdateTaggedPlayerNamesString(playerCollection);
        }

        private void PlayerNameListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Checks to make sure the selected index is not -1. If the index is -1, this means all items are unselected. This will happen when
            // PlayerNameListBox.UnselectAll() gets called at the end of this event handler. This condition prevents an exception from being thrown.
            if (PlayerNameListBox.SelectedIndex >= 0)
            {
                if (SelectActionComboBox.SelectedItem.ToString() == ComboBoxOptions.available)
                {
                    playerCollection[PlayerNameListBox.SelectedIndex].Status = ComboBoxOptions.available;
                    playerCollection[PlayerNameListBox.SelectedIndex].ListBoxItemColor = StatusColorSelection.LightGreen;
                }
                else if (SelectActionComboBox.SelectedItem.ToString() == ComboBoxOptions.maybe)
                {
                    playerCollection[PlayerNameListBox.SelectedIndex].Status = ComboBoxOptions.maybe;
                    playerCollection[PlayerNameListBox.SelectedIndex].ListBoxItemColor = StatusColorSelection.LightOrange;
                }
                else if (SelectActionComboBox.SelectedItem.ToString() == ComboBoxOptions.unavailable)
                {
                    playerCollection[PlayerNameListBox.SelectedIndex].Status = ComboBoxOptions.unavailable;
                    playerCollection[PlayerNameListBox.SelectedIndex].ListBoxItemColor = StatusColorSelection.LightRed;
                }
                else if (SelectActionComboBox.SelectedItem.ToString() == ComboBoxOptions.noResponse)
                {
                    playerCollection[PlayerNameListBox.SelectedIndex].Status = ComboBoxOptions.noResponse;
                    playerCollection[PlayerNameListBox.SelectedIndex].ListBoxItemColor = StatusColorSelection.LightGray;
                }
                else if (SelectActionComboBox.SelectedItem.ToString() == ComboBoxOptions.removePlayer)
                {
                    if (MessageBox.Show($"Are you sure you want to remove {playerCollection[PlayerNameListBox.SelectedIndex].Name} from the list?",
                        "Remove Player Warning", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        playerCollection.RemoveAt(PlayerNameListBox.SelectedIndex);
                        PlayerDataHandling.WriteTextFile(playerCollection);
                    }
                }

                messages.UpdateTaggedPlayerNamesString(playerCollection);

                // When this line is called, the ListBox selection is changed so that the selected index is -1. Because the selection has changed, the
                // PlayerNameListBox_SelectionChanged() event handler will triggered again.
                PlayerNameListBox.UnselectAll();
            }
        }
        private void AddPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewNameEntryTextBox.Text == "")
            {
                MessageBox.Show("Invalid input.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (playerCollection.Where(p => p.Name == NewNameEntryTextBox.Text).Any())   // Checks if the name entry is a duplicate.
            {
                MessageBox.Show("Name already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                playerCollection.Add(new Player(NewNameEntryTextBox.Text, ComboBoxOptions.noResponse, StatusColorSelection.LightGray));
                PlayerDataHandling.SortPlayerCollectionAlphabetically(ref playerCollection);
                PlayerDataHandling.WriteTextFile(playerCollection);
                messages.UpdateTaggedPlayerNamesString(playerCollection);
            }
            NewNameEntryTextBox.Text = "";
        }

        // When the copy button is clicked in the user interface, the generated message (user input + tagged player names) will be copied
        // to the clipboard. After the string is copied, it can then be pasted in WhatsApp. Note that each tagged player will need to be
        // manually hyperlinked in WhatsApp so that these players will be notified.
        private void CopyMessageButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(messages.UserInputString + messages.TaggedPlayerNamesString);
        }

        // Allows the window to be moved around.
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Closes the window.
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}