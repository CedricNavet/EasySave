using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.Model
{
    public enum BackupType { mirror, differential };
    public class Backups
    {
        public String BackupsName { get; set; } // Nom de sauvegarde
        public String Source { get; set; }// Source
        public String Target { get; set; } // Destination
        public BackupType BackupType { get; set; } // Type de sauvegarde
    }
}
