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

        public TestProcessing(Link root)
        {
            history = new Stack<Link>();
            contextHistory = new Stack<Context>();
            currentLink = mainLink = root;
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
            history.Push(currentLink);
            contextHistory.Push(context);
            currentLink = (currentLink as Question).getNext(answer);
            return currentLink.isQuestion();
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
        public Link getMainLink()
        {
            return mainLink;
        }
    }
}
