using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.Interfaces
{

    enum Colours // for use with eye and hair colour
    {
        blue,
        green,
        brown,
        purple,
        red,
        yellow,
    }
    enum HairTypes
    {
        Long,
        Short,
        VeryLong,
        VeryShort,
        Bald
    }
    interface ICharacter
    {
        void setName(string name);
        void setAge(int age);
        void setRole(IRole role);
        void setGender(char gender);
        void setRace(string race);
        string getName();
        IRole getRole();
        char getGender();
        int getAge();
        string getRace();


        string outputData();
    }
}
