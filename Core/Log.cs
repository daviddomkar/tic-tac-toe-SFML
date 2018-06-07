using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piskvorky.Core
{
    class Log
    {
        private static string TAG = "";

        public static void SetTag(string tag)
        {
            TAG = tag;
        }

        public static void Debug(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[" + DateTime.Now + "]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" [DEBUG] " + TAG + " : " + message);
        }
    }
}
