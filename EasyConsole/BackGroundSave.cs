using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Threading;
using EasySave.Model;
using System.Diagnostics;

namespace EasySave
{
    public class BackGroundSave : IDisposable
    {
        private static Mutex mutex = new Mutex();
        private string path;
        private string fileExtension;
        private string business_soft;
        public enum SaveType
        {
            sequential,
            unique
        }

        public BackGroundSave(string path, string fileExtension, string business_soft)
        {
            this.fileExtension = fileExtension;
            this.path = path;
            this.business_soft = business_soft;
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
            backup.LastBackupCompletion = DateTime.Now;
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
                backup.LastBackupCompletion = DateTime.Now;
                Process[] name = Process.GetProcessesByName(business_soft);
                if (name.Length != 0)
                    break;

                Console.WriteLine("Save named : " + backup.BackupName + " .....Done");
            }
            Console.WriteLine("All Done");
            //Thread.Sleep(1000);
        }

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
                        ThreadPool.QueueUserWorkItem(StartSaveFileDifferential, temp);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void StartSaveFileDifferential(object mydata)
        {

            SaveArgs saveArgs = (SaveArgs)mydata;
            using (MD5 md5Hash = MD5.Create())
            {
                bool IsTxt = false;
                int encrypTime = 0;
                if (!File.Exists(saveArgs.oldPath.Replace(saveArgs.backup.Source, saveArgs.backup.Target)))
                {
                    TimeSpan timeSpan = DateTime.Now - DateTime.Now;
                    if (Path.GetExtension(saveArgs.oldPath) == fileExtension)
                    {
                        IsTxt = true;
                        encrypTime = SendArgs(saveArgs.backup, saveArgs.oldPath);
                    }
                    else
                    {
                        DateTime startsave = DateTime.Now;
                        File.Copy(saveArgs.oldPath, saveArgs.oldPath.Replace(saveArgs.backup.Source, saveArgs.backup.Target), true);
                        DateTime stopsave = DateTime.Now;
                        timeSpan = stopsave - startsave;
                    }


                    Console.WriteLine("{0} thread wait", Thread.CurrentThread.ManagedThreadId);
                    mutex.WaitOne();
                    Console.WriteLine("{0} thread proceed", Thread.CurrentThread.ManagedThreadId);
                    if (IsTxt)
                        WriteLogs(saveArgs.backup, timeSpan, saveArgs.oldPath, encrypTime);
                    else
                        WriteLogs(saveArgs.backup, timeSpan, saveArgs.oldPath);

                    SaveProgression(Directory.GetFiles(saveArgs.backup.Source, "*", SearchOption.AllDirectories), saveArgs.oldPath, saveArgs.backup);
                    mutex.ReleaseMutex();
                    Console.WriteLine("{0} thread release", Thread.CurrentThread.ManagedThreadId);

                }
                else
                {
                    string hash = GetMd5Hash(md5Hash, Tools.ReadData(saveArgs.oldPath.Replace(saveArgs.backup.Source, saveArgs.backup.Target)));

                    if (!VerifyMd5Hash(md5Hash, saveArgs.oldPath, hash))
                    {
                        TimeSpan timeSpan = DateTime.Now - DateTime.Now;
                        if (Path.GetExtension(saveArgs.oldPath) == fileExtension)
                        {
                            IsTxt = true;
                            encrypTime = SendArgs(saveArgs.backup, saveArgs.oldPath);
                        }
                        else
                        {
                            DateTime startsave = DateTime.Now;
                            File.Copy(saveArgs.oldPath, saveArgs.oldPath.Replace(saveArgs.backup.Source, saveArgs.backup.Target), true);
                            DateTime stopsave = DateTime.Now;
                            timeSpan = stopsave - startsave;
                        }

                        Console.WriteLine("{0} thread wait", Thread.CurrentThread.ManagedThreadId);
                        mutex.WaitOne();
                        Console.WriteLine("{0} thread proceed", Thread.CurrentThread.ManagedThreadId);
                        if (IsTxt)
                            WriteLogs(saveArgs.backup, timeSpan, saveArgs.oldPath, encrypTime);
                        else
                            WriteLogs(saveArgs.backup, timeSpan, saveArgs.oldPath);

                        SaveProgression(Directory.GetFiles(saveArgs.backup.Source, "*", SearchOption.AllDirectories), saveArgs.oldPath, saveArgs.backup);
                        mutex.ReleaseMutex();
                        Console.WriteLine("{0} thread release", Thread.CurrentThread.ManagedThreadId);
                    }
                }
            }

        }

