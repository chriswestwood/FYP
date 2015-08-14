using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.Interfaces
{
    interface IPlot
    {
       
        void setName(string n);
        void setDesc(string d);
        string getName();

        string getDesc();
        void setModifier(string m);

        string getModifier();
        void setType(string t);
        string getType();
        void setValue(int v);
        
    }
}
