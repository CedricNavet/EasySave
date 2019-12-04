using System;
using System.IO;

namespace CryptoSoft
{
    class Program
    {
        public static TimeSpan InterceptArgsEncrypt(string[] args)
        {
            string key = "xorencrypt";
            if (!File.Exists(args[2]))
                return new TimeSpan(-1);
            Console.WriteLine(args[0]);
            Console.WriteLine(args[1]);
            Console.WriteLine(args[2]);
            Console.WriteLine(args[0].Replace(args[0], args[1]));

            File.Copy(args[2], args[2].Replace(args[0], args[1]), true);
            DateTime StartSave = DateTime.Now;
            string lo = ReadData(args[2].Replace(args[0], args[1]));
            char[] output = new char[lo.Length];

            for (int i = 0; i < lo.Length; i++)
            {
                output[i] = (char)(lo[i] ^ key[i % key.Length]);
            }
            var temp = "";
            foreach (char item in output)
            {
                temp += item;
            }
            WriteData(temp, args[2].Replace(args[0], args[1]));
            DateTime EndSave = DateTime.Now;
            return EndSave - StartSave;
        }

        public static string ReadData(string filepath)
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filepath);
                string temp = reader.ReadToEnd();
                reader.Close();
                return temp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void WriteData(string jsonString, string filepath)
        {
            TextWriter writer;

            try
            {
                writer = new StreamWriter(filepath);
                writer.Write(jsonString);
                writer.Close();
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        static int Main(string[] args)
        {
            if (args.Length > 0)
            {
                var temp = InterceptArgsEncrypt(args);
                return (int)temp.TotalMilliseconds;
            }
            return -1;
        }
    }
}
