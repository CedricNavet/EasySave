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
            if (args.Length > 0)
            {
                using (PipeStream pipeClient = new AnonymousPipeClientStream(PipeDirection.In, args[0]))
                {
                    Console.WriteLine("[CLIENT] Current TransmissionMode: {0}.", pipeClient.TransmissionMode);
                    using (StreamReader sr = new StreamReader(pipeClient))
                    {
                        string temp;
                        string key = "xorkey";
                        do
                        {
                            Console.WriteLine("[CLIENT] Wait for sync...");
                            temp = sr.ReadLine();
                        }
                        while (!temp.StartsWith("SYNC"));

                        while ((temp = sr.ReadLine()) != null)
                        {
                            string encrypted = Encryptage.encryptDecrypt(temp, key);
                            string decrypted = Encryptage.encryptDecrypt(temp, key);

                        }
                    }
                }
            }
            else
            {
                Console.Write("[CLIENT] Press Enter to continue...");
            }
        }
    }
}
