using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.Model
{
    public class BackGroundSave
    {
        private List<Backups> backups = new List<Backups>();
        private string jsonSave;
        private string path;
        public BackGroundSave()
        {
        }

        public void StartSave(string jsonSave, string path)
        {
            this.jsonSave = jsonSave;
            this.path = path;
            backups = Tools.JsonToObject<Backups>(jsonSave);
            SaveCheck();
        }

        private void SaveCheck()
        {
            
            while (true)
            {
                if (false)
                {

                }//Check if a new save is add

                foreach (Backups backup in backups)
                {
                    if (Tools.SequentialBackup(DateTime.Now, backup.TimeToSave))
                    {
                        DateTime startsave = DateTime.Now;
                        if(backup.BackupType == BackupType.mirror)
                        {
                            Tools.backUp(backup, path);
                        }
                        else
                        {

                        }

                        TimeSpan temp = DateTime.Now - startsave;
                        Logs log = new Logs()
                        {
                            Timestamp = DateTime.Now,
                            TaskName = backup.BackupsName,
                            SourceFileAddress = backup.Source,
                            DestinationFileAddress = backup.Target,
                            FileSize = 0, TransferTime = temp
                        };
                    }
                }


            }
            
        }
    }
}