        private void BackUpMirror(Backup backup, string pathJson)
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
                        ThreadPool.QueueUserWorkItem(StartSaveFileMirror, temp);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private void StartSaveFileMirror(object state)
        {
            bool IsTxt = false;
            int encrypTime = 0;
            SaveArgs saveArgs = (SaveArgs)state;
            TimeSpan timeSpan = DateTime.Now - DateTime.Now;
            if (Path.GetExtension(saveArgs.oldPath) == ".txt")
            {
                IsTxt = true;
                //Console.WriteLine(saveArgs.oldPath);
                encrypTime = SendArgs(saveArgs.backup, saveArgs.oldPath);
                //Console.WriteLine("Retour " + temp);
            }
            else
            {
                DateTime startsave = DateTime.Now;
                File.Copy(saveArgs.oldPath, saveArgs.oldPath.Replace(saveArgs.backup.Source, saveArgs.backup.Target), true);
                DateTime stopsave = DateTime.Now;
                timeSpan = stopsave - startsave;
            }

            Console.WriteLine("{0} thread wait", Thread.CurrentThread.ManagedThreadId);
            mutex.WaitOne();
            Console.WriteLine("{0} thread proceed", Thread.CurrentThread.ManagedThreadId);
            if (IsTxt)
                WriteLogs(saveArgs.backup, timeSpan, saveArgs.oldPath, encrypTime);
            else
                WriteLogs(saveArgs.backup, timeSpan, saveArgs.oldPath);

            SaveProgression(Directory.GetFiles(saveArgs.backup.Source, "*", SearchOption.AllDirectories), saveArgs.oldPath, saveArgs.backup);
            mutex.ReleaseMutex();
            Console.WriteLine("{0} thread release", Thread.CurrentThread.ManagedThreadId);
        }

        #region MD5
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

        #endregion

        private void SaveProgression(string[] directoryFile, string currentFile, Backup backup)
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
                    if (Isfind)
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
                backup = backup,
            };
            Tools.WriteData(Tools.ObjectToJson(saveProgress), path + @"\SaveProgression.json");
        }

        private void WriteLogs(Backup backup, TimeSpan timeSpan, string file, int encrypTime = 0)
        {
            Logs log = new Logs()
            {
                Timestamp = DateTime.Now,
                TaskName = backup.BackupName,
                SourceFileAddress = file,
                DestinationFileAddress = file.Replace(backup.Source, backup.Target),
                FileSize = Tools.FileSize(file),
                TransferTime = timeSpan.TotalMilliseconds,
                Encryptiontime = encrypTime
            };
            string json = Tools.ReadData(path + @"\Logs.json");
            IList<Logs> tempList = Tools.JsonToObject<Logs>(json);
            tempList.Add(log);
            Tools.WriteData(Tools.ObjectToJson(tempList), path + @"\Logs.json");
        }

        public int SendArgs(Backup backup, string oldPath)
        {
            string pathSource = backup.Source;
            string pathDestination = backup.Target;
            string pathFileSource = oldPath;
            Process p = new Process();
            string destination = oldPath.Replace(backup.Source, backup.Target);
            if (backup.BackupType == BackupType.differential)
            {
                if (File.Exists(destination))
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        string hash = GetMd5Hash(md5Hash, CryptCheck(oldPath));
                        if (VerifyMd5Hash(md5Hash, destination, hash))
                            return 0;
                    }
                }
            }


            p.StartInfo.FileName = @"C:\Users\ccdu2\OneDrive - Association Cesi Viacesi mail\Mes Devoirs\Autres\C#\EasySave\CryptoSoft\bin\Release\netcoreapp2.1\win-x64\CryptoSoft.exe";
            p.StartInfo.Arguments = "\"" + oldPath + "\"  \"" + destination + "\"";
            p.Start();
            p.WaitForExit();
            int result = p.ExitCode;
            return result;
        }


        private string CryptCheck(string destination)
        {
            string key = "xorencrypt";
            string lo = Tools.ReadData(destination);
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
            return temp;
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
