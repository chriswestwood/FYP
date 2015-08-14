using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.GameClasses
{
    interface IItem
    {
        void addType(string t);
        List<string> getTypes();

        bool hasType(string type);

        void setName(string n);

        void setDesc(string desc);

        string getName();

        void setRarity(int r);
        int getRarity();

        void increaseAmount(int a);
        void decreaseAmount(int a);
        int getAmount();

        string outputData();
        string Use(IGameCreature target);
        string Use(IPlayerCharacter player);
        string Use(ILocation loc, IPlayerCharacter player);
    }
}
