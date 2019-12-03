using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Threading;
using EasySave.Model;
using System.Threading;
using System.Collections;

namespace EasySave
{
    public class BackGroundSave : IDisposable
    {
        private IList<Backup> backups = new List<Backup>();
        private static Mutex mutex = new Mutex();
        private string path;
        public enum SaveType
        {
            sequential,
            unique
        }
        public BackGroundSave(string path)
        {
            this.path = path;
        }

        public void StartMonoSave(Backup backup)
        {
            if (backup.BackupType == BackupType.mirror)
            {
                BackUpMirror(backup, path);
            }
            else if (backup.BackupType == BackupType.differential)
            {
                DifferentialBackUp(backup, path);
            }
            Console.WriteLine("Save named : " + backup.BackupName + " .....Done");
            //Thread.Sleep(1000);
        }

        public void StartSequentialSaves(List<Backup> backups)
        {
            foreach (Backup backup in backups)
            {
                if (backup.BackupType == BackupType.mirror)
                {
                    BackUpMirror(backup, path);
                }
                else if (backup.BackupType == BackupType.differential)
                {
                    DifferentialBackUp(backup, path);
                }
                Console.WriteLine("Save named : " + backup.BackupName + " .....Done");
            }
            Console.WriteLine("All Done");
            //Thread.Sleep(1000);
        }
        //public void StartSave(string path, SaveType saveType, string SaveName = null)
        //{
        //    this.path = path.Replace("InMemorySave.json", "");
        //    SaveCheck(saveType, SaveName);
        //}

        /*private void SaveCheck(SaveType saveType, string SaveName)
        {
            Console.Clear();
            backups = Tools.JsonToObject<Backup>(Tools.ReadData(this.path + @"InMemorySave.json"));
            if (saveType == SaveType.sequential)
            {
                foreach (Backup backup in backups)
                {
                    if (backup.BackupType == BackupType.mirror)
                    {
                        BackUp(backup, path);
                    }
                    else if (backup.BackupType == BackupType.differential)
                    {
                        DifferentialBackUp(backup, path);
                    }
                    Console.WriteLine("Save named : " + backup.BackupName + " .....Done");
                }
                Console.WriteLine("All Done");
                Thread.Sleep(1000);
            }
            else if (saveType == SaveType.unique)
            {
                Backup backup = null;
                foreach (Backup item in backups)
                {
                    if (item.BackupName == SaveName)
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
                Console.WriteLine("Save named : " + backup.BackupName + " .....Done");
                Thread.Sleep(1000);
            }


        }*/


        private void DifferentialBackUp(Backup backup, string pathJson)
        {
            try
            {
                if (Directory.Exists(backup.Source) && Directory.Exists(backup.Target))
                {
                    foreach (string dirPath in Directory.GetDirectories(backup.Source, "*", SearchOption.AllDirectories))
                        Directory.CreateDirectory(dirPath.Replace(backup.Source, backup.Target));

                    foreach (string oldPath in Directory.GetFiles(backup.Source, "*.*", SearchOption.AllDirectories))
                    {
                        object temp = new SaveArgs() { backup = backup, oldPath = oldPath };
                        ThreadPool.QueueUserWorkItem(StartSaveFile, temp);
                        //StartOneSave(backup, oldPath);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void StartSaveFile(object mydata)
        {
            SaveArgs saveArgs = (SaveArgs)mydata;
            using (MD5 md5Hash = MD5.Create())
            {
                if (!File.Exists(saveArgs.oldPath.Replace(saveArgs.backup.Source, saveArgs.backup.Target)))
                {
                    DateTime startsave = DateTime.Now;
                    File.Copy(saveArgs.oldPath, saveArgs.oldPath.Replace(saveArgs.backup.Source, saveArgs.backup.Target), true);
                    DateTime stopsave = DateTime.Now;
                    TimeSpan timeSpan = stopsave - startsave;

                    mutex.WaitOne();
                    WriteLogs(saveArgs.backup, timeSpan, saveArgs.oldPath);
                    SaveProgression(Directory.GetFiles(saveArgs.backup.Source, "*", SearchOption.AllDirectories), saveArgs.oldPath, saveArgs.backup);
                    mutex.ReleaseMutex();
                }
                else
                {
                    string hash = GetMd5Hash(md5Hash, Tools.ReadData(saveArgs.oldPath.Replace(saveArgs.backup.Source, saveArgs.backup.Target)));

                    if (!VerifyMd5Hash(md5Hash, saveArgs.oldPath, hash))
                    {
                        DateTime startsave = DateTime.Now;
                        File.Copy(saveArgs.oldPath, saveArgs.oldPath.Replace(saveArgs.backup.Source, saveArgs.backup.Target), true);
                        DateTime stopsave = DateTime.Now;
                        TimeSpan timeSpan = stopsave - startsave;

                        mutex.WaitOne();
                        WriteLogs(saveArgs.backup, timeSpan, saveArgs.oldPath);
                        SaveProgression(Directory.GetFiles(saveArgs.backup.Source, "*", SearchOption.AllDirectories), saveArgs.oldPath, saveArgs.backup);
                        mutex.ReleaseMutex();
                    }
                }
            }

        }

        private void BackUpMirror(Backup backups, string pathJson)
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
                        File.Copy(oldPath, oldPath.Replace(backups.Source, backups.Target), true);
                        DateTime stopsave = DateTime.Now;
                        TimeSpan timeSpan = stopsave - startsave;

                        mutex.WaitOne();
                        WriteLogs(backups, timeSpan, oldPath);
                        SaveProgression(Directory.GetFiles(backups.Source, "*", SearchOption.AllDirectories), oldPath, backups);
                        mutex.ReleaseMutex();

                    }

                }

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
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

        private bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
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

        private void SaveProgression(string[] directoryFile, string currentFile, Backup backups)
        {
            long TotalFileSize = 0;
            int numberRemainFiles = 0;
            long RemainFileSize = 0;
            bool Isfind = false;
            foreach (string item in directoryFile)
            {
                if (item == currentFile || Isfind == false)
                {
                    numberRemainFiles++;
                    long temp = Tools.FileSize(item);
                    RemainFileSize += temp;
                    TotalFileSize += temp;
                }
                else
                {
                    long temp = Tools.FileSize(item);
                    TotalFileSize += temp;
                    if(Isfind)
                        RemainFileSize += temp;
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
            Tools.WriteData(Tools.ObjectToJson(saveProgress), path + @"\SaveProgression.json");
        }

        private void WriteLogs(Backup backup, TimeSpan timeSpan, string file)
        {       
            Logs log = new Logs()
            {
                Timestamp = DateTime.Now,
                TaskName = backup.BackupName,
                SourceFileAddress = file,
                DestinationFileAddress = file.Replace(backup.Source, backup.Target),
                FileSize = Tools.FileSize(file),
                TransferTime = timeSpan
            };
            string json = Tools.ReadData(path + @"\Logs.json");
            IList<Logs> tempList = Tools.JsonToObject<Logs>(json);
            tempList.Add(log);
            Tools.WriteData(Tools.ObjectToJson(tempList), path + @"\Logs.json");
        }

        private class SaveArgs
        {
            public string oldPath { get; set; }
            public Backup backup { get; set; }
        }

        #region IDisposable Support
        private bool disposedValue = false; // Pour détecter les appels redondants

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~BackGroundSave()
        // {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
