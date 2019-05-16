using System;
using System.IO;
using System.Security.Permissions;

namespace DirectoryWatcher
{
    public class Watcher
    {
        static void Main()
        {
            Run();

        }

        private static void Run()
        {
            string directory = @"\\192.168.4.95\share\4Leon";

            using (FileSystemWatcher directoryWatcher = new FileSystemWatcher())
            {
                directoryWatcher.Path = directory;

                //directoryWatcher.NotifyFilter = NotifyFilters.LastAccess
                //                              | NotifyFilters.LastWrite
                //                              | NotifyFilters.FileName
                //                              | NotifyFilters.DirectoryName;

                directoryWatcher.Filter = "*.pdf";

                directoryWatcher.Created += watchChanged;

                directoryWatcher.EnableRaisingEvents = true;
                directoryWatcher.IncludeSubdirectories = true;

                Console.WriteLine("Press 'q' to quit the sample. ");
                while (Console.Read() != 'q');

            }
        }
        private static void watchChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"File Path: {e.FullPath}");
            Console.WriteLine($"Change Type: {e.ChangeType}");
            Console.WriteLine("Copying Operation is started...");
            copyOperation(e.FullPath);

        }

        private static void copyOperation(string sourceDir, string destDir= @"\\192.168.4.95\share\paste")
        {
            try
            {
                string[] fileList = Directory.GetFiles(sourceDir, "*.pdf");

                foreach (var file in fileList)
                {


                }
            }
        }
            
    
 


        
    }
}
