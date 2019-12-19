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
        public string getText()
        {
            return text;
        }
    }
    class Question : Link
    {

        List<String> answers;
        List<Link> links;
        Dictionary<String, int> map;
        public Question()
        {
            isQuest = true;
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
    }
    class Answer : Link
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
