using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StoryGeneratorEngine.StoryClasses
{
    class AreaType :IAreaType
    {
        private string name;
        private string colour;
        private int value;
        private string description;
        public void setName(string n)
        {
            name = n;
        }

        public void setColour(string c)
        {
            colour = c;
        }

        public void setValue(string i)
        {
            value = Int32.Parse(i);
        }

        public void setDesc(string desc)
        {
            description = desc;
        }

        public int getValue()
        {
            return value;
        }
        public string getName()
        {
            return name;
        }

        public string outputData()
        {
            string output;
            output = "Area: " + name + "\nColour: " + colour + "\nDesc: " + description;
            return output;
        }


        public string getColour()
        {
            return colour;
        }

        public string getDesc()
        {

            return description;
        }
    }
}
