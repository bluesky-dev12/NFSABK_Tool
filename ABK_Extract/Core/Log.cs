using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABK_Extract.Core
{
    public class Log
    {
        private static void WriteTime()
        {
            Console.Write("[{0}] ", DateTime.Now.ToLongTimeString());
        }

        private static void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format.Replace("\n", "\n\t"), args);

            Console.ResetColor();

            Debug.WriteLine("[{0}] {1}", DateTime.Now.ToLongTimeString(), string.Format(format.Replace("\n", "\n\t\t"), args));
        }

        public static void WriteError(string format, params object[] args)
        {
            WriteTime();

            Console.ForegroundColor = ConsoleColor.Red;

            WriteLine(format, args);
        }

        public static void WriteSuccess(string format, params object[] args)
        {
            WriteTime();

            Console.ForegroundColor = ConsoleColor.Green;

            WriteLine(format, args);
        }
        public static void WriteInfo(string format, params object[] args)
        {
            WriteTime();

            WriteLine(format, args);
        }
        public static void WriteData(string format, params object[] args)
        {
            WriteTime();

            Console.ForegroundColor = ConsoleColor.Blue;

            WriteLine(format, args);
        }
    }
}
