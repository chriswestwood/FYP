using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.GameClasses
{
    class Character : ICharacter
    {
        private string name;
        private IRole role;
        private string race = "Human";
        private Colours eyeColour = Colours.blue, hairColour = Colours.yellow;
        private HairTypes hairType = HairTypes.Short;
        private int age = 18;
        private char gender = 'M';

        public Character() { }
        public Character(string name, char gender, int age, IRole role, string race)
        {
            this.name = name;
            this.gender = gender;
            this.age = age;
            this.role = role;
            this.race = race;
            // give random hair type , eye colour , hair colour 
        }
        public string getName()
        {
            return name;
        }

        public IRole getRole()
        {
            return role;
        }
        public char getGender()
        {
            return gender;
        }

        public int getAge()
        {
            return age;
        }



        public void setName(string name)
        {
            this.name = name;

        }

        public void setAge(int age)
        {

            this.age = age;

        }

        public void setRole(IRole role)
        {

            this.role = role;
        }

        public void setGender(char gender)
        {

            this.gender = gender;

        }


        public void setRace(string race)
        {
            this.race = race;
        }

        public string getRace()
        {
            return race;
        }

        public string outputData()
        {
            // create string for output in particular format
            string g = "";
            if(gender == 'M')
            {
                g = "male";
            }
            if (gender == 'F')
            {
                g = "female";
            }
            string output = name + "\n";
            output += "A " + g + " " + race + " " + role.getName() + "\n";
            output += role.getDesc();
            return output;
        }


    }
}
