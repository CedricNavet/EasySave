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
        private string lang;
        private string type;

        public SaveCreation(string pathJson, string lang)
        {
            this.lang = lang;
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
            switch(lang)
            {
                case "EN":
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

                    Console.WriteLine("Choose your type of save (differential ou mirror) :");
                    var list = new string[] { "mirror", "differential" };
                    type = Console.ReadLine();

                    while (!list.Contains(type))
                    {
                        Console.WriteLine("Choose your type of save (differential ou mirror) :");
                        type = Console.ReadLine();
                    }
                    break;
                case "FR":
                    Console.WriteLine("Choissiez le nom de votre travail de sauvegarde :");
                    save.BackupsName = Console.ReadLine();
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Choissisez le dossier Source :");
                        save.Source = Console.ReadLine();
                        Console.WriteLine("Choissisez le dossier de Destination :");
                        save.Target = Console.ReadLine();
                    } while (!(Directory.Exists(save.Source) && Directory.Exists(save.Target)));

                    Console.WriteLine("Choissiez votre type de sauvegarde (differential ou mirror) :");
                    var list2 = new string[] { "mirror", "differential" };
                    type = Console.ReadLine();

                    while (!list2.Contains(type))
                    {
                        Console.WriteLine("Choissiez votre type de sauvegarde (differential ou mirror) :");
                        type = Console.ReadLine();
                    }
                    break;
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