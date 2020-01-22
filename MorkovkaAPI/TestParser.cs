using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorkovkaAPI
{
    public enum typeEntity
    {
        text,
        question,
        answer
    }
    public class RecEntity
    {
        public int num;
        public typeEntity type;
        
    }

    public class QuestEntity :  RecEntity
    {
        public List<int> numbersTextLines = new List<int>();
        public List<int> numbersAnswersTexts = new List<int>();
        public List<int> numbersAnswers = new List<int>();

    }

    public class AnswerEntity : RecEntity
    {
        public int numText;
    }

    public class TextEntity : RecEntity
    {
        public string text;
    }
    public class TestParser
    {
        string path;
        FileStream file;
        StreamReader fin;
        Dictionary<int, RecEntity> entityRecords = new Dictionary<int, RecEntity>();
        TestCreater creator;
        public TestParser(string _path)
        {
            path = _path;
            file = new FileStream(path, FileMode.Open);
            fin = new StreamReader(file);
            creator = new TestCreater();
        }

        void ParseHeader()
        {
            string tmp;
            while ((tmp = fin.ReadLine()) != "END HEADER")
            {
                if (tmp == "") continue;
                string[] strs = tmp.Split('|');
                if (strs[0] == "Main Question Number")
                {
                    creator.setMainEntity(Convert.ToInt32(strs[1]));
                    continue;
                }
                //TODO add support of fields header: author data eth
            }
        }

        public void Parse()
        {
            ParseHeader();
            string tmp;
            while ((tmp = fin.ReadLine()) != "END.")
            {
                if (tmp == "") continue;
                string[] strs = tmp.Split('|');
                AddEntity(strs);
            }
            creator.setRecords(entityRecords);
            fin.Close();
            file.Close();
        }

        public Link getRootLink()
        {
            if(creator.getLink() == null)creator.generate();
            return creator.getLink();
        }
        private void AddEntity(string[] strs)
        {
            RecEntity tmp;
            if (strs[1] == "Q")
            {
                tmp = CreateQwest(strs);
            }
            else if (strs[1] == "A")
            {
                tmp = CreateAnswer(strs);
            }
            else if (strs[1] == "T")
            {
                tmp = CreateText(strs);
            }
            else tmp = null;
            entityRecords[Convert.ToInt32(strs[0])] = tmp;        
        }

        private RecEntity CreateText(string[] strs)
        {
            TextEntity res = new TextEntity();
            res.num = Convert.ToInt32(strs[0]);
            res.text = strs[2];
            res.type = typeEntity.text;
            return res;
        }

        private RecEntity CreateAnswer(string[] strs)
        {
            AnswerEntity res = new AnswerEntity();
            res.num = Convert.ToInt32(strs[0]);
            res.numText = Convert.ToInt32(strs[2]);
            res.type = typeEntity.answer;
            return res;
        }

        private RecEntity CreateQwest(string[] strs)
        {
            QuestEntity res = new QuestEntity();
            res.num = Convert.ToInt32(strs[0]);
            int numOfLines = Convert.ToInt32(strs[2]);
            for (int i = 0; i < numOfLines; i++)
            {
                res.numbersTextLines.Add(Convert.ToInt32(strs[3 + i]));
            }
            int count = Convert.ToInt32(strs[3+ numOfLines]);
            numOfLines += 4;// стартовая позиция
            for(int i = 0; i < count; i++)
            {
                res.numbersAnswersTexts.Add(Convert.ToInt32(strs[numOfLines + i]));
                res.numbersAnswers.Add(Convert.ToInt32(strs[numOfLines + count + i]));
            }
            res.type = typeEntity.question;
            return res;
        }
        
    }

    public class TestCreater
    {
        Dictionary<int, RecEntity> entityRecords;
        Dictionary<int, Link> numbersLinks;
        int mainEntity;
        Question startQuestion;
        public TestCreater ()
        {
            startQuestion = null;
            numbersLinks = new Dictionary<int, Link>();
        }

        public void setMainEntity(int mainEntity)
        {
            this.mainEntity = mainEntity;
        }

        public void setRecords(Dictionary<int, RecEntity> entityRecords)
        {
            this.entityRecords = entityRecords;
        }

        internal void generate()
        {
            if (!(entityRecords[mainEntity].type == typeEntity.question)) throw new Exception("Type of start entity must be a question!");
            startQuestion = (linkEntityHandler(mainEntity) as Question);
        }

        private Link linkEntityHandler(int entityNumber)
        {
            if (numbersLinks.ContainsKey(entityNumber)) return numbersLinks[entityNumber];
            Link currentLink;
            if(entityRecords[entityNumber].type == typeEntity.text) throw new Exception("Type of start entity must be a question or answer!");
            if(entityRecords[entityNumber].type == typeEntity.question)
            {
                int countTextLines = (entityRecords[entityNumber] as QuestEntity).numbersTextLines.Count;
                String text = "";
                for (int i = 0; i < countTextLines; i++)
                {
                    text += textEntityHandler((entityRecords[entityNumber] as QuestEntity).numbersTextLines[i])+"\n";
                }
                currentLink = new Question(text);
                numbersLinks.Add(entityNumber, currentLink);
                int countAnswers = (entityRecords[entityNumber] as QuestEntity).numbersAnswersTexts.Count;
                for (int i = 0; i< countAnswers; i++)
                {
                    (currentLink as Question).addAnswer(textEntityHandler((entityRecords[entityNumber] as QuestEntity).numbersAnswersTexts[i]),
                        linkEntityHandler((entityRecords[entityNumber] as QuestEntity).numbersAnswers[i]));
                }
            }
            else
            {
                int numText = (entityRecords[entityNumber] as AnswerEntity).numText;
                currentLink = new Answer(textEntityHandler(numText));
            }
            return currentLink;
        }

        private string textEntityHandler(int entityNumber)
        {
            if(entityRecords[entityNumber].type != typeEntity.text) throw new Exception("Type of start entity must be a text!");
            return (entityRecords[entityNumber] as TextEntity).text;
        }

        internal Link getLink()
        {
            return startQuestion;
        }
    }
}
