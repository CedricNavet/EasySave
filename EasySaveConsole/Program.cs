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
            EasySaveConsole.Model.Model model = new Model.Model();
            EasySaveConsole.View.View view = new View.View(model);
            EasySaveConsole.Controller.Controller controller = new Controller.Controller(model, view);
        }
    }
}
