using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.StoryClasses
{
    class Creature : ICreature
    {
        private string name;
        private List<string> types = new List<string>();
        private int value;
        private string description;
        private bool bIsHostile;
        public Creature()
        {

        }
        public void setName(string n)
        {
            name = n;
        }

        public void addType(string type)
        {
            types.Add(type);
        }

        public void setDesc(string desc)
        {
            description = desc;
        }

        public void setValue(int v)
        {
            value = v;
        }


        public string outputData()
        {
            string output = "It is a " + name + "\n";
            output += description + "\n";
            foreach (string s in types)
            {
                output += s + "\n";
            }
            if (bIsHostile)
            {
                output += "It is hostile towards you" + "\n";
            }
            else
            {
                output += "It is neutral towards you" + "\n";
            }
            return output;
        }
        public string getName() { return name; }


        public int getValue()
        {
            return value;
        }


        public List<string> getTypes()
        {
            return types;
        }


        public bool hasType(string type)
        {
            return types.Contains(type);
        }
    }
}
