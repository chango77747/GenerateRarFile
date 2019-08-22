using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace GenerateRarFile
{
    class ArgParser
    {
        public static Dictionary<string, string> Parse(string[] args)
        {
            Dictionary<string, string> returnVal = new Dictionary<string, string>();
            // set defaults
            returnVal["f"] = null;                        
            returnVal["s"] = null;
            returnVal["h"] = null;
            string currentKey = "";
            foreach (string str in args)
            {
                if (str.Substring(0, 1) == "-")
                {
                    currentKey = str.Substring(1);
                    bool expectedKey = false;
                    foreach (string exp in returnVal.Keys)
                    {
                        if (string.Equals(currentKey, exp, StringComparison.OrdinalIgnoreCase))
                        {
                            expectedKey = true;
                            break;
                        }
                    }
                    if (!expectedKey)
                    {
                        ArgParser.Usage();
                    }
                    else
                    {
                        returnVal[currentKey] = "";
                    }
                }
                else
                {
                    returnVal[currentKey] = str;
                }
            }
            if (returnVal["h"] !=null ) { ArgParser.Usage(); }
            return returnVal;
        }
        public static void Usage()
        {
            Console.WriteLine("Program to create a RAR file with user defined amount of bytes.");
            Console.WriteLine("Written by: Evan MFing Pena AKA Chango77747");
            Console.WriteLine("Written because apparently Senior Managers and Global Leads still support consultants");
            Console.WriteLine("\nRequired Arguments:");
            Console.WriteLine("\t-f\t\tPath to filename you want created");
            //Console.WriteLine("\t-t\t\tType of compression:zip/rar");
            Console.WriteLine("\t-s\t\tSize of file you want in MB");
            Environment.Exit(1);
        }
    }
}