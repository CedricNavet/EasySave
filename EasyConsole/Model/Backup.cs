using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.Model
{
    public enum BackupType { mirror, differential };
    public class Backup
    {
        public String BackupName { get; set; } // Nom de sauvegarde
        public String Source { get; set; }// Source
        public String Target { get; set; } // Destination
        public BackupType BackupType { get; set; } // Type de sauvegarde
        public DateTime LastBackupCompletion { get; set; }//Heure de la sauvegarde
    }
}
