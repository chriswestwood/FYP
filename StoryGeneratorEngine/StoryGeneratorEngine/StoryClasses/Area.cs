using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.GameClasses
{
    class Area : IArea
    {
        private string name;
        private IAreaType type;
        private string description;
        public Area()
        {
           
        }
        public Area(string name, IAreaType type)
        {
            this.name = name;
            this.type = type;
        }
        public void addType(IAreaType t)
        {
            type = t;
        }

        public void setName(string n)
        {
            name = n;
        }

        public IAreaType getType()
        {
            return type;
        }

        public string getName()
        {
            return name;
        }


        public string outputData()
        {
            return name;
        }


        public void setDesc(string d)
        {
            description = d;
        }
        public string getDesc()
        {
            return description;
        }
    }
}
