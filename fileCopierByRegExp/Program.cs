using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace fileCopierByRegExp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                System.Console.WriteLine("Please enter a numeric argument.");
                System.Console.WriteLine("example app.exe <source folder> <destination folder> <regexp value>.");
                Console.ReadKey();
                return;
            }

            if (!Directory.Exists(args[0]))
            {
                System.Console.WriteLine("Arg1. Input directory not exist.");
                Console.ReadKey();
                return;
            }

            if (!Directory.Exists(args[1]))
            {
                try { Directory.CreateDirectory(args[1]); }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Arg2. Output directory create error. " + ex.Message);
                    Console.ReadKey();
                    return;
                }
            }

            Regex regex;
            try { regex = new Regex(args[2]);}
            catch (Exception ex)
            {
                System.Console.WriteLine("Arg3. RegExp value error. " + ex.Message);
                Console.ReadKey();
                return;
            }

            string[] fileList = Directory.GetFiles(args[0]);
            System.Console.WriteLine($"Found {fileList.Length} files.");
            //System.Console.WriteLine();
            int chk = 0;
            int cpd = 0;
            foreach (string fullFileName in fileList)
            {
                chk++;
                string fileName = fullFileName.Split('\\').Last();
                if (regex.IsMatch(fileName))
                {
                    cpd++;
                    try{ File.Copy(fullFileName, args[1] + "\\" + fileName); }
                    catch (Exception ex)
                    {System.Console.WriteLine($"{fileName} file coping error. " + ex.Message);}
                    
                }
                System.Console.Write($"{chk} files checked. {cpd} files copied");
                Console.SetCursorPosition(0, Console.CursorTop);
            }
            Console.ReadKey();
        }
    }
}
