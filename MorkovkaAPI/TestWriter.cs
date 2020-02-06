using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorkovkaAPI
{
    public class Property
    {
        public List<Tuple<string, string>> prop = new List<Tuple<string, string>>();
        public string testName;
        public void addAuthor(string author)
        {
            prop.Add(new Tuple<string, string>("Author", "" + author));
        }

        public void addMainQuestion(int num)
        {
            prop.Add(new Tuple<string, string>("Main Question Number", "" + num));
        }
    }
    public class TestWriter
    {
        Link root;
        FileStream file;
        StreamWriter fout;
        Dictionary<Link, int> linkNumbers;
        Dictionary<string, int> textNumbers;
        Property prop;
        int currentEntityNumber=1;
        string testData;

        public TestWriter(Link root, Property prop)
        {
            this.root = root;
            this.prop = prop;
        }

        void generateTestData()
        {
            linkNumbers = new Dictionary<Link, int>();
            textNumbers = new Dictionary<string, int>();
            testData = "";
            prop.addMainQuestion(currentEntityNumber);
            addEntity(root);
            testData += "END.";
            addHeader();
        }

        int addEntity(Link link)
        {
           
            int num;
            if (linkNumbers.ContainsKey(link)) 
                return linkNumbers[link];
            num = currentEntityNumber++;
            linkNumbers.Add(link, num);
            if (link.isQuestion()) writeQuestion(num, link);
            else writeAnswers(num, link);
            return num;
        }

        private void writeAnswers(int num, Link link)
        {
            string str = "" + num;
            string[] strs = link.getText().Split('\n');
            str += "|A|" + (strs.Length - 1);
            for (int i = 0; i < strs.Length - 1; i++)
            {
                str += "|" + addEntity(strs[i]);
            }
            str += "\n";
            testData += str;
        }

        private void writeQuestion(int num, Link link)
        {
            string str = ""+num;
            string[] strs = link.getText().Split('\n');
            str += "|Q|" + (strs.Length-1);
            for (int i = 0; i < strs.Length-1; i++)
            {
                str +="|" + addEntity(strs[i]);
            }
            var answlist = (link as Question).getAnswers();
            str += "|"+answlist.Count;
            for (int i = 0; i < answlist.Count; i++)
            {
                str += "|"+addEntity(answlist[i]);
            }
            var linkslist = (link as Question).getLinks();
            for (int i = 0; i < linkslist.Count; i++)
            {
                str += "|"+addEntity(linkslist[i]);
            }
            str += "\n";
            testData += str;
        }

        int addEntity(string text)
        {
            int num;
            if (textNumbers.ContainsKey(text)) return textNumbers[text];
            else
            {
                num = currentEntityNumber++;
                textNumbers.Add(text, num);
            }
            testData += ""+num+"|T|" + text + "\n";
            return num;
        }

        void addHeader()
        {
            string headerStr = "";
            headerStr += "HEADER\n";
            for (int i=0;i<prop.prop.Count;i++)
            {
                headerStr += prop.prop[i].Item1 + "|" + prop.prop[i].Item2+"\n";
            }
            headerStr += "END HEADER\n\n";
            testData = headerStr + testData;
        }

        public void Save(string path)
        {
            //file = new FileStream(path + "/" + prop.testName + ".test", FileMode.Append);
            fout = new StreamWriter(path);
            generateTestData();
            fout.Write(testData);
            fout.Close();
        }
    }
}
