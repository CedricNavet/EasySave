using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSoft
{
    class Encryptage
    {
        public static string encryptDecrypt(string input, string key)
        {
            if (!File.Exists(input))
                return null;

            string lo = ReadData(input);
            char[] output = new char[lo.Length];

            for (int i = 0; i < lo.Length; i++)
            {
                output[i] = (char)(lo[i] ^ key[i % key.Length]);
            }
            string temp = "";
            foreach (char item in output)
            {
                temp += item;
            }
            WriteData(temp, input);
            return temp;
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
    }
}
