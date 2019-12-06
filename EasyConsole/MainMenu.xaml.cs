using EasySave;
using EasySave.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        //private bool IsProcessusActive = false;
        private BackGroundSave SaveClass;

        public MainMenu(string path = @"..\SaveState\")
        {
            this.path = path;
            SaveClass = new BackGroundSave(path);
            this.DataContext = backups;
            InitializeComponent();
            Tools.FileCreations(path);
        }

        private void ProcessusCkeck(object sender, System.Timers.ElapsedEventArgs e)
        {
            Process[] name = Process.GetProcessesByName("");
            if (name.Length != 0)
            {
                //IsProcessusActive = true;

            }
            // Button btn = (Button)sender;
        }

        private void Display_Save_Loaded(object sender, RoutedEventArgs e)
        {
            //ListView.Items.Clear();
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
            Button btn = (Button)sender;
            ModifySave winModif = new ModifySave((Backup)btn.DataContext, backups.IndexOf((Backup)btn.DataContext));
            winModif.Show();
            winModif.MyEvent += ModifyList;
        }

        private void ModifyList(object sender, RoutedEventArgs e)
        {
            IndexAndBackup indexAndBackup = (IndexAndBackup)sender;
            if (indexAndBackup.index == -1)
            {
                backups.Add(indexAndBackup.backup);
                ListView.Items.Add(indexAndBackup.backup);
            }
            else
            {
                backups[indexAndBackup.index] = indexAndBackup.backup;
                ListView.Items.Remove(indexAndBackup.backup);
                ListView.Items.Insert(indexAndBackup.index, indexAndBackup.backup);
            }
            Tools.WriteData(Tools.ObjectToJson(backups), path + @"InMemorySave.json");
        }

        private void Button_Click_MonoSave(object sender, RoutedEventArgs e)
        {
            Process[] name = Process.GetProcessesByName("Calculator");
            if (name.Length != 0)
            {
                MessageBoxResult messageBox = MessageBox.Show("The business software is launched you cannot start a backup", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //Console.WriteLine("No Process found");
                var item = (sender as FrameworkElement).DataContext;
                int index = ListView.Items.IndexOf(item);
                SaveClass.StartMonoSave((Backup)ListView.Items[index]);
                backups[index] = (Backup)ListView.Items[index];
                //ListView.ref
                //ListView.Items.Remove(indexAndBackup.backup);
                //ListView.Items.Insert(indexAndBackup.index, indexAndBackup.backup);
                Tools.WriteData(Tools.ObjectToJson(backups), path + @"InMemorySave.json");
            }

        }

        private void Button_Click_SequentialSave(object sender, RoutedEventArgs e)
        {
            Process[] name = Process.GetProcessesByName("Calculator");
            if (name.Length != 0)
            {
                MessageBoxResult messageBox = MessageBox.Show("The business software is launched you cannot start a backup", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                List<Backup> backups = new List<Backup>();
                List<int> indexs = new List<int>();
                foreach (var item in ListView.SelectedItems)
                {
                    backups.Add((Backup)item);
                    indexs.Add(ListView.Items.IndexOf(item));
                }
                    
                SaveClass.StartSequentialSaves(backups);
                Tools.WriteData(Tools.ObjectToJson(this.backups), path + @"InMemorySave.json");
            }
            
        }

        private void Button_Click_CreateSave(object sender, RoutedEventArgs e)
        {
            ModifySave modifySave = new ModifySave();
            modifySave.Show();
            modifySave.MyEvent += ModifyList;
        }

    }
}
