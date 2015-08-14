using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.Interfaces
{
    interface ICreature
    {

        void setName(string n);

        string getName();
        void addType(string type);
        void setDesc(string desc);
        void setValue(int v);

        bool hasType(string type);

        int getValue();

        List<string> getTypes();

        string outputData();
    }
}
