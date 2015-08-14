using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.Interfaces
{
    interface IRole
    {
        void setName(string name);
        void addType(string type);
        void setDesc(string desc);
        string outputData();


        string getName();
       string getType();
       string getDesc();


    }
}
