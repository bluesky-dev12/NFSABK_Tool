using System;
using System.IO;
using System.Text;
using ABK_Extract;

public class ABK
{
    public static void Main(string[] args)
    {
        Console.Title = "ABK Tool 1.0";
        Console.WriteLine("===============================================================================");
        Console.WriteLine("|                        ABK_Decoder_Encoder_Tool                             |");
        Console.WriteLine("|                      Inspired on Crabzette&XanTool                          |");
        Console.WriteLine("===============================================================================");

        if (args.Length != 2)
        {
            Console.WriteLine("ERROR: Too few arguments.");
            Console.WriteLine("USAGE: ABKExtractor.exe -Decompile FILE.ABK");
            return;
        }
        if (args[0] == "-Decompile")
        {
            Extract.ExtractBNKFromABK(args[1]);
        }
        if (args[0] == "-DecompileHeader")
        {
            ABK_Header.ReadHeader(args[1]);
        }
    }
}