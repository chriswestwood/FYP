using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.Interfaces
{
    interface IAreaType
    {
        void setName(string n);
        void setColour(string c);
        void setValue(string i);
        void setDesc(string desc);
        string getDesc();
        int getValue();

        string getName();

        string getColour();

        string outputData();
    }
}
