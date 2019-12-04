using EasySave.Model;
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
            this.Close();
        }

        private void Button_Click_Browse_Source(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                Source.Text = dialog.SelectedPath;
            }
        }

        private void Button_Click_Browse_Target(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                Target.Text = dialog.SelectedPath;
            }
        }
    }

    public class IndexAndBackup
    {
        public int index { get; set; }
        public Backup backup { get; set; }
    }
}
