using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.Model
{
    public class Backups
    {
        public String backupsName { get; set; } // Nom de sauvegarde
        public String source { get; set; }// Source
        public String target { get; set; } // Destination
        public enum backupType {mirror, differential }; // Type de sauvegarde
    }
}
