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
            EasySaveConsole.View.View view = new View.View();
            EasySaveConsole.Controller.Controller controller = new Controller.Controller(view);
        }
    }
}
