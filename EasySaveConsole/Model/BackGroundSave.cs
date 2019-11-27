using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.Model
{
    public class BackGroundSave
    {
        private IList<Backups> backups = new List<Backups>();
        //private string jsonSave;
        private string path;
        public BackGroundSave()
        {
        }

        public void StartSave(string path)
        {
            //this.jsonSave = jsonSave;
            this.path = path;
            backups = Tools.JsonToObject<Backups>(Tools.ReadData(path));
            SaveCheck();
        }

        private void SaveCheck()
        {

            foreach (Backups backup in backups)
            {

                if (backup.BackupType == BackupType.mirror)
                {
                    BackUp(backup, path);
                }
                else if (backup.BackupType == BackupType.differential)
                {

                }



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
                    foreach (string newPath in Directory.GetFiles(backups.Source, "*.*", SearchOption.AllDirectories))
                    {
                        DateTime startsave = DateTime.Now;
                        SaveProgression(Directory.GetFiles(backups.Source, "*", SearchOption.AllDirectories), pathJson, newPath);
                        File.Copy(newPath, newPath.Replace(backups.Source, backups.Target), true);
                        WriteLogs(backups, pathJson, startsave);

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
            };
            StreamWriter stream = File.CreateText(path + @"\SaveProgression");
            stream.Flush();
            stream.Close();
            Tools.WriteData(Tools.ObjectToJson(saveProgress), path + @"\SaveProgression");
        }

        private static void WriteLogs(Backups backup, string pathJson, DateTime startSave)
        {
            TimeSpan temp = DateTime.Now - startSave;
            Logs log = new Logs()
            {
                Timestamp = DateTime.Now,
                TaskName = backup.BackupsName,
                SourceFileAddress = backup.Source,
                DestinationFileAddress = backup.Target,
                FileSize = 0,
                TransferTime = temp
            };
            pathJson = Tools.ReadData(@"\Logs.json");
            Tools.IsValidJson<Logs>(pathJson);
            Tools.WriteData(Tools.ObjectToJson(log), pathJson + @"\Logs.json");
        }
    }
}
