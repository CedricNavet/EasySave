using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{
    public class MenuAction
    {
        public readonly string Name;
        public readonly ArrowPosition ArrowPosition;
        public readonly Type ClassName;

        public MenuAction(string name, ArrowPosition arrowPosition, Type ClassName = null)
        {
            this.Name = name;
            this.ArrowPosition = arrowPosition;
            this.ClassName = ClassName;
        }

        public object Instanciate()
        {
            if(ClassName == null)
            {
                return 0;
            }
            else
                return (object)Activator.CreateInstance(ClassName);
        }
    }
}
