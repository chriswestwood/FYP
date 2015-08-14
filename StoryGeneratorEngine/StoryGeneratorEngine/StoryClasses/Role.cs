using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.StoryClasses
{
    class Role : IRole
    {

        private string name;
        private string type;
        private string description;

        public Role() 
        {

        }
        public void setName(string name)
        {
            this.name = name;
        }

        public void addType(string type)
        {
            this.type = type;
        }

        public void setDesc(string desc)
        {
            description = desc;
        }
        public string outputData()
        {
            return "Role: " + name + "\nDescription: " + description;
        }

        public string getName()
        {
            return name;
        }
        public string getType()
        {
            return type;
        }
        public string getDesc()
        {
            return description;
        }

       
    }
}
