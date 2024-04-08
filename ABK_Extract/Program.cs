using System;
using System.IO;
using System.Text;
using ABK_Extract;

public class ABK
{
    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("ERROR: Too few arguments.");
            Console.WriteLine("USAGE: ABKExtractor.exe FILE.ABK");
            return;
        }
        if (args[0] == "-Decompile")
        {
            Extract.ExtractBNKFromABK(args[1]);
        }
    }
}