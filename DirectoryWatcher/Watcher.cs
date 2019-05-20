using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DirectoryWatcher
{
    public class Watcher
    {
        static List<string> dirList = new List<string>();
        static void Main()
        {
            //Start the file watch operaion
            Watch();
        }


        private static void Watch()
        {
            string directory = @"\\ad001.siemens.net\dfs001\File\CI_GEN\Prototypen_IT\S7-CPs\Ind-Ethernet\CC7-CloudConnect\cc7";

            using (FileSystemWatcher directoryWatcher = new FileSystemWatcher())
            {
                directoryWatcher.Path = directory;
            
                directoryWatcher.Created += watchChanged;

                directoryWatcher.EnableRaisingEvents = true;
                directoryWatcher.IncludeSubdirectories = true;

                Console.WriteLine("Press 'q' to quit. ");
                while (Console.Read() != 'q');

            }
        }

        /// <summary>
        /// If any change occurs in the source,
        /// It starts to copy the file into destDir
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destDir"></param>

        private static void copyOperation(string source, string destDir = @"\\192.168.0.251\share\paste")
        {
            Console.WriteLine("Search files in the folder...");
            string[] files = Directory.GetFiles(source, "*.upd", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                // Extract only the file name from the path.
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destDir, fileName);

                Console.WriteLine("Copying is started...");
                File.Copy(file, destFile, true);

                Console.WriteLine(file);
            }


        }

        private static void watchChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"File Path: {e.FullPath} and Change Type: {e.ChangeType}");
            dirList.Add(e.FullPath);
            Thread.Sleep(500);
            copyOperation(dirList[0]);

        }






    }
}
