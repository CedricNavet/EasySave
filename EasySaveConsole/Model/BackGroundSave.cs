using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EasySaveConsole.Model
{
    public class BackGroundSave
    {
        private IList<Backups> backups = new List<Backups>();
        //private string jsonSave;
        private string path;
        public enum SaveType
        {
            sequential,
            unique
        }
        public BackGroundSave()
        {
        }

        public void StartSave(string path, SaveType saveType, string SaveName = null)
        {
            //this.jsonSave = jsonSave;
            this.path = path.Replace("InMemorySave.json", "");
            SaveCheck(saveType, SaveName);
        }

        private void SaveCheck(SaveType saveType, string SaveName)
        {
            backups = Tools.JsonToObject<Backups>(Tools.ReadData(this.path + @"InMemorySave.json"));
            if(saveType == SaveType.sequential)
            {
                foreach (Backups backup in backups)
                {
                    if (backup.BackupType == BackupType.mirror)
                    {
                        BackUp(backup, path);
                    }
                    else if (backup.BackupType == BackupType.differential)
                    {
                        DifferentialBackUp(backup, path);
                    }
                }
            }
            else if(saveType == SaveType.unique)
            {
                Backups backup = null;
                foreach (Backups item in backups)
                {
                    if(item.BackupsName == SaveName)
                    {
                        backup = item;
                        break;
                    }
                }

                if (backup.BackupType == BackupType.mirror)
                {
                    BackUp(backup, path);
                }
                else if (backup.BackupType == BackupType.differential)
                {
                    DifferentialBackUp(backup, path);
                }
            }
            

        }

        private void DifferentialBackUp(Backups backups, string pathJson)
        {
            try
            {
                if (Directory.Exists(backups.Source) && Directory.Exists(backups.Target))
                {
                    foreach (string dirPath in Directory.GetDirectories(backups.Source, "*", SearchOption.AllDirectories))
                        Directory.CreateDirectory(dirPath.Replace(backups.Source, backups.Target));

                    foreach (string oldPath in Directory.GetFiles(backups.Source, "*.*", SearchOption.AllDirectories))
                    {
                        using (MD5 md5Hash = MD5.Create())
                        {
                            string hash = GetMd5Hash(md5Hash, Tools.ReadData(oldPath.Replace(backups.Source, backups.Target)));

                            if (VerifyMd5Hash(md5Hash, oldPath, hash))
                            {
                                continue;
                            }
                            else
                            {
                                //Console.WriteLine("The hashes are not same.");
                                DateTime startsave = DateTime.Now;
                                SaveProgression(Directory.GetFiles(backups.Source, "*", SearchOption.AllDirectories), pathJson, oldPath, backups);
                                File.Copy(oldPath, oldPath.Replace(backups.Source, backups.Target), true);
                                WriteLogs(backups, pathJson, startsave, oldPath);
                            }
                        }


                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, Tools.ReadData(input));

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void BackUp(Backups backups, string pathJson)
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
                    foreach (string oldPath in Directory.GetFiles(backups.Source, "*.*", SearchOption.AllDirectories))
                    {
                        DateTime startsave = DateTime.Now;
                        SaveProgression(Directory.GetFiles(backups.Source, "*", SearchOption.AllDirectories), pathJson, oldPath, backups);
                        File.Copy(oldPath, oldPath.Replace(backups.Source, backups.Target), true);
                        WriteLogs(backups, pathJson, startsave, oldPath);

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
                Console.Write(e.Message);
            }
        }

        private static void SaveProgression(string[] directoryFile, string path, string currentFile, Backups backups)
        {
            long TotalFileSize = 0;
            int numberRemainFiles = 0;
            long RemainFileSize = 0;
            foreach (string item in directoryFile)
            {
                if (item == currentFile)
                {
                    numberRemainFiles++;
                    long temp = Tools.FileSize(item);
                    RemainFileSize += temp;
                    TotalFileSize += temp;
                }
                else
                {
                    TotalFileSize += Tools.FileSize(item);
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
                backup = backups,
            };
            Tools.WriteData(Tools.ObjectToJson(saveProgress), path + @"\SaveProgession.json");
        }

        private static void WriteLogs(Backups backup, string pathJson, DateTime startSave, string file)
        {
            TimeSpan temp = DateTime.Now - startSave;
            Logs log = new Logs()
            {
                Timestamp = DateTime.Now,
                TaskName = backup.BackupsName,
                SourceFileAddress = file,
                DestinationFileAddress = file.Replace(backup.Source, backup.Target),
                FileSize = Tools.FileSize(file),
                TransferTime = temp
            };
            string json = Tools.ReadData(pathJson + @"\Logs.json");
            IList<Logs> tempList = Tools.JsonToObject<Logs>(json);
            tempList.Add(log);
            Tools.WriteData(Tools.ObjectToJson(tempList), pathJson + @"\Logs.json");
        }
    }
}
