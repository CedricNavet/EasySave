using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Projet_EasySave.Model.Model model = new Model.Model();
            Projet_EasySave.View.View view = new View.View(model);
            Projet_EasySave.Controller.Controller controller = new Controller.Controller(model, view);
        }
    }
}
