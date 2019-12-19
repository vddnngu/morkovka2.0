using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morkovka
{
    class Property
    {
        public List<Tuple<string, string>> prop = new List<Tuple<string, string>>();
        public string testName;
        public void addAuthor(string author)
        {
            //TODO
        }

        public void addMainQuestion(int num)
        {
            prop.Add(new Tuple<string, string>("Main Question Number", "" + num));
        }
    }
    class TestWriter
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
            addHeader();
            addEntity(root);
            testData += "END.";
        }

        int addEntity(Link link)
        {
            int num;
            if (linkNumbers.ContainsKey(link)) return linkNumbers[link];
            else { 
                num = currentEntityNumber++;
                linkNumbers.Add(link, num);
            }
            if (link.isQuestion()) writeQuestion(num, link);
            else writeAnswers(num, link);
            return num;
        }

        private void writeAnswers(int num, Link link)
        {
            string str = "" + num;
            str += "|A|" + addEntity(link.getText());
            str += "\n";
            testData += str;
        }

        private void writeQuestion(int num, Link link)
        {
            string str = ""+num;
            str += "|Q|" + addEntity(link.getText());
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
            testData += "HEADER\n";
            for (int i=0;i<prop.prop.Count;i++)
            {
                testData += prop.prop[i].Item1 + "|" + prop.prop[i].Item2+"\n";
            }
            testData += "END HEADER\n\n";
        }

        public void Save(string path)
        {
            //file = new FileStream(path + "/" + prop.testName + ".test", FileMode.Append);
            fout = new StreamWriter(path + "/" + prop.testName + ".test");
            generateTestData();
            fout.Write(testData);
            fout.Close();
        }
    }
}
