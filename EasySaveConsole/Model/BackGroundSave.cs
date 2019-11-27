using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.Model
{
    public class BackGroundSave
    {
        private IList<Backups> backups = new List<Backups>();
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
                    
                    if (backup.BackupType == BackupType.mirror)
                    {
                        Tools.backUp(backup, path);
                    }
                    else if(backup.BackupType == BackupType.differential)
                    {

                    }

                    

                }


            }

        }
    }
}
