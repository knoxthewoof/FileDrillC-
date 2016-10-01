using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    class Program
    {

       
    }
    class UserInp
    {
        public enum userChoice { No, Yes }

        static public userChoice GetDecisionFromUser(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);

                string answer = Console.ReadLine();
                if (answer == "y")
                {
                    return userChoice.Yes;
                }
                else if (answer == "n")
                {
                    return userChoice.No;
                }

                Console.WriteLine("---Input must be 'y' or 'n'.");
            }
        }
    }
    class searchFile
    {
        static public IEnumerable<FileInfo> GetFilesModifiedInLast24Hours(string path)
        {
            var directory = new DirectoryInfo(path);
            DateTime from_date = DateTime.Now.AddDays(-1);
            DateTime to_date = DateTime.Now;
            var files = directory.GetFiles()
              .Where(file => file.LastWriteTime >= from_date && file.LastWriteTime <= to_date);
            return files.ToList();
        }
    }
    class Programs
    {
        static void Main(string[] args)
        {
            string sourcePath = @"C:\Users\jj\Desktop\Hold";
            string targetPath = @"C:\Users\jj\Desktop\Receive";

            do
            {
                Console.WriteLine("Recent changes and additions are as follows: ");
                var modifiedFiles = searchFile.GetFilesModifiedInLast24Hours(sourcePath);
                var num = 0;

                foreach (var file in modifiedFiles)
                {
                    Console.WriteLine("\"" + file + "\"");
                    num++;
                }

                if (UserInp.GetDecisionFromUser($"{num} file(s) have recently been changed or created. Initiate transfer now? y/n") == UserInp.userChoice.Yes)
                {
                    foreach (var file in modifiedFiles)
                    {
                        File.Copy(file.FullName, Path.Combine(targetPath, file.Name), true);
                    }

                    Console.WriteLine("{0} file(s) transfered.", num);
                }
            }
            while (UserInp.GetDecisionFromUser("File(s) have been transferred, would you like to ext now? y/n") == UserInp.userChoice.No);

            Console.WriteLine("Program is now complete, Thank You!");
        }
    }
}
