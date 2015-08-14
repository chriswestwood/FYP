using StoryGeneratorEngine.EngineClasses;
using StoryGeneratorEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryGeneratorEngine.GameClasses
{
    class PlayerCharacter : IPlayerCharacter
    {
        private ICharacter self;
        private List<IItem> inventory = new List<IItem>();
        private IItem weapon = null;
        private int health;
        //private IItem head,body,arm,leg,feet; // protection
        private int score;


        public PlayerCharacter(ICharacter c)
        {
            self = c;
            health = 100;
            score = 0;
        }


        public string addItem(IItem item)
        {

            inventory.Add(item);
            return "added " + item.getName() + " to inventory\n";
        }

        public void removeItem(string item)
        {
            //find item
            // remove item
        }

        public string UseItem(string target, string item)
        {
            throw new NotImplementedException();
        }
        public string updateHealth(int amount)
        {
            health += amount;
            if (health > 100) health = 100;
            updateScore(amount);
            if(amount <0)
            {
                return "You were attacked for " + amount + " damage\n";
            }
            else
            {
                return "You were healed for " + amount + "\n";
            }
        }


        public string getItemNames()
        {
            string invNames = "Current Inventory:" + "\n";
            foreach(IItem i in inventory)
            {
                invNames+= i.getName() + "\n";
            }
            if(weapon != null)  invNames += "Currently Equipped: " + weapon.getName() + "\n";
            return invNames;
        }
        public List<IItem> getInv()
        {
            return inventory;
        }


        public void updateScore(int value)
        {
            score += value;
        }


        public string equip(IItem item)
        {
            if(weapon != null)inventory.Add(weapon);
            weapon = item;
            return "You have equipped the " + item.getName() + "\n";
        }


        public string attack(IGameCreature creature)
        {
            int hitValue = StoryGenerationClass.Instance.getRandomNumber(100);
            if(hitValue > creature.getCreature().getValue() * 5)
            {
                // hit
                int hitDamage;
                if(weapon != null)
                {
                    hitDamage = StoryGenerationClass.Instance.getRandomNumber(weapon.getRarity() + 1) + 1;
                }
                else hitDamage = StoryGenerationClass.Instance.getRandomNumber(2) + 1;

                creature.updateHealth(-hitDamage);
                return "You dealt " + hitDamage + " damage to " + creature.getCreature().getName() + "\n" + creature.getCreature().getName() + " has " + creature.getHealth() + " health left\n";
            }
            else
            {
                // miss
                return "You attack was blocked by " + creature.getCreature().getName() + "\n";
            }
        }


        public int getHealth()
        {
            return health;
        }

        public string addNewItem(string name = null, string type = null, string areaType = null, int highestValue = 10)
        {
            IItem newItem = StoryGenerationClass.Instance.getItem(name, type, areaType, highestValue);
            inventory.Add(newItem);
            return newItem.getName();
        }


        public string outputData()
        {
            string output="";
            output += "You are " + self.outputData() + "\n";
            output += "CurrentHealth: " + health + "\n";
            output += getItemNames();
            return output;
        }


        public int getScore()
        {
            return score;
        }
    }
}
