using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeBot
{
    public static class Writer
    {
        public static void WriteErrorln(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public static void WriteWarningln(string value)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public static void WriteSuccessln(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public static void WriteError(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(value);
            Console.ResetColor();
        }
        public static void WriteWarning(string value)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(value);
            Console.ResetColor();
        }
        public static void WriteSuccess(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(value);
            Console.ResetColor();
        }
        public static void WriteIgnoredln(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public static void WriteIgnored(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(value);
            Console.ResetColor();
        }
        public static void WriteGetChat(string value)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(value);
            Console.ResetColor();
        }
        public static void WriteGetChatln(string value)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(value);
            Console.ResetColor();
        }
    }
}
