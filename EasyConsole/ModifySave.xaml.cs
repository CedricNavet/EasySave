using EasySave;
using EasySave.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace EasyConsole
{
    /// <summary>
    /// Logique d'interaction pour ModifySave.xaml
    /// </summary>
    public partial class ModifySave : Window
    {
        public event RoutedEventHandler MyEvent;
        private IList<Backup> backups = new List<Backup>();
        string path = @"..\SaveState\";
        Backup temp;
        private int indexPrivate = -1;

        public ModifySave(Backup backup, int index)
        {
            this.indexPrivate = index;
            this.temp = backup;
            this.DataContext = backup;
            InitializeComponent();
            //foreach (BackupType type in (BackupType[])Enum.GetValues(typeof(BackupType)))
            //{
            //    MenuSaveType.Items.Add(type);
            //}
            this.MenuSaveType.Text = backup.BackupType.ToString();
        }

        public ModifySave()
        {           
            temp = new Backup();
            this.DataContext = temp;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            temp.BackupType = (BackupType)MenuSaveType.SelectedValue;

            IndexAndBackup backup1 = new IndexAndBackup() { backup = temp, index = indexPrivate };
            MyEvent?.Invoke(backup1, null);


            //var all = new { item = temp, index = indexPrivate };

            //backups = Tools.JsonToObject<Backup>(Tools.ReadData(path + @"InMemorySave.json"));
            //foreach (Backup backup in backups)
            //{
            //    //BackupType type = backup.BackupType;
            //    if (backup == temp)
            //    {
            //        backup.BackupName = BackupName.Text;
            //        backup.Source = Source.Text;
            //        backup.Target = Target.Text;
            //        switch (MenuSaveType.Text)
            //        {
            //            case "differential":
            //                backup.BackupType = BackupType.differential;
            //                break;
            //            case "mirror":
            //                backup.BackupType = BackupType.mirror;
            //                break;
            //        }
            //    }
            //}

            this.Close();
            //Tools.WriteData(Tools.ObjectToJson(backups), path + @"InMemorySave.json");
            //MyEvent?.Invoke(this, null);

        }
    }

    public class IndexAndBackup
    {
        public int index { get; set; }
        public Backup backup { get; set; }
    }
}
