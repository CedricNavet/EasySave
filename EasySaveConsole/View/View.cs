using Projet_EasySave.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_EasySave.View
{
    public class View
    {
        private Model.Model model;

        public View(Model.Model model)
        {
            this.model = model;
        }

        public void Menu()
        {
            //List<Model.Travail> temp = .Request("Select * from travail");
            Travail travail = new Travail();
            travail.Id = 2;
            travail.Name = "t";
            travail.SaveType = "complete";
            travail.TimeStamping = new DateTime(2019,02,22,00,00,00);
            travail.TaskName = "n";
            travail.SourceFile = "s";
            travail.DestinationFile = "d";
            travail.FileSize = 1;
            travail.TransferTime = 1;
            model.AddTravail(travail);
            List<Travail> temp = (List<Travail>)model.GetAllTravail();
            Console.WriteLine(temp.Count);
            Console.WriteLine("Hello");
            Console.ReadLine();
        }
    }
}
