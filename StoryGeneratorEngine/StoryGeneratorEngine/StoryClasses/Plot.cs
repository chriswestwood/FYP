using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.StoryClasses
{
    class Plot : IPlot
    {
        private string name;
        private string modifier;
        private string type; // type to modify
        private int value;
        private string description;
        public void setName(string n)
        {
            name = n;
        }

        public void setModifier(string m)
        {
            modifier = m;
        }

        public void setType(string t)
        {
            type = t;
        }


       
        public string getName()
        {
            return name;
        }

        public string getDesc()
        {
            return description;
        }


        public void setDesc(string d)
        {
            description = d;
        }

        public void setValue(int v)
        {
            value = v;
        }


        public string getModifier()
        {
            return modifier;
        }


        public string getType()
        {
            return type;
        }
    }
}
