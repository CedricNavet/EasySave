using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.Model
{
    public class Logs
    {
        public DateTime Timestamp { get; set; } //horodatage
        public String TaskName { get; set; } //nom de la tache
        public String SourceFileAddress { get; set; } //adresse fichier source
        public String DestinationFileAddress { get; set; } //adress fichier de destination
        public long FileSize { get; set; } //taille fichier
        public double TransferTime { get; set; } //temps de transfert
    }
}
