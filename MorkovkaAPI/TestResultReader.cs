using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorkovkaAPI
{
    public class ResEntity
    {
        public string name;
        public string date;
        public string start;
        public string finish;
        public string result;
        public string testPath;
        
    }
    public class TestResultParser
    {
        string path;
        FileStream file;
        StreamReader fin;
        string testPath;
        public List<ResEntity> resEntities;
        public TestResultParser(string _path)
        {
            path = _path;
            file = new FileStream(path, FileMode.Open);
            fin = new StreamReader(file);

        }
        public TestResult getTestResult()
        {
            ResultCreator creator = new ResultCreator();
            List<Attempt> attempt = creator.getTestResults(resEntities);
            TestParser testParser = new TestParser(testPath);
            testParser.Parse();
            TestProcessing game = new TestProcessing(testParser.getRootLink());
            TestResult testResult = new TestResult();
            testResult.setTestProcessing(game);
            testResult.setAttempts(attempt);
            testResult.setTestPath(testPath);
            testResult.setOwnPath(path);
            return testResult;
        }
        public void allParse()
        {
            ParseHeader();
            Parse();
        }
        public void ParseHeader()
        {
            string tmp;
            while ((tmp = fin.ReadLine()) != "END HEADER")
            {
                if (tmp == "") continue;
                string[] strs = tmp.Split('|');
                if (strs[0] == "TestPath")
                {
                    testPath = strs[1];
                    continue;
                }  
            }
        }
        public void Parse()
        {
            resEntities = new List<ResEntity>();
            string tmp;
            while ((tmp = fin.ReadLine()) != "END")
            {
                if (tmp == "") continue;
                string[] strs = tmp.Split('|');
                ResEntity entity = new ResEntity();
                entity.name = strs[0];
                entity.date = strs[1];
                entity.start = strs[2];
                entity.finish = strs[3];
                entity.result = strs[4];
                entity.testPath = testPath;
                resEntities.Add(entity);
            }
        }
        
    }
    public class ResultCreator
    {
        List<Attempt> attempts = new List<Attempt>();
        public Attempt entityHandler(ResEntity _resEntity)
        {
            Attempt result = new Attempt();
            result.setName(_resEntity.name);
            result.setDate(new Date(_resEntity.date));
            result.setTimeStart(new Time(_resEntity.start));
            result.setTimeFinish(new Time(_resEntity.finish));
            string[] strs = _resEntity.result.Split(' ');
            for (int i = 0; i<strs.Length; i++)
            {
                result.getListAnswers().Add(Convert.ToInt32(strs[i]));
            }
            return result;
        }
        public List<Attempt> getTestResults (List<ResEntity> _resEntity)
        {
            TestParser parser = new TestParser(_resEntity[0].testPath);
            parser.Parse();
            TestProcessing game = new TestProcessing(parser.getRootLink());
            for (int i=0; i<_resEntity.Count; i++)
            {
                Attempt result = entityHandler(_resEntity[i]);
                result.setTestProcessing(game);
                attempts.Add(result);
            }
            return attempts;
        }

 
    }

}
