using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyConsole.Model
{
    class AnonymousServer
    {
        
        public static void SendToClient()
        {
            Process pipeClient = new Process();
            pipeClient.StartInfo.FileName = @"Mettre le futur chemin ici ";  //Revoir pour le chemin de CryptoSoft
            Process[] processes = Process.GetProcessesByName("CryptoSoft");
            if(processes.Length > 0)
            {

            }
            else
            {
                using (AnonymousPipeServerStream pipeServer = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable))
                {

                    pipeClient.StartInfo.Arguments = pipeServer.GetClientHandleAsString();
                    pipeClient.StartInfo.UseShellExecute = false;
                    pipeClient.Start();

                    pipeServer.DisposeLocalCopyOfClientHandle();

                    try
                    {
                        using (StreamWriter sw = new StreamWriter(pipeServer))
                        {
                            sw.AutoFlush = true;
                            sw.WriteLine("SYNC");
                            Console.WriteLine("[SERVER] Enter text :");
                            sw.WriteLine(Console.ReadLine());
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("[SERVER] Error : {0}", e.Message);
                    }
                    pipeClient.WaitForExit();
                }
            }

        }
        
        
    }
}
