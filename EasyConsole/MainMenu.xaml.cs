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
        private BackGroundSave SaveClass;
        private string business_soft;

        public MainMenu(string fileExtension, string business_soft, string CryptoSoft, string path = @"..\SaveState\")
        {
            this.business_soft = business_soft;
            this.path = path;
            SaveClass = new BackGroundSave(path, fileExtension, business_soft, CryptoSoft);
            InitializeComponent();
            Tools.FileCreations(path);
        }


        private void Display_Save_Loaded(object sender, RoutedEventArgs e)
        {
            //ListView.Items.Clear();
            foreach (var item in Tools.JsonToObject<Backup>(Tools.ReadData(path + @"InMemorySave.json")))
            {
                ListView.Items.Add(item);
            }
            ListView.Items.Refresh();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show(Properties.Resources.checkDelete,
                                      Properties.Resources.Confirmation,
                                      MessageBoxButton.YesNo,
                                      MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Button btn = (Button)sender;
                //backups.Remove((Backup)btn.DataContext);
                ListView.Items.Remove((Backup)btn.DataContext);
                IList<Backup> backups = new List<Backup>();
                foreach (var item in ListView.Items)
                {
                    backups.Add((Backup)item);
                }
                ListView.Items.Refresh();
                Tools.WriteData(Tools.ObjectToJson(backups), path + @"InMemorySave.json");
            }
        }

        private void Button_Click_Modify(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            ModifySave winModif = new ModifySave((Backup)btn.DataContext, ListView.Items.IndexOf((Backup)btn.DataContext));
            winModif.Show();
            winModif.MyEvent += ModifyList;
        }

        private void ModifyList(object sender, RoutedEventArgs e)
        {
            IndexAndBackup indexAndBackup = (IndexAndBackup)sender;
            if (indexAndBackup.index == -1)
            {
                //ListView.Items.Add(indexAndBackup.backup);
                ListView.Items.Add(indexAndBackup.backup);
            }
            else
            {
                //backups[indexAndBackup.index] = indexAndBackup.backup;
                ListView.Items.Remove(indexAndBackup.backup);
                ListView.Items.Insert(indexAndBackup.index, indexAndBackup.backup);
            }
            IList<Backup> backups = new List<Backup>();
            foreach (var item in ListView.Items)
            {
                backups.Add((Backup)item);
            }
            ListView.Items.Refresh();
            Tools.WriteData(Tools.ObjectToJson(backups), path + @"InMemorySave.json");
        }

        private void Button_Click_MonoSave(object sender, RoutedEventArgs e)
        {
            Process[] name = Process.GetProcessesByName(business_soft);
            if (name.Length != 0)
            {
                MessageBoxResult messageBox = MessageBox.Show(Properties.Resources.processRunning, Properties.Resources.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //Console.WriteLine("No Process found");
                var item = (sender as FrameworkElement).DataContext;
                int index = ListView.Items.IndexOf(item);
                SaveClass.StartMonoSave((Backup)ListView.Items[index]);
                ListView.Items.Refresh();
                IList<Backup> backups = new List<Backup>();
                foreach (var rouge in ListView.Items)
                {
                    backups.Add((Backup)rouge);
                }
                ListView.Items.Refresh();
                Tools.WriteData(Tools.ObjectToJson(backups), path + @"InMemorySave.json");
            }

        }

        private void Button_Click_SequentialSave(object sender, RoutedEventArgs e)
        {
            Process[] name = Process.GetProcessesByName(business_soft);
            if (name.Length != 0)
            {
                MessageBoxResult messageBox = MessageBox.Show(Properties.Resources.processRunning, Properties.Resources.error, MessageBoxButton.OK, MessageBoxImage.Error);
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
                IList<Backup> backups2 = new List<Backup>();
                foreach (var item in ListView.Items)
                {
                    backups2.Add((Backup)item);
                }
                ListView.Items.Refresh();
                Tools.WriteData(Tools.ObjectToJson(backups2), path + @"InMemorySave.json");
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
