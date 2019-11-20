using EasySaveConsole.Model;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.View
{
    public class Menu
    {
        public void Test()
        {
            Console.WriteLine("Test");
            Tools.WriteData("Coucou", @"C:\Users\hautb\Desktop\CCTL\Annuaire\Marboulin");
            
        }
        public void SizeBytes()
        {
            DirectoryInfo sizeOctet = new DirectoryInfo("c:\\Users\\hautb\\Desktop\\CCTL\\Annuaire");
            FileInfo[] bytes = sizeOctet.GetFiles();
            Console.WriteLine(sizeOctet.Name);
            foreach (FileInfo f in bytes)
                Console.WriteLine("The size of {0} is {1} bytes.", f.Name, f.Length);
        }
    }
}
