using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasySaveConsole.Controller
{
    public class Controller
    {
        private Model.Model model;
        private View.View view;

        View.Menu menu = new View.Menu();

        public Controller(Model.Model model, View.View view)
        {
            this.view = view;
            this.model = model;
            menu.Test();
            menu.SizeBytes();
            view.Menu();
            
            
        }

       

    }
}
