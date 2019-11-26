using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                return JsonConvert.SerializeObject(objetToSerialize, Formatting.Indented);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public static IList<T> JsonToObject<T>(string jsonString)
        {
            if (jsonString.Length == 0)
            {
                IList<T> list = new List<T>();
                return list;
            }
            try
            {
                JToken parseJson = JToken.Parse(jsonString);
                IList<JToken> results = parseJson.Children().ToList();
                IList<T> backupData = new List<T>();

                foreach (JToken result in results)
                {
                    T backup = result.ToObject<T>();
                    backupData.Add(backup);
                }

                return backupData;
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

        public static bool IsSizeEquivalent(long s1, long s2)
        {
            return s1 == s2;
        }

        //fonction qui compare l'heure actuelle et l'heure de sauvegarde
        public static bool SequentialBackup(DateTime currentTime, DateTime saveTime)
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

        /// <summary>
        /// check JSON valid type 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static bool IsValidJson<T>(this string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject<T>(strInput);
                    return true;
                }
                catch // not valid
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void backUp(Backups backups, string pathJson)
        {
            try
            {

                /*
                 * if sourcePath & destPath are both Directories
                 * @param SourcePath : Source path of the Directory to copy
                 * Copy the entire Directory & Sub-Directory in a mirror-like manner in a new Selected Directory
                 */
                if (Directory.Exists(backups.Source) && Directory.Exists(backups.Target))
                {
                    foreach (string dirPath in Directory.GetDirectories(backups.Source, "*", SearchOption.AllDirectories))
                        Directory.CreateDirectory(dirPath.Replace(backups.Source, backups.Target));
                    foreach (string newPath in Directory.GetFiles(backups.Source, "*.*", SearchOption.AllDirectories))
                    {
                        SaveProgression(Directory.GetFiles(backups.Source, "*", SearchOption.AllDirectories), pathJson, newPath);
                        File.Copy(newPath, newPath.Replace(backups.Source, backups.Target), true);
                        
                    }
                        
                }
                /*
                 * @param destPath : Destination of the file to be copied
                 * If the sourcePath is a File & the destPath a Directory
                 * Get the fileName of sourcePath and save it into the destPath
                 */
                else if (File.Exists(backups.Source) && Directory.Exists(backups.Target))
                {
                    string[] path = backups.Source.Split(@"\".ToCharArray());
                    string file = path[path.Length - 1];
                    String destFile = Path.Combine(backups.Target, file);
                    File.Copy(backups.Source, backups.Source.Replace(backups.Source, destFile), true);
                }
                /*
                 * If destPath & sourcePath are Files, copy the source to the Destination
                 */
                else if (File.Exists(backups.Source))
                {
                    File.Copy(backups.Source, backups.Source.Replace(backups.Source, backups.Target), true);

                }
                /*
                 * If destPath & sourcePath are Files & Directory, nonono bad u
                 */
                else if (Directory.Exists(backups.Source) && File.Exists(backups.Target))
                {
                    Console.WriteLine("Nonono bad you no directory in file  T_T'");
                }

            }
            catch (Exception e)
            {
                Console.Write("Une erreur a été levé {0}", e);
            }
        }

        private static void SaveProgression(string[] directoryFile, string path, string currentFile)
        {
            long TotalFileSize = 0;
            int numberRemainFiles = 0;
            long RemainFileSize = 0;
            foreach (string item in directoryFile)
            {
                if (item == currentFile)
                {
                    numberRemainFiles++;
                    long temp = FileSize(item);
                    RemainFileSize += temp;
                    TotalFileSize += temp;
                }
                else
                {
                    TotalFileSize += FileSize(item);
                }
                
            }
              
            SaveProgress saveProgress = new SaveProgress()
            {
                Timestamp = DateTime.Now,
                NumberTotalFiles = directoryFile.Length,
                TotalFileSize = TotalFileSize,
                NumberRemainFiles = numberRemainFiles,
                RemainFileSize = RemainFileSize,
                CurrentFileName = currentFile,
            };
            
            WriteData(ObjectToJson(saveProgress),path+@"\SaveProgression" );
        }
    }
}
