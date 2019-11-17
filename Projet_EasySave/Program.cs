using Projet_EasySave.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_EasySave
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Projet_EasySave.Model.Model model = new Model.Model();
            Projet_EasySave.View.View view = new View.View(model);
            Projet_EasySave.Controller.Controller controller= new Controller.Controller(model, view);
            
        }
    }
}
