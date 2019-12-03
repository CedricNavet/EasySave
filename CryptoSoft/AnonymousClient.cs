using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSoft
{
    class AnonymousClient
    {
        public static void ReceivesServer(string[] args)
        {
            Process pipeTest = new Process();
            if (args.Length > 0)
            {
                using (PipeStream pipeClient = new AnonymousPipeClientStream(PipeDirection.In, args[0]))
                {
                    using (StreamReader sr = new StreamReader(pipeClient))
                    {
                        string temp;
                        string key = "xorkey";
                        do
                        {
                            temp = sr.ReadLine();
                        }
                        while (!temp.StartsWith("SYNC"));
                        while ((temp = sr.ReadLine()) != null)
                        {
                            
                        }
                    }
                }
            }
        }
    }
}
