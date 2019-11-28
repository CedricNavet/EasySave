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
            Console.WriteLine("Choose the name of your Save :");
            save.BackupsName = Console.ReadLine();
            do
            {
                Console.Clear();
                Console.WriteLine("Choose the Source Directory :");
                save.Source = Console.ReadLine();
                Console.WriteLine("Choose the destination Directory : ");
                save.Target = Console.ReadLine();
            } while (!(Directory.Exists(save.Source) && Directory.Exists(save.Target)));
            
            Console.WriteLine("Choose yout type of save (differential ou mirror) :");
            var list = new string[] { "mirror", "differential" };
            string type = Console.ReadLine();

            while (!list.Contains(type))
            {
                Console.WriteLine("Choose yout type of save (differential ou mirror) :");
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