using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorkovkaAPI
{
    public class Date
    {
        int day;
        int month;
        int year;
        public Date(string d)
        {
            string[] strs = d.Split('.');
            day = Convert.ToInt32(strs[0]);
            month = Convert.ToInt32(strs[1]);
            year = Convert.ToInt32(strs[2]);
        }
    }
    public class Time
    {
        int hour;
        int minute;
        int seconds;
        public Time(string t)
        {
            string[] strs = t.Split(':');
            hour = Convert.ToInt32(strs[0]);
            minute = Convert.ToInt32(strs[1]);
            seconds = Convert.ToInt32(strs[2]);
        }
        public static int operator-(Time op1, Time op2)
        {
            int h = op1.hour - op2.hour;
            int m= op1.minute - op2.minute;
            int s= op1.seconds - op2.seconds;
            return h * 3600 + m * 60 + s;
        }

    }
    public class TestResult
    {
        string name;
        Date date;
        Time start;
        Time finish;
        TestProcessing test;
        List<int> answers;
        
        public void setName(string _name)
        {
            name = _name;
        }
        public string getName()
        {
            return name;
        }
        public void setDate(Date _date)
        {
            date = _date;
        }
        public Date getDate()
        {
            return date;
        }
        public void setTimeStart(Time _start)
        {
            start = _start;
        }
        public Time getTimeStart()
        {
            return start;
        }
        public void setTimeFinish(Time _finish)
        {
            finish = _finish;
        }
        public Time getTimeFinish()
        {
            return finish;
        }
        public void setTestProcessing(TestProcessing _test)
        {
            test = _test;
        }
        public TestProcessing getTestProcessing()
        {
            return test;
        }
        public void setListAnswers(List<int> _answers)
        {
            answers = _answers;
        }
        public List<int> getListAnswers()
        {
            return answers;
        }
        public TestResult()
        {
            answers = new List<int>();
        }
    }
}
