using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.Model
{
    public class SaveProgress
    {
        public DateTime Timestamp { get; set; } //horodatage
        public int NumberTotalFiles { get; set; } //Nb fichier totaux
        public long TotalFileSize { get; set; } //Taille fichier totaux
        public int NumberRemainFiles { get; set; } //Nb fichier restant
        public long RemainFileSize { get; set; } //Taille fichier restant
        public String CurrentFileName { get; set; } //Nom du fichier en cours
    }
}
