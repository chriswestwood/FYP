using StoryGeneratorEngine.GameClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.Interfaces
{
    interface IPlayerCharacter
    {
        string addItem(IItem item);
        void removeItem(string item);
        string UseItem(string target, string item);

        string getItemNames();
        List<IItem> getInv();

        string equip(IItem item);

        string updateHealth(int amount);

        string attack(IGameCreature creature);

        string addNewItem(string name = null, string type = null, string areaType = null, int highestValue = 10);

        void updateScore(int value);

        int getScore();
        int getHealth();

        string outputData();
    }
}
