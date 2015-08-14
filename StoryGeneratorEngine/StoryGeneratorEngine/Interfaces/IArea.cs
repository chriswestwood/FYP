using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.Interfaces
{
    interface IArea
    {
        void addType(IAreaType t);
        void setName(string n);

        void setDesc(string d);

        string getDesc();
        IAreaType getType();
        string getName();

        string outputData();
    }
}
