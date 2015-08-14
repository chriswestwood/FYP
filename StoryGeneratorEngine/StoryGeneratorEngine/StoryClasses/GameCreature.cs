using StoryGeneratorEngine.EngineClasses;
using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.StoryClasses
{
    class GameCreature : IGameCreature
    {

        ICreature creature;
        // stats
        int health;
        int attackDamage;
        int defence;
        bool bIsHostile = false;
        public GameCreature(ICreature c)
        {
            creature = c;
            foreach(string s in c.getTypes())
            {
                if(s.ToLower() == "hostile")
                {
                    bIsHostile = true;
                }
            }
            health = ((StoryGenerationClass.Instance.getRandomNumber(creature.getValue()) + 1) * creature.getValue());
            attackDamage = StoryGenerationClass.Instance.getRandomNumber(creature.getValue()) + creature.getValue() / 2;
            defence = creature.getValue();
        }
        public ICreature getCreature()
        {
            return creature;
        }


        public string updateHealth(int amount)
        {
            health += amount;
            if (amount < 0)
            {
                bIsHostile = true;
                return creature.getName() +" was attacked for " + -amount + " damage\n";
            }
            else
            {
                return creature.getName() + " was healed for " + -amount + "\n";
            }
        }


        public string outputData()
        {
            string output = creature.outputData();
            output += "Health: " + health;
            return output;
        }


        public string attack(IPlayerCharacter player)
        {
            int hitDamage;
            if(StoryGenerationClass.Instance.getRandomNumber(100) > 50)
            {
                // hit
                hitDamage =StoryGenerationClass.Instance.getRandomNumber(getDamage()+1) + 1;
                player.updateHealth(-hitDamage);
                return creature.getName() + " attacked you for " + hitDamage + " damage\n" + "You have " + player.getHealth() + " health remaining\n";
            }
            return creature.getName() + " missed you with their attack\n";
        }


        public bool getIsHostile()
        {
            return bIsHostile;
        }


        public int getDamage()
        {
            return attackDamage;
        }

        public int getDefence()
        {
            return defence;
        }

        public int getHealth()
        {
            return health;
        }
    }
}
