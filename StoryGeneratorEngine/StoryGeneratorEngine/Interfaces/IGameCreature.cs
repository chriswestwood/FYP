using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.Interfaces
{
    interface IGameCreature
    {

        ICreature getCreature();

        string updateHealth(int amount);

        string attack(IPlayerCharacter player);

        bool getIsHostile();

        int getDamage();
        int getDefence();

        int getHealth();
        string outputData();
    }
}
