using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace PolygonConsole
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var report = new Report();

            report.Text1 = "987";
            report.Text2 = "Здравствуй Мир!!!";

            report.CreatePackage("doc.docx");



            //Console.ReadLine();
        }
    }
}
