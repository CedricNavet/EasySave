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

        public BackGroundSave()
        {
        }

        public void StartSave(string jsonSave)
        {
            this.jsonSave = jsonSave;
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
                        if(backup.BackupType == BackupType.mirror)
                        {
                            Tools.backUp(backup);
                        }
                        else
                        {

                        }
                    }
                }


            }
            
        }
    }
}
