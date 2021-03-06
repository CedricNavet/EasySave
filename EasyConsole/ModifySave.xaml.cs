using EasySave.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
        Backup Backup;
        private int indexPrivate = -1;

        public ModifySave(Backup backup, int index)
        {
            this.indexPrivate = index;
            this.Backup = backup;
            InitializeComponent();
            BackupName.Text = backup.BackupName;
            Source.Text = backup.Source;
            Target.Text = backup.Target;
            this.MenuSaveType.Text = backup.BackupType.ToString();
        }

        public ModifySave()
        {
            Backup = new Backup();
            this.DataContext = Backup;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (BackupName.Text == "" || Source.Text == "" || Target.Text == "" || MenuSaveType.SelectedItem == null)
            {
                return;
            }
            if (!Directory.Exists(Source.Text) || !Directory.Exists(Target.Text))
            {
                return;
            }
            Backup.BackupName = BackupName.Text;
            Backup.Source = Source.Text;
            Backup.Target = Target.Text;
            Backup.BackupType = (BackupType)MenuSaveType.SelectedValue;
            Backup.LastBackupCompletion = DateTime.Now;

            IndexAndBackup backup1 = new IndexAndBackup() { backup = Backup, index = indexPrivate };
            
            MyEvent?.Invoke(backup1, null);
            this.Close();

        }

        private void Button_Click_Browse_Source(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                Source.Text = dialog.SelectedPath;
                Backup.Source = Source.Text;
            }
        }

        private void Button_Click_Browse_Target(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                Target.Text = dialog.SelectedPath;
                Backup.Target = Target.Text;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }



    public class IndexAndBackup
    {
        public int index { get; set; }
        public Backup backup { get; set; }
    }
}
