using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasySaveConsole.Model
{
    public static class Tools
    {
        public static string ObjectToJson<T>(T objetToSerialize)
        {
            try
            {
                return JsonConvert.SerializeObject(objetToSerialize);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public static List<T> JsonToObject<T>(string jsonString)
        {
            try
            {
                return (List<T>)JsonConvert.DeserializeObject(jsonString);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public static void WriteData(string text, string filepath)
        {
            TextWriter writer = null;
            try
            {
                writer = new StreamWriter(filepath);
                writer.Write(text);
                writer.Close();
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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

        public static long FileSize(string filename)
        {
            return new FileInfo(filename).Length;
        }

        public static bool IsSizeEquivalent(long s1,long s2)
        {
            return s1 == s2;
        }

        //fonction qui compare l'heure actuelle et l'heure de sauvegarde
        public static bool SequentialBackup (DateTime currentTime, DateTime saveTime)
        {
            return currentTime >= saveTime;
        }

        public static bool CopyFiles(string source, string destination, bool IsFolder)
        {
            if (IsFolder)
            {
                try
                {
                    string[] picList = Directory.GetFiles(source);
                    foreach (string item in picList)
                    {
                        // Remove path from the file name.
                        string fName = item.Substring(source.Length + 1);

                        // Use the Path.Combine method to safely append the file name to the path.
                        // Will overwrite if the destination file already exists.
                        File.Copy(Path.Combine(source, fName), Path.Combine(destination, fName), true);
                    }
                    return true;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong path");
                    Thread.Sleep(2000);
                    return false;
                }
                
            }
            else
            {
                try
                {
                    throw new NotImplementedException();

                    string fName = source.Substring(source.Length + 1);
                    File.Copy(Path.Combine(source, fName), Path.Combine(destination, fName), true);
                    return true;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong path");
                    Thread.Sleep(2000);
                    return false; ;
                }
            }
        }
    }
}
