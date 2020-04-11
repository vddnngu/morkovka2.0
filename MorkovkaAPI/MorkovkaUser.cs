using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorkovkaAPI
{
    public class MorkovkaUser
    {
        string name;
        public void setName(string _name)
        {
            name = _name;
        }
        public string getName()
        {
            return name;
        }
    }
}
