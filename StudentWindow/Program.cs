using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MorkovkaAPI
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //TestResultParser resultParser = new TestResultParser("C:/Users/Настя/Documents/HEADER.res");
            //resultParser.ParseHeader();
            //resultParser.Parse();
            //TestResultWriter testResultWriter = new TestResultWriter(resultParser.getTestResult());
            //testResultWriter.Save("C:/Users/Настя/Documents/HEADER.0.2.res");
            //TestResultParser resultParser1 = new TestResultParser("C:/Users/Настя/Documents/HEADER.0.2.res");
            //resultParser1.ParseHeader();
            //resultParser1.Parse();


        }
    }
}
