using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.StoryClasses
{
    class Event : IEvent
    {
        private string areaType = "";
        private List<string> mods = new List<string>();
        private List<int> values = new List<int>();
        private List<string> types = new List<string>();
        private string description = "";


        public Event()
        {

        }
        public Event(string aT)
        {
            areaType = aT;
        }
        public string getAreaType()
        {
            return areaType;
        }


        public void setAreaType(string at)
        {
            areaType = at;
        }

        public void addMod(string m)
        {
            mods.Add(m);
        }

        public void addValue(int v)
        {
            values.Add(v);
        }

        public void addType(string t)
        {
            types.Add(t);
        }

        public void setDesc(string d)
        {
           description = d;
        }

        public List<string> getMods()
        {
           return mods;
        }

        public List<int> getValues()
        {
            return values;
        }

        public List<string> getTypes()
        {
           return types;
        }

        public string outputData()
        {
           return description + "\n";
        }
    }
}
