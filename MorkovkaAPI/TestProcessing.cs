using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorkovkaAPI
{
    public struct Context
    {
        public Link prevLink;
        public String answerText;
    }
    public class TestProcessing
    {
        Link currentLink, mainLink;
        Context context;
        Stack<Link> history;
        Stack<Context> contextHistory;
        MorkovkaUser curUser = new MorkovkaUser();
        Attempt curAttempt;
        TestResult testResult;
        string path;
        Boolean curUserIsTeacher;

        public TestProcessing(Link root)
        {
            history = new Stack<Link>();
            contextHistory = new Stack<Context>();
            currentLink = mainLink = root;
        }
        public void setCurUserIsTeacher(Boolean name)
        {
            curUserIsTeacher = name;
        }
        public Boolean getCurUserIsTeacher()
        {
            return curUserIsTeacher;
        }
        public List<String> getAnswers()
        {
            if (currentLink.GetType() == typeof(Question))
                return (currentLink as Question).getAnswers();
            else
                return null;
        }
        public List<Link> getLinks()
        {
            if (currentLink.GetType() == typeof(Question))
                return (currentLink as Question).getLinks();
            else
                return null;
        }
        public string getCurLinkText()
        {
            if ((curAttempt == null)  && (curUserIsTeacher == false))
            {
                curAttempt = new Attempt();
                curAttempt.setName(curUser.getName());
                curAttempt.setDate(new Date(DateTime.Now));
                curAttempt.setTimeStart(new Time(DateTime.Now));

            }
            return currentLink.getText();
        }
        public bool curLinkIsQuestion()
        {
            return currentLink.isQuestion();
        }
        public Link getCurLink()
        { 
            return currentLink;
        }
        public bool goNext(String answer)
        {
            if (!currentLink.isQuestion()) throw new Exception("Answer has not next");
            context.answerText = answer;
            context.prevLink = currentLink;
            if (curUserIsTeacher == false)
            {
                curAttempt.addTestAnswer((currentLink as Question).getAnswers().IndexOf(answer));
            }
            history.Push(currentLink);
            contextHistory.Push(context);
            currentLink = (currentLink as Question).getNext(answer);
            if ((currentLink.isQuestion()==false)  && (curUserIsTeacher == false))
            {
                curAttempt.setTimeFinish(new Time(DateTime.Now));
                testResult.addAttempt(curAttempt);
                testResult.reWrite();
            }
            return currentLink.isQuestion();
        }
        public TestResult getTestResult()
        {
            return testResult;
        }
        public Context getContext()
        {
            return context;
        }

        public bool goBack()
        {
            if (history.Count == 0) return false;
            currentLink = history.Pop();
            context = contextHistory.Pop();
            return true;
        }

        public bool canGoBack()
        {
            return (history.Count != 0);
        }

        public void renewCurrent(Link newCurrent)
        {
            if (currentLink == mainLink)
                mainLink = currentLink = newCurrent;
            currentLink = newCurrent;
        }
        public void setTestResult(TestResult _testResult)
        {
            testResult = _testResult;
        }
        public Link getMainLink()
        {
            return mainLink;
        }
        public void setUserName( string _name)
        {
            curUser.setName(_name);
        }
        public void getUserName()
        {
            curUser.getName();
        }
        public void setPath(string _path)
        {
            path = _path;
        }
        public string getPath()
        {
            return path;
        }
    }
}
