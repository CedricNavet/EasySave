using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EasySaveConsole.Model
{
    class SaveFiles
    {

        public static void backUp(string sourcePath, string destPath)
        {
            try
            {
                /*
                 * if sourcePath & destPath are both Directories
                 * @param SourcePath : Source path of the Directory to copy
                 * Copy the entire Directory & Sub-Directory in a mirror-like manner in a new Selected Directory
                 */
                if (Directory.Exists(sourcePath) && Directory.Exists(destPath))
                {
                    foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                        Directory.CreateDirectory(dirPath.Replace(sourcePath, destPath));
                    foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                        File.Copy(newPath, newPath.Replace(sourcePath, destPath), true);
                }
                /*
                 * @param destPath : Destination of the file to be copied
                 * If the sourcePath is a File & the destPath a Directory
                 * Get the fileName of sourcePath and save it into the destPath
                 */
                else if (File.Exists(sourcePath) && Directory.Exists(destPath))
                {   
                    string[] path = sourcePath.Split(@"\".ToCharArray());
                    string file = path[path.Length - 1];
                    String destFile = Path.Combine(destPath, file);
                    File.Copy(sourcePath, sourcePath.Replace(sourcePath, destFile), true);
                }
                /*
                 * If destPath & sourcePath are Files, copy the source to the Destination
                 */
                else if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, sourcePath.Replace(sourcePath, destPath), true);

                }
                /*
                 * If destPath & sourcePath are Files & Directory, nonono bad u
                 */
                else if (Directory.Exists(sourcePath) && File.Exists(destPath))
                {
                    Console.WriteLine("Nonono bad you no directory in file  T_T'");
                }

            }
            catch (Exception e)
            {
                Console.Write("Une erreur a été levé {0}", e);
            }
        }
    }
}
