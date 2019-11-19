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
        public byte totalFileSize { get; set; } //Taille fichier totaux
        public int numberRemainFiles { get; set; } //Nb fichier restant
        public byte remainFileSize { get; set; } //Taille fichier restant
        public String currentFileName { get; set; } //Nom du fichier en cours
    }
}
