using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;

namespace DirectoryWatcher
{
    public class Watcher
    {
        static List<string> dirList = new List<string>();
        static void Main()
        {
            //Start file watch operaion
            Watch();
            //dirList[0] is the root level of the directory
            copyOperation(dirList[0]);



        }


        


        private static void Watch()
        {
            string directory = @"\\192.168.4.95\share\4Leon";

            using (FileSystemWatcher directoryWatcher = new FileSystemWatcher())
            {
                directoryWatcher.Path = directory;

                //directoryWatcher.NotifyFilter = NotifyFilters.LastAccess
                //                              | NotifyFilters.LastWrite
                //                              | NotifyFilters.FileName
                //                              | NotifyFilters.DirectoryName;

                //directoryWatcher.Filter = "*.pdf";

                directoryWatcher.Created += watchChanged;

                directoryWatcher.EnableRaisingEvents = true;
                directoryWatcher.IncludeSubdirectories = true;





                Console.WriteLine("Press 'q' to quit the sample. ");
                while (Console.Read() != 'q')
                    ;

            }
        }

        private static void copyOperation(string source, string destDir = @"\\192.168.4.95\share\paste")
        {
            Console.WriteLine("Search files in the folder...");
            string[] files = Directory.GetFiles(source, "*.jpg", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                // Extract only the file name from the path.
                string fileName = Path.GetFileName(file);
                string  destFile = Path.Combine(destDir, fileName);

                Console.WriteLine("Copying is started...");
                File.Copy(file, destFile, true);
            
                Console.WriteLine(file);
            }


        }

        private static void watchChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"File Path: {e.FullPath} and Change Type: {e.ChangeType}");
            dirList.Add(e.FullPath);

        }






    }
}
