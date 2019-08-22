using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
/*
 Program written to create a RAR file of size that you input. 
 Usage: GenerateRarFile -f {PathToFilename} -s {filesizeInMB}
 Example: GenerateRarFile -f C:\temp\MFer.rar -s 100
 */

namespace GenerateRarFile
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> arguments = ArgParser.Parse(args);
            string filePath = null;
            int size = 0;
            
            //parsing cmdline args
            foreach (KeyValuePair<string, string> kp in arguments)
            {
                //Console.WriteLine("Key = {0} , Value = {1}", kp.Key, kp.Value);
                if (kp.Key == "f") { filePath = kp.Value; }
                if (kp.Key == "s") { size = Convert.ToInt32(kp.Value); }
            }

            //making sure the file extension is OK
            string e = Path.GetExtension(filePath);
            if (e != ".rar")
            {
                filePath = Path.ChangeExtension(filePath, ".rar");
            }

            //Doing the real work
            CreateByteFile(size, filePath);
            ChangeBytesToRAR(filePath);
        }

        private static void CreateByteFile(int size, string filename)
        {
            
            FileStream F = new FileStream(filename, FileMode.OpenOrCreate,
            FileAccess.ReadWrite);
            //Console.WriteLine(CalculateFileSize(size).ToString());

            for (int i = 1; i <= Convert.ToInt32(CalculateFileSize(size)); i++)
            {
                F.WriteByte((byte)i);
            }
            F.Close();
            Console.WriteLine("File written: {0}", filename);
            Console.WriteLine("File size: {0}MB", size.ToString());
        }

        public static double CalculateFileSize(int size)
        {
            //Console.WriteLine("File size will be " + size.ToString() + "MB");
            double bytesize = size*(1048576);
            return bytesize;
        }

        private static void ChangeBytesToRAR(string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] header = { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x00 };
                stream.Position = 0;
                stream.Write(header, 0, header.Length);
                stream.Close();
            }
        }

    }
}
