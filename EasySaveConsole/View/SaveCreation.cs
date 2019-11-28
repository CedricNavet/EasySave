using EasySaveConsole.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EasySaveConsole.View
{
    public class SaveCreation
    {
        string temp;
        private string pathJson;

        public SaveCreation(string pathJson)
        {
            this.pathJson = pathJson;
            ShowMenu();
        }

        private void ShowMenu()
        {
            SequentialCreation();
        }

        private void SequentialCreation()
        {
            Backups save = new Backups();
            Console.WriteLine("Choissisez le nom de votre Save :");
            save.BackupsName = Console.ReadLine();
            do
            {
                Console.Clear();
                Console.WriteLine("Choissisez votre Dossier Source");
                save.Source = Console.ReadLine();
                Console.WriteLine("Choissisez votre Dossier de destination");
                save.Target = Console.ReadLine();
            } while (!(Directory.Exists(save.Source) && Directory.Exists(save.Target)));
            
            Console.WriteLine("Choissisez le type de sauvegarde (differential ou mirror) :");
            var list = new string[] { "mirror", "differential" };
            string type = Console.ReadLine();

            while (!list.Contains(type))
            {
                Console.WriteLine("Choissisez le type de sauvegarde (differential ou mirror) :");
                type = Console.ReadLine();
            }
            int typeEnum = 50;
            switch (type)
            {
                case "mirror":
                    typeEnum = 0;
                    break;
                case "differential":
                    typeEnum = 1;
                    break;
            }
            save.BackupType = (BackupType) typeEnum;
            save.TimeToSave = DateTime.Now;
            var jsonFile = JsonConvert.DeserializeObject<List<Backups>>(Tools.ReadData(pathJson));
            if (jsonFile == null)
            {
                temp = Tools.ObjectToJson<Backups>(save);
            }
            else
            {
                jsonFile.Add(save);
                temp = Tools.ObjectToJson<List<Backups>>(jsonFile);
            }
            
            Tools.WriteData(temp, pathJson);
        }

        
   
    }
}