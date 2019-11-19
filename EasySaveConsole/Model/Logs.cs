using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.Model
{
    public class Logs
    {
        public DateTime timestamp { get; set; } //horodatage
        public String nameTable { get; set; } //nom de la table
        public String sourceFileAddress { get; set; } //adresse fichier source
        public String destinationFileAddress { get; set; } //adress fichier de destination
        public byte fileSize { get; set; } //taille fichier
        public int transferTime { get; set; } //temps de transfert
    }
}
