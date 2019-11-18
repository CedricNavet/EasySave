using EasySaveConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.View
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
            model.AddTravail(travail);
            List<Travail> l = new List<Travail>();
            l.Add(travail);
            string temp = Tools.ObjectToJson<List<Travail>>(l);
            Console.WriteLine(temp);
            List <Travail> temp2 = (List<Travail>)model.GetAllTravail();
            Console.WriteLine(temp2.Count);
            Console.WriteLine("Hello");
            Console.ReadLine();
        }
    }
}
