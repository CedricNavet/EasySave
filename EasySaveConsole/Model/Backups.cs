using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.Model
{
    public class Backups
    {
        public String backupsName; // Nom de sauvegarde
        public String source; // Source
        public String target; // Destination
        enum backupType {mirror, differential }; // Type de sauvegarde
    }
}
