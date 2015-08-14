using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.Interfaces
{
    interface IEvent
    {

        string getAreaType();
        void setAreaType(string at);
        void addMod(string m);
        void addValue(int v);
        void addType(string t);
        void setDesc(string d);
        List<string> getMods();
        List<int> getValues();
        List<string> getTypes();
        string outputData();
    }
}
