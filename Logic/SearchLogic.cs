using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    //extention for string class that help to check if the string contain some of the value that sent to the function
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }


    public class Searching
    {

        public List<string> files = new List<string>();


        //Function that get string value and path, and searchin if the value is in the curect path in all directorys (in the path)
        public List<string> GetFiles(string value, string path = "c:/")
        {
            DirectoryInfo di = new DirectoryInfo(path);
            bool isEmpty = !files.Any();

            try
            {

                foreach (var fi in di.GetFiles("*.*", SearchOption.TopDirectoryOnly))
                {

                    bool contains = fi.Name.Contains(value, StringComparison.OrdinalIgnoreCase);
                    if (contains)
                    {
                        files.Add(fi.DirectoryName + @"\" + fi.Name);

                    }
                }
            }

            catch (UnauthorizedAccessException)
            {
                //Move the curser to show its not stuck
                Console.Write(" ");
            }
            try
            {
                foreach (var dir in Directory.GetDirectories(path))
                {
                    GetFiles(value, dir);
                }

            }
            catch (UnauthorizedAccessException)
            {
                //Move the curser to show its not stuck
                Console.Write(" ");
            }
            return files;
        }

    }
}


