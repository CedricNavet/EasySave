using EasySave;
using EasySave.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EasyConsole
{
    /// <summary>
    /// Logique d'interaction pour MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private string path;
        private IList<Backup> backups = new List<Backup>();
        private bool IsProcessusActive = false;

        public MainMenu(string path = @"..\SaveState\")
        {
            this.path = path;
            InitializeComponent();
            Tools.FileCreations(path);
            System.Timers.Timer timer = new System.Timers.Timer(10);
            timer.Elapsed += ProcessusCkeck;
            timer.Start();
        }

        private void ProcessusCkeck(object sender, System.Timers.ElapsedEventArgs e)
        {
            Process[] name = Process.GetProcessesByName("");
            if (name.Length != 0)
            {
                IsProcessusActive = true;

            }

            // Button btn = (Button)sender;
        }

        private void Display_Save_Loaded(object sender, RoutedEventArgs e)
        {
            backups = Tools.JsonToObject<Backup>(Tools.ReadData(path + @"InMemorySave.json"));
            foreach (var item in backups)
            {
                ListView.Items.Add(item);
            }

        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete this save?",
                                          "Confirmation",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Button btn = (Button)sender;
                backups.Remove((Backup)btn.DataContext);
                ListView.Items.Remove((Backup)btn.DataContext);
                Tools.WriteData(Tools.ObjectToJson(backups), path + @"InMemorySave.json");
            }
        }

        private void Button_Click_Modify(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_Click_MonoSave(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_SequentialSave(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
