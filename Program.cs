using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Activities.Statements;

namespace fileCopy
{
    class Program
    {

        #region private properties
        private static string SourcePath = @""; // input  your source path hereof you want to use this codebase to run automatically
        private static string DestinationPath = @""; //input your destination path hereof you want to use this codebase to run automatically
        private static string newFolderName;
        #endregion

        static void Main(string[] args)
        {
            getSourceDirectory();
            Console.ReadLine();
            // to use this app automatically in your development where you would have already added you source
            // path and destination part on line 15 and 16 respectively , 
            //un comment the below line 28 and comment out line 22 and 23

            //  DirectoryCopy(SourcePath, DestinationPath, true);
        }

        #region get your source path
        public static void getSourceDirectory()
        {

            Console.WriteLine("Enter Source Directory:  "); // or Console.Write("Type any number:  "); to enter number in the same line
            string sourcepath = Console.ReadLine();
            if (!Directory.Exists(sourcepath))
            {
                Console.WriteLine("Source path does not exist. Kindly input a valid source path");
                getSourceDirectory();
            }
            else
            {
                if (String.IsNullOrEmpty(sourcepath))
                {
                    Console.WriteLine(" Source path is not valid. Kindly input a valid source path");
                    getSourceDirectory();
                }
                else
                {
                    SourcePath = sourcepath;
                    Console.WriteLine(" Your source path is {" + SourcePath +"}");
                    getDestinationDirectory();
                }
            }
          
        }

        #endregion

        #region get destinition path
        public static void getDestinationDirectory()
        {

            Console.WriteLine("Enter Destination Directory:  "); // or Console.Write("Type any number:  "); to enter number in the same line
            string destinationpath = Console.ReadLine();
            if (!Directory.Exists(destinationpath))
            {
                Console.WriteLine("Destination path  does not exist. Kindly input a valid source path");
                getDestinationDirectory();
            }
            else
            {
                if (String.IsNullOrEmpty(destinationpath))
                {
                    Console.WriteLine(" Destinition path is not valid. Kindly input a valid Destinition path");
                    getDestinationDirectory();
                }
                else
                {
                    DestinationPath = destinationpath;
                    Console.WriteLine(" Your Destination path is {" + DestinationPath + "}");

                    CreateNewFolder();

                }
            }
        }
        #endregion

        #region create new folder
        public static void CreateNewFolder()
        {
            Console.WriteLine("Are you sure you aren't creating a new folder. Type n to create or y to continue without creating a new folder.  [y/n] ?");
            var response = Console.ReadKey(false).Key;

            if(response.ToString() == "Y")
            {
                Console.WriteLine("kinldy type the name of the new folder you that should be created");
                 newFolderName = Console.ReadLine();
               
                    if (String.IsNullOrEmpty(newFolderName))
                    {
                        Console.WriteLine(" folder name is empty");
                       
                    }
                    else
                    {
                    DestinationPath = DestinationPath + newFolderName;
                 
                    DirectoryCopy(SourcePath, DestinationPath, true);
                }
            }
            else
            {
                Console.WriteLine("Are you sure you aren't creating a  new folder. Type n to create or y to continue without creating a new folder    [y/n] ?");
                var res = Console.ReadKey(false).Key;
                if (res.ToString() == "N")
                {
                    Console.WriteLine("kinldy type the name of the new folder you that should be created");
                     newFolderName = Console.ReadLine();

                    if (String.IsNullOrEmpty(newFolderName))
                    {
                        Console.WriteLine("folder name is empty");

                    }
                    else
                    {
                        DestinationPath = DestinationPath + newFolderName;
                        DirectoryCopy(SourcePath, DestinationPath, true);

                    }
                }
                else
                {
                    DirectoryCopy(SourcePath, DestinationPath, true);
                }
            }
           
        }

        #endregion

        #region copying your files
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {

            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);


            if (!dir.Exists)
            {
                Console.WriteLine("Source directory does not exist or could not be found");
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found:"
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.       

            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();


            foreach (FileInfo file in files)
            {
                try
                {
                    string tempPath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(tempPath, false);
                    var report = file + " has been copied into " + tempPath + " successfully";
                    Console.WriteLine(report);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message .ToString());
                    return;
                }
             
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    try
                    {
                        string tempPath = Path.Combine(destDirName, subdir.Name);
                        DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }
                 
                }
            }
        }
      
        #endregion
    }
}
