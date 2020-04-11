using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorkovkaAPI
{
    public class TestResultWriter
    {
        string path;
        string resData;
        TestResult testResult;
        FileStream file;
        StreamWriter fout;


        public TestResultWriter(TestResult _testRes)
        {
            testResult = _testRes;
        }
        void addHeader()
        {
            string headerStr = "";
            headerStr += "HEADER\n";
            headerStr += "TestPath"+ "|"  + testResult.getTestPath() + "\n";
            headerStr += "END HEADER\n\n";
            resData += headerStr;
        }
        private void writeAttempt(Attempt at)
        {
            string str = "";
            str += at.getName() + "|"+at.getDate().ToString()+"|" + at.getTimeStart().ToString() + "|" + at.getTimeFinish().ToString() + "|";
            for (int i=0; i < at.getListAnswers().Count()-1; i++)
            {
                str += at.getListAnswers()[i] + " ";
            }
            str += at.getListAnswers()[at.getListAnswers().Count()-1];
            str += "\n";
            resData += str;
        }
        private void writeTestResults()
        {
            List<Attempt> attempts = testResult.GetAttempts();
            for (int i=0; i<attempts.Count; i++)
            {
                writeAttempt(attempts[i]);
            }
        }
        void generateResData()
        {
            resData = "";
            addHeader();
            writeTestResults();
            resData += "END";
        }
        public void Save(string path)
        {
            fout = new StreamWriter(path);
            generateResData();
            fout.Write(resData);
            fout.Close();
        }

    }
}
