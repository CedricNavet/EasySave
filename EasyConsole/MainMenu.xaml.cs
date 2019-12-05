using EasySave;
using EasySave.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

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
        private BackGroundSave SaveClass;

        public MainMenu(string path = @"..\SaveState\")
        {
            this.path = path;
            SaveClass = new BackGroundSave(path);
            this.DataContext = backups;
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

            //var temp = (Backup)ListView.Items[indexAndBackup.index];
            //temp.BackupType = indexAndBackup.backup.BackupType;
            Tools.WriteData(Tools.ObjectToJson(backups), path + @"InMemorySave.json");
        }

        private void Button_Click_MonoSave(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = ListView.Items.IndexOf(item);
            SaveClass.StartMonoSave((Backup)ListView.Items[index]);
        }

        private void Button_Click_SequentialSave(object sender, RoutedEventArgs e)
        {
            List<Backup> backups = new List<Backup>();
            foreach (var item in ListView.SelectedItems)
            {
                backups.Add((Backup)item);
            }
            SaveClass.StartSequentialSaves(backups);
        }

        private void Button_Click_CreateSave(object sender, RoutedEventArgs e)
        {
            ModifySave modifySave = new ModifySave();
            modifySave.Show();
            modifySave.MyEvent += ModifyList;
        }
    }
}