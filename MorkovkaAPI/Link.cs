using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorkovkaAPI
{

    public class Link
    {
        protected string text;
        protected bool isQuest;
        public bool isQuestion()
        {
            return isQuest;
        }
        public void setText(string txt)
        {
            text = txt;
        }
        public string getText()
        {
            return text;
        }
    }
    public class Question : Link
    {

        List<String> answers;
        List<Link> links;
        Dictionary<String, int> map;
        public Question()
        {
            isQuest = true;
            answers = new List<string>();
            links = new List<Link>();
            map = new Dictionary<string, int>();
        }
        public Question(String _text, List<String> _answers, List<Link> _links)
        {
            answers = new List<String>();
            links = new List<Link>();
            map = new Dictionary<String, int>();
            int i = 0;
            foreach (var aIt in _answers)
            {
                answers.Add(aIt);
                map[aIt] = i++;
            }
            foreach (var lIt in _links) links.Add(lIt);
            text = _text;
            isQuest = true;

        }
        public Question(String _text)
        {
            answers = new List<String>();
            links = new List<Link>();
            map = new Dictionary<String, int>();
            text = _text;
            isQuest = true;
        }

        public void addAnswer(String answer, Link link)
        {
            answers.Add(answer);
            links.Add(link);
            map[answer] = map.Count;
        }

        public void replaceAnswerText(string oldText, string newText)
        {
            Link tmp = links[map[oldText]];
            map.Remove(oldText);
            answers[answers.IndexOf(oldText)] = newText;
            map[newText] = links.IndexOf(tmp);
        }
        public Link getNext(String answer)
        {
            return links[map[answer]];
        }
        public List<String> getAnswers()
        {
            return answers;
        }
        public List<Link> getLinks()
        {
            return links;
        }
        public void removeAnswer(string txt)
        {
            for (int i = 0; i < answers.Count; i++)
            {
                if (answers[i] == txt)
                {
                    answers.RemoveAt(i);
                    links.RemoveAt(i);
                    renewMap();
                }
            }
        }
        void renewMap()
        {
            map.Clear();
            for (int i = 0; i < answers.Count; i++)
            {
                map[answers[i]] = i;
            }
        }
    }

    
    public class Answer : Link
    {

        public Answer()
        {
            isQuest = false;
        }
        public Answer(String _text)
        {
            isQuest = false;
            text = _text;
        }
        
    }
}
