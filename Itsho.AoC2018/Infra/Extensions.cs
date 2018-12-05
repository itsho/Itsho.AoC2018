using System;
using System.Diagnostics;

namespace Itsho.AoC2018.Infra
{
    public static class Extensions
    {
        public static void ConsoleWriteLineTimed(string title, Func<string> actionToRun)
        {
            var sw = new Stopwatch();
            sw.Start();
            var strResult = actionToRun();
            sw.Stop();
            Console.WriteLine(title + "\t" + strResult + "\t (" + sw.Elapsed.ToString() + ")");
        }
    }
}